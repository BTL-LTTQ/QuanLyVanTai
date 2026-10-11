using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Interface dịch vụ AI Chatbot hỗ trợ khách hàng.
    /// Hỗ trợ gọi Gemini API và OpenAI API bất đồng bộ.
    /// </summary>
    public interface IAiChatbotService
    {
        /// <summary>
        /// Gửi tin nhắn tới AI và nhận phản hồi (bất đồng bộ, hỗ trợ lịch sử hội thoại)
        /// </summary>
        /// <param name="userMessage">Tin nhắn từ người dùng</param>
        /// <param name="conversationHistory">Lịch sử hội thoại trước đó để giữ ngữ cảnh</param>
        /// <param name="contextInfo">Thông tin ngữ cảnh về tuyến xe, giá vé... để AI tư vấn tốt hơn</param>
        /// <param name="cancellationToken">Token hủy tác vụ</param>
        Task<AiChatbotResponse> SendMessageAsync(
            string userMessage,
            List<ChatMessageDto>? conversationHistory = null,
            string? contextInfo = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Kiểm tra cấu hình và kết nối tới AI API
        /// </summary>
        Task<bool> TestConnectionAsync();

        /// <summary>
        /// Tạo System Prompt định hướng AI đóng vai nhân viên tư vấn nhà xe
        /// </summary>
        string BuildSystemPrompt(string? extraContext = null);

        /// <summary>
        /// Thông tin nhà cung cấp AI đang dùng
        /// </summary>
        string CurrentProvider { get; }
    }
}
