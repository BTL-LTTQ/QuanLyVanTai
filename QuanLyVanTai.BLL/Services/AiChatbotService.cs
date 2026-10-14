using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Logging;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Dịch vụ AI Chatbot hỗ trợ khách hàng - gọi Gemini API và OpenAI API bất đồng bộ.
    /// Tự động fallback về câu trả lời nội bộ nếu API không khả dụng.
    /// </summary>
    public class AiChatbotService : IAiChatbotService
    {
        private static readonly HttpClient _httpClient;
        private readonly AiChatbotConfig _config;

        static AiChatbotService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public string CurrentProvider => _config.Provider;

        public AiChatbotService(AiChatbotConfig config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        // ====================================================================================
        // HÀM CHÍNH: Gửi tin nhắn và nhận phản hồi
        // ====================================================================================
        public async Task<AiChatbotResponse> SendMessageAsync(
            string userMessage,
            List<ChatMessageDto>? conversationHistory = null,
            string? contextInfo = null,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return new AiChatbotResponse { Success = false, ErrorMessage = "Tin nhắn không được để trống." };

            if (string.IsNullOrWhiteSpace(_config.ApiKey) || _config.ApiKey.StartsWith("YOUR_"))
            {
                LoggerService.Warning("AI Chatbot: API Key chưa được cấu hình. Sử dụng chế độ demo ngoại tuyến.");
                return GetOfflineFallbackResponse(userMessage, null, contextInfo);
            }

            try
            {
                return _config.Provider.Equals("OpenAI", StringComparison.OrdinalIgnoreCase)
                    ? await CallOpenAiAsync(userMessage, conversationHistory, contextInfo, cancellationToken)
                    : await CallGeminiAsync(userMessage, conversationHistory, contextInfo, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                LoggerService.Warning("AI Chatbot: Request bị hủy do timeout sau 30 giây.");
                return new AiChatbotResponse
                {
                    Success = false,
                    IsFallback = true,
                    ErrorMessage = "Yêu cầu tới AI bị quá thời gian (timeout). Vui lòng thử lại.",
                    ResponseText = "Xin lỗi, hệ thống AI đang bận. Vui lòng thử lại sau giây lát hoặc liên hệ nhân viên để được hỗ trợ."
                };
            }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "AI Chatbot: Lỗi khi gọi {Provider} API.", _config.Provider);
                return GetOfflineFallbackResponse(userMessage, ex.Message, contextInfo);
            }
        }

        // ====================================================================================
        // GEMINI API (Google AI Studio)
        // Endpoint: https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent
        // ====================================================================================
        private async Task<AiChatbotResponse> CallGeminiAsync(
            string userMessage,
            List<ChatMessageDto>? history,
            string? contextInfo,
            CancellationToken ct)
        {
            string model = string.IsNullOrWhiteSpace(_config.Model) ? "gemini-1.5-flash" : _config.Model;
            string endpoint = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_config.ApiKey}";

            // Xây dựng danh sách turn (contents) - bỏ qua system role vì Gemini dùng systemInstruction
            var contents = new List<object>();

            // Thêm lịch sử hội thoại vào contents
            if (history != null)
            {
                foreach (var msg in history)
                {
                    if (msg.Role == "system") continue; // bỏ system, sẽ đưa vào systemInstruction
                    string geminiRole = msg.Role == "assistant" ? "model" : "user";
                    contents.Add(new { role = geminiRole, parts = new[] { new { text = msg.Content } } });
                }
            }

            // Thêm tin nhắn mới nhất của người dùng
            contents.Add(new { role = "user", parts = new[] { new { text = userMessage } } });

            string systemPrompt = BuildSystemPrompt(contextInfo);

            var requestBody = new
            {
                systemInstruction = new { parts = new[] { new { text = systemPrompt } } },
                contents = contents,
                generationConfig = new
                {
                    temperature = _config.Temperature,
                    maxOutputTokens = _config.MaxTokens,
                    topP = 0.9
                },
                safetySettings = new[]
                {
                    new { category = "HARM_CATEGORY_HARASSMENT", threshold = "BLOCK_ONLY_HIGH" },
                    new { category = "HARM_CATEGORY_HATE_SPEECH", threshold = "BLOCK_ONLY_HIGH" }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, content, ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                LoggerService.Warning("Gemini API trả về lỗi HTTP {StatusCode}: {Body}", (int)response.StatusCode, responseBody);
                return GetOfflineFallbackResponse(userMessage, $"Lỗi HTTP {(int)response.StatusCode}");
            }

            // Parse phản hồi JSON của Gemini
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            if (root.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
            {
                var firstCandidate = candidates[0];
                if (firstCandidate.TryGetProperty("content", out var contentPart) &&
                    contentPart.TryGetProperty("parts", out var parts) &&
                    parts.GetArrayLength() > 0)
                {
                    string text = parts[0].GetProperty("text").GetString() ?? "";
                    LoggerService.Info("AI Chatbot [Gemini]: Phản hồi thành công ({Length} ký tự).", text.Length);
                    return new AiChatbotResponse { Success = true, ResponseText = text.Trim(), ProviderUsed = "Gemini" };
                }
            }

            return GetOfflineFallbackResponse(userMessage, "Không parse được phản hồi từ Gemini.");
        }

        // ====================================================================================
        // OPENAI API (gpt-4o-mini, gpt-3.5-turbo...)
        // Endpoint: https://api.openai.com/v1/chat/completions
        // ====================================================================================
        private async Task<AiChatbotResponse> CallOpenAiAsync(
            string userMessage,
            List<ChatMessageDto>? history,
            string? contextInfo,
            CancellationToken ct)
        {
            string endpoint = !string.IsNullOrWhiteSpace(_config.CustomApiUrl)
                ? _config.CustomApiUrl
                : "https://api.openai.com/v1/chat/completions";

            string model = string.IsNullOrWhiteSpace(_config.Model) ? "gpt-4o-mini" : _config.Model;

            // Xây dựng danh sách messages
            var messages = new List<object>
            {
                new { role = "system", content = BuildSystemPrompt(contextInfo) }
            };

            if (history != null)
            {
                foreach (var msg in history.Where(m => m.Role != "system"))
                {
                    string openAiRole = msg.Role == "model" ? "assistant" : msg.Role;
                    messages.Add(new { role = openAiRole, content = msg.Content });
                }
            }

            messages.Add(new { role = "user", content = userMessage });

            var requestBody = new
            {
                model = model,
                messages = messages,
                temperature = _config.Temperature,
                max_tokens = _config.MaxTokens
            };

            var json = JsonSerializer.Serialize(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _config.ApiKey);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request, ct);
            var responseBody = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                LoggerService.Warning("OpenAI API trả về lỗi HTTP {StatusCode}: {Body}", (int)response.StatusCode, responseBody);
                return GetOfflineFallbackResponse(userMessage, $"Lỗi HTTP {(int)response.StatusCode}");
            }

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            if (root.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
            {
                var firstChoice = choices[0];
                if (firstChoice.TryGetProperty("message", out var message) &&
                    message.TryGetProperty("content", out var contentEl))
                {
                    string text = contentEl.GetString() ?? "";
                    LoggerService.Info("AI Chatbot [OpenAI]: Phản hồi thành công ({Length} ký tự).", text.Length);
                    return new AiChatbotResponse { Success = true, ResponseText = text.Trim(), ProviderUsed = "OpenAI" };
                }
            }

            return GetOfflineFallbackResponse(userMessage, "Không parse được phản hồi từ OpenAI.");
        }

        // ====================================================================================
        // SYSTEM PROMPT định hướng vai trò Chatbot là nhân viên tư vấn nhà xe
        // ====================================================================================
        public string BuildSystemPrompt(string? extraContext = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Bạn là một nhân viên tư vấn thân thiện, chuyên nghiệp của nhà xe VanTai Express.");
            sb.AppendLine("Nhiệm vụ của bạn là tư vấn và hỗ trợ khách hàng về:");
            sb.AppendLine("1. Lịch trình chạy xe: Giờ xuất phát, hành trình, thời gian di chuyển ước tính");
            sb.AppendLine("2. Giá vé: Giá cước, phụ thu, ưu đãi (nếu có)");
            sb.AppendLine("3. Thông tin tuyến đường: Điểm đón, trả khách, các trạm trung chuyển");
            sb.AppendLine("4. Loại xe: Ghế ngồi, giường nằm, limousine, số chỗ");
            sb.AppendLine("5. Hướng dẫn đặt vé, thanh toán, hoàn/đổi vé");
            sb.AppendLine("6. Thông tin liên hệ hỗ trợ: Hotline, địa chỉ văn phòng");
            sb.AppendLine();
            sb.AppendLine("Quy tắc trả lời:");
            sb.AppendLine("- Luôn dùng tiếng Việt, thân thiện, lịch sự");
            sb.AppendLine("- Ưu tiên trả lời ngắn gọn, đúng trọng tâm câu hỏi");
            sb.AppendLine("- Nếu không có thông tin cụ thể, hướng khách gọi Hotline: 1800-1234");
            sb.AppendLine("- Không tự bịa thông tin giá vé/lịch trình nếu không chắc chắn");
            sb.AppendLine("- Kết thúc mỗi câu trả lời bằng câu hỏi mở để giữ cuộc trò chuyện");

            if (!string.IsNullOrWhiteSpace(extraContext))
            {
                sb.AppendLine();
                sb.AppendLine("=== THÔNG TIN HỆ THỐNG THỰC TẾ CỦA NHÀ XE (dùng để tư vấn) ===");
                sb.AppendLine(extraContext);
            }

            return sb.ToString();
        }

        // ====================================================================================
        // TEST KẾT NỐI
        // ====================================================================================
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var result = await SendMessageAsync("Xin chào, bạn có thể giúp tôi không?");
                return result.Success;
            }
            catch
            {
                return false;
            }
        }

        // ====================================================================================
        // FALLBACK nội bộ khi API không khả dụng (không cần internet)
        // ====================================================================================
        private static AiChatbotResponse GetOfflineFallbackResponse(string userMessage, string? errorDetails = null, string? contextInfo = null)
        {
            string text = userMessage.ToLower();
            string reply;

            if (text.Contains("giá vé") || text.Contains("giá") || text.Contains("bao nhiêu tiền"))
            {
                if (!string.IsNullOrWhiteSpace(contextInfo) && contextInfo.Contains("TUYẾN XE"))
                {
                    reply = "💰 Bảng giá vé các tuyến đang khai thác:\n" +
                            contextInfo.Trim() + "\n\n" +
                            "Giá vé đã bao gồm bảo hiểm hành khách và nước uống. Bạn muốn đặt vé cho tuyến nào ạ?";
                }
                else
                {
                    reply = "💰 Giá vé tham khảo của nhà xe:\n" +
                            "• Tuyến Hà Nội - Đà Nẵng: từ 380.000đ\n" +
                            "• Tuyến Hà Nội - TP. HCM: từ 750.000đ\n" +
                            "• Tuyến TP. HCM - Đà Lạt: từ 280.000đ\n" +
                            "• Tuyến Hà Nội - Hải Phòng: từ 120.000đ\n" +
                            "• Tuyến TP. HCM - Vũng Tàu: từ 150.000đ\n\n" +
                            "Giá có thể thay đổi theo hạng ghế/giường nằm. Bạn muốn đi tuyến nào và thời gian nào ạ?";
                }
            }
            else if (text.Contains("lịch") || text.Contains("giờ") || text.Contains("mấy giờ") || text.Contains("khởi hành"))
            {
                reply = "🕐 Xe xuất bến hàng ngày theo các khung giờ:\n" +
                        "• Sáng sớm: 5:00 - 7:00\n" +
                        "• Buổi sáng: 8:00 - 10:00\n" +
                        "• Buổi chiều: 13:00 - 15:00\n" +
                        "• Tối & đêm: 18:00 - 22:00\n\n" +
                        "Lịch cụ thể theo tuyến, bạn muốn hỏi tuyến nào ạ?";
            }
            else if (text.Contains("đặt vé") || text.Contains("mua vé") || text.Contains("booking"))
            {
                reply = "🎫 Để đặt vé, bạn có thể:\n" +
                        "1. Gọi Hotline: **1800-1234** (miễn phí 8:00-22:00)\n" +
                        "2. Đến trực tiếp văn phòng nhà xe\n" +
                        "3. Nhờ nhân viên tại đây hỗ trợ đặt vé\n\n" +
                        "Bạn muốn đặt vé tuyến nào và ngày nào ạ?";
            }
            else if (text.Contains("hoàn") || text.Contains("đổi") || text.Contains("trả vé") || text.Contains("hủy"))
            {
                reply = "🔄 Chính sách đổi/hoàn vé:\n" +
                        "• Trước 24h: Hoàn 80% giá vé\n" +
                        "• Trước 12h: Hoàn 50% giá vé\n" +
                        "• Dưới 12h: Không hoàn tiền\n\n" +
                        "Để đổi/hoàn vé, vui lòng liên hệ Hotline: 1800-1234. Bạn cần hỗ trợ thêm gì không ạ?";
            }
            else if (text.Contains("điểm đón") || text.Contains("trạm") || text.Contains("bến xe") || text.Contains("lên xe"))
            {
                reply = "📍 Các điểm đón/trả khách chính:\n" +
                        "• **Hà Nội**: Bến xe Mỹ Đình, Bến xe Giáp Bát\n" +
                        "• **TP. HCM**: Bến xe Miền Đông mới (TP. Thủ Đức)\n" +
                        "• **Đà Nẵng**: Bến xe Trung tâm Đà Nẵng\n" +
                        "• **Đà Lạt**: Bến xe Liên tỉnh Đà Lạt\n\n" +
                        "Bạn cần thông tin điểm đón cho tuyến cụ thể nào ạ?";
            }
            else if (text.Contains("loại xe") || text.Contains("ghế") || text.Contains("giường") || text.Contains("limousine"))
            {
                reply = "🚌 Các loại xe phục vụ:\n" +
                        "• **Ghế ngồi**: Xe 45-50 chỗ, tiết kiệm\n" +
                        "• **Giường nằm**: Xe 36-40 chỗ, thoải mái cho chuyến dài\n" +
                        "• **Limousine VIP**: Xe 22-24 phòng, cao cấp, WiFi, nước uống\n\n" +
                        "Bạn ưu tiên loại xe nào cho chuyến đi ạ?";
            }
            else if (text.Contains("xin chào") || text.Contains("hello") || text.Contains("hi") || text.Contains("chào"))
            {
                reply = "👋 Xin chào! Tôi là trợ lý ảo của nhà xe **VanTai Express**.\n\n" +
                        "Tôi có thể giúp bạn tư vấn về:\n" +
                        "🎫 Giá vé & đặt vé\n" +
                        "🕐 Lịch trình xe chạy\n" +
                        "📍 Điểm đón/trả khách\n" +
                        "🚌 Các loại xe\n\n" +
                        "Bạn cần hỗ trợ gì ạ?";
            }
            else
            {
                reply = "🤝 Cảm ơn bạn đã liên hệ nhà xe **VanTai Express**!\n\n" +
                        "Hiện tại hệ thống AI đang bảo trì tạm thời. Để được hỗ trợ nhanh nhất, vui lòng:\n" +
                        "📞 Gọi Hotline: **1800-1234** (miễn phí, 8:00-22:00)\n" +
                        "💬 Nhắn tin Fanpage: **facebook.com/VanTaiExpress**\n\n" +
                        "Bạn có câu hỏi nào khác không ạ?";
            }

            return new AiChatbotResponse
            {
                Success = true,
                ResponseText = reply,
                ProviderUsed = "Offline-Fallback",
                IsFallback = true
            };
        }
    }
}
