using System.Drawing.Drawing2D;
using Core.Configs;
using Core.Logging;
using FontAwesome.Sharp;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL;
using QuanLyVanTai.UI.Utils;

namespace QuanLyVanTai.UI.UserControls
{
    /// <summary>
    /// Widget AI Chatbot nổi ở góc phải màn hình.
    /// Kiến trúc: UserControl chứa FAB toggle button + panel chat (ẩn/hiện khi bấm FAB).
    /// </summary>
    public class UcAiChatbot : UserControl
    {
        // === Event thông báo trạng thái đóng/mở cho Form cha ===
        public event EventHandler? ChatToggled;

        // === Services ===
        private IAiChatbotService? _chatbotService;
        private readonly List<ChatMessageDto> _history = new();
        private CancellationTokenSource? _cts;

        // === Kích thước cố định ===
        private const int FabSize = 56;
        private const int ChatW = 360;
        private const int ChatH = 430;
        private const int ChatSpacing = 6;

        // === UI Controls ===
        private Panel pnlChat = null!;
        private Panel pnlHeader = null!;
        private FlowLayoutPanel flowMsgs = null!;
        private FlowLayoutPanel pnlSuggestions = null!;
        private Panel pnlInput = null!;
        private TextBox txtInput = null!;
        private IconButton btnSend = null!;
        private IconButton btnFab = null!;
        private Label lblStatus = null!;
        private Label lblTyping = null!;
        private ProgressBar pbLoading = null!;
        private ToolTip toolTip = null!;

        private bool _isOpen = false;
        private bool _waiting = false;
        private string? _context;
        private System.Windows.Forms.Timer? _dotTimer;
        private int _dotCount = 0;

        public bool IsOpen => _isOpen;

        public UcAiChatbot()
        {
            this.BackColor = Color.Transparent;
            toolTip = new ToolTip();

            BuildUI();

            // Mặc định ban đầu ở trạng thái thu gọn (chỉ hiển thị nút FAB)
            UpdateLayoutState(false);

            Task.Run(InitServiceAsync);
        }

        // ===================================================================
        // XÂY DỰNG GIAO DIỆN
        // ===================================================================
        private void BuildUI()
        {
            // ---------- FAB (Floating Action Button) ----------
            btnFab = new IconButton
            {
                Size = new Size(FabSize, FabSize),
                IconChar = IconChar.CommentDots,
                IconColor = Color.White,
                IconSize = 28,
                BackColor = Color.FromArgb(37, 99, 235), // Xanh dương thương hiệu
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Text = string.Empty,
                TabStop = false
            };
            btnFab.FlatAppearance.BorderSize = 0;
            btnFab.FlatAppearance.MouseOverBackColor = Color.FromArgb(29, 78, 216);
            btnFab.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 64, 175);

            ApplyCircleRegion(btnFab);
            btnFab.SizeChanged += (s, e) => ApplyCircleRegion(btnFab);
            btnFab.Click += (s, e) => ToggleChat();

            toolTip.SetToolTip(btnFab, "Chatbot AI - Tư vấn lịch trình & giá vé nhà xe");
            this.Controls.Add(btnFab);

            // ---------- Panel Chat ----------
            pnlChat = new Panel
            {
                Size = new Size(ChatW, ChatH),
                Location = new Point(0, 0),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Visible = false
            };

            // Viền panel chat
            pnlChat.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(203, 213, 225), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlChat.Width - 1, pnlChat.Height - 1);
            };

            // --- Header ---
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 56,
                BackColor = Color.FromArgb(30, 58, 138) // Navy đậm sang trọng
            };

            var picRobot = new IconPictureBox
            {
                IconChar = IconChar.Robot,
                IconColor = Color.White,
                IconSize = 26,
                Size = new Size(32, 32),
                Location = new Point(12, 12),
                BackColor = Color.Transparent
            };

            var lblTitle = new Label
            {
                Text = "Trợ Lý Ảo VanTai",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(48, 8)
            };

            lblStatus = new Label
            {
                Text = "● Đang chuẩn bị...",
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(186, 230, 253),
                AutoSize = true,
                Location = new Point(50, 30)
            };

            var btnClose = new IconButton
            {
                IconChar = IconChar.Xmark,
                IconColor = Color.White,
                IconSize = 16,
                Size = new Size(28, 28),
                Location = new Point(ChatW - 36, 14),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Text = string.Empty
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
            btnClose.Click += (s, e) => ToggleChat();
            toolTip.SetToolTip(btnClose, "Đóng khung chat");

            var btnClear = new IconButton
            {
                IconChar = IconChar.RotateLeft,
                IconColor = Color.White,
                IconSize = 14,
                Size = new Size(28, 28),
                Location = new Point(ChatW - 68, 14),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Text = string.Empty
            };
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatAppearance.MouseOverBackColor = Color.FromArgb(40, 255, 255, 255);
            btnClear.Click += ClearHistory;
            toolTip.SetToolTip(btnClear, "Xóa lịch sử hội thoại");

            pnlHeader.Controls.AddRange(new Control[] { picRobot, lblTitle, lblStatus, btnClose, btnClear });

            // --- Vùng gợi ý câu hỏi nhanh (Suggestion Chips) ---
            pnlSuggestions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 36,
                BackColor = Color.FromArgb(241, 245, 249),
                Padding = new Padding(6, 4, 6, 2),
                WrapContents = false,
                AutoScroll = true
            };
            AddSuggestionChip("🎫 Giá vé");
            AddSuggestionChip("🕐 Lịch trình");
            AddSuggestionChip("📍 Điểm đón");
            AddSuggestionChip("🚌 Loại xe");

            // --- Vùng hiển thị tin nhắn ---
            flowMsgs = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 250, 252),
                Padding = new Padding(8, 6, 8, 6)
            };

            var pnlBody = new Panel
            {
                Location = new Point(1, 56),
                Size = new Size(ChatW - 2, ChatH - 56 - 54),
                BackColor = Color.FromArgb(248, 250, 252)
            };
            pnlBody.Controls.Add(flowMsgs);
            pnlBody.Controls.Add(pnlSuggestions);

            // Typing label & Progress bar
            pbLoading = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                MarqueeAnimationSpeed = 25,
                Location = new Point(1, ChatH - 56),
                Size = new Size(ChatW - 2, 2),
                Visible = false
            };

            lblTyping = new Label
            {
                Text = string.Empty,
                Font = new Font("Segoe UI", 8F, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(10, ChatH - 54 - 18),
                Size = new Size(240, 16),
                Visible = false
            };

            // --- Khu vực nhập tin nhắn ---
            pnlInput = new Panel
            {
                Location = new Point(1, ChatH - 54),
                Size = new Size(ChatW - 2, 53),
                BackColor = Color.White
            };

            var pnlInputDivider = new Panel
            {
                Dock = DockStyle.Top,
                Height = 1,
                BackColor = Color.FromArgb(226, 232, 240)
            };

            txtInput = new TextBox
            {
                Location = new Point(8, 8),
                Size = new Size(ChatW - 60, 36),
                Font = new Font("Segoe UI", 9.5F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 250, 252),
                PlaceholderText = "Nhập câu hỏi... (Enter gửi)"
            };
            txtInput.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    _ = SendAsync();
                }
            };

            btnSend = new IconButton
            {
                Location = new Point(ChatW - 48, 8),
                Size = new Size(38, 36),
                IconChar = IconChar.PaperPlane,
                IconColor = Color.White,
                IconSize = 16,
                BackColor = Color.FromArgb(37, 99, 235),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Text = string.Empty
            };
            btnSend.FlatAppearance.BorderSize = 0;
            btnSend.Click += async (s, e) => await SendAsync();
            toolTip.SetToolTip(btnSend, "Gửi tin nhắn");

            pnlInput.Controls.AddRange(new Control[] { pnlInputDivider, txtInput, btnSend });

            pnlChat.Controls.AddRange(new Control[] { pnlHeader, pnlBody, pbLoading, lblTyping, pnlInput });
            this.Controls.Add(pnlChat);
        }

        private void AddSuggestionChip(string text)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
                Height = 26,
                Font = new Font("Segoe UI", 8F),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 58, 138),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Margin = new Padding(2, 1, 4, 1)
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 246, 255);
            btn.Click += (s, e) =>
            {
                txtInput.Text = text.Substring(2).Trim(); // Bỏ icon ở đầu
                _ = SendAsync();
            };
            pnlSuggestions.Controls.Add(btn);
        }

        // ===================================================================
        // KHỞI TẠO SERVICE BẤT ĐỒNG BỘ
        // ===================================================================
        private async Task InitServiceAsync()
        {
            try
            {
                var config = ConfigReader.GetAiChatbotConfig();
                _chatbotService = new AiChatbotService(config);
                _context = await BuildContextAsync();

                if (this.IsHandleCreated)
                {
                    this.Invoke(() =>
                    {
                        bool isOffline = string.IsNullOrEmpty(config.ApiKey) || config.ApiKey.StartsWith("YOUR_");
                        lblStatus.Text = isOffline ? "● Sẵn sàng (Ngoại tuyến)" : $"● Sẵn sàng ({config.Provider})";
                        lblStatus.ForeColor = isOffline ? Color.FromArgb(253, 186, 116) : Color.FromArgb(134, 239, 172);
                    });
                }
            }
            catch (Exception ex)
            {
                LoggerService.Warning("UcAiChatbot: Init service thất bại - {Msg}", ex.Message);
                _chatbotService = new AiChatbotService(new AiChatbotConfig { Provider = "Gemini", ApiKey = "offline" });
                if (this.IsHandleCreated)
                {
                    this.Invoke(() =>
                    {
                        lblStatus.Text = "● Ngoại tuyến";
                        lblStatus.ForeColor = Color.FromArgb(253, 186, 116);
                    });
                }
            }
        }

        private static async Task<string> BuildContextAsync()
        {
            try
            {
                using var db = new AppDbContext();
                var sb = new System.Text.StringBuilder();

                var routes = await db.Routes
                    .Where(r => r.Status == "Active")
                    .Take(8).ToListAsync();
                if (routes.Any())
                {
                    sb.AppendLine("TUYẾN XE HIỆN CÓ CỦA NHÀ XE:");
                    foreach (var r in routes)
                        sb.AppendLine($"  - Tuyến {r.RouteCode}: {r.RouteName} | {r.DistanceKm}km | Thời gian: ~{r.EstimatedHours}h | Giá vé từ: {r.BasePrice:N0}đ");
                }

                var stations = await db.Stations
                    .Where(s => s.Status == "Active").Take(6).ToListAsync();
                if (stations.Any())
                {
                    sb.AppendLine("CÁC TRẠM/BẾN XE ĐÓN TRẢ KHÁCH:");
                    foreach (var s in stations)
                        sb.AppendLine($"  - {s.StationName} ({s.City}): {s.Address}");
                }

                return sb.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        // ===================================================================
        // MỞ / ĐÓNG PANEL CHAT & CẬP NHẬT KÍCH THƯỚC WIDGET
        // ===================================================================
        public void ToggleChat()
        {
            _isOpen = !_isOpen;
            UpdateLayoutState(_isOpen);
            ChatToggled?.Invoke(this, EventArgs.Empty);

            if (_isOpen)
            {
                if (flowMsgs.Controls.Count == 0)
                {
                    AddBotMessage(
                        "👋 Xin chào! Tôi là **Trợ lý ảo VanTai Express**.\n\n" +
                        "Tôi có thể giải đáp ngay về:\n" +
                        "🎫 Giá vé các tuyến & chương trình ưu đãi\n" +
                        "🕐 Lịch trình xe chạy & giờ khởi hành\n" +
                        "📍 Bến xe & điểm đón trả khách\n" +
                        "🚌 Tiện ích loại xe (Giường nằm, Limousine VIP)\n\n" +
                        "Bạn cần nhà xe hỗ trợ thông tin gì ạ?");
                }
                txtInput.Focus();
                ScrollBottom();
            }
        }

        private void UpdateLayoutState(bool open)
        {
            pnlChat.Visible = open;

            if (open)
            {
                // Khi mở: Widget phóng to để chứa khung chat + nút FAB bên dưới
                this.Size = new Size(ChatW, ChatH + FabSize + 8);
                pnlChat.Location = new Point(0, 0);
                btnFab.Location = new Point(ChatW - FabSize - 2, ChatH + 4);
                btnFab.IconChar = IconChar.ChevronDown;
                toolTip.SetToolTip(btnFab, "Thu nhỏ khung chat");
            }
            else
            {
                // Khi đóng: Widget thu nhỏ vừa khít bằng kích thước nút tròn FAB
                this.Size = new Size(FabSize + 4, FabSize + 4);
                btnFab.Location = new Point(2, 2);
                btnFab.IconChar = IconChar.CommentDots;
                toolTip.SetToolTip(btnFab, "Chatbot AI - Tư vấn lịch trình & giá vé nhà xe");
            }
        }

        // ===================================================================
        // GỬI TIN NHẮN
        // ===================================================================
        private async Task SendAsync()
        {
            if (_waiting || _chatbotService == null) return;
            string msg = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(msg)) return;

            txtInput.Clear();
            AddUserMessage(msg);
            _history.Add(new ChatMessageDto { Role = "user", Content = msg });
            if (_history.Count > 20) _history.RemoveAt(0);

            SetWaiting(true);
            try
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                var resp = await _chatbotService.SendMessageAsync(
                    msg, new List<ChatMessageDto>(_history), _context, _cts.Token);

                _history.Add(new ChatMessageDto
                {
                    Role = resp.ProviderUsed.Contains("OpenAI") ? "assistant" : "model",
                    Content = resp.ResponseText
                });

                AddBotMessage(resp.ResponseText, resp.ProviderUsed);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                LoggerService.Error(ex, "UcAiChatbot: Lỗi gửi tin nhắn.");
                AddBotMessage("⚠ Có lỗi kết nối tới hệ thống AI. Vui lòng thử lại sau.", isError: true);
            }
            finally
            {
                SetWaiting(false);
            }
        }

        // ===================================================================
        // HIỂN THỊ BUBBLE TIN NHẮN
        // ===================================================================
        private void AddUserMessage(string text) => AddBubble(text, isUser: true);

        private void AddBotMessage(string text, string? provider = null, bool isError = false)
            => AddBubble(text, isUser: false, provider: provider, isError: isError);

        private void AddBubble(string text, bool isUser, string? provider = null, bool isError = false)
        {
            if (InvokeRequired)
            {
                Invoke(() => AddBubble(text, isUser, provider, isError));
                return;
            }

            int containerWidth = flowMsgs.Width - flowMsgs.Padding.Horizontal - 24;
            if (containerWidth < 200) containerWidth = 310;
            int bubbleMax = (int)(containerWidth * 0.82);

            var bubble = new Panel
            {
                BackColor = isUser
                    ? (isError ? Color.FromArgb(254, 226, 226) : Color.FromArgb(37, 99, 235))
                    : (isError ? Color.FromArgb(254, 226, 226) : Color.White),
                Padding = new Padding(10, 8, 10, 8),
                Margin = new Padding(0, 2, 0, 2),
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 9.25F, FontStyle.Regular),
                ForeColor = isUser && !isError ? Color.White : Color.FromArgb(30, 41, 59),
                BackColor = Color.Transparent,
                AutoSize = true,
                MaximumSize = new Size(bubbleMax, 0)
            };
            bubble.Controls.Add(lbl);

            // Viền nhẹ cho bot message
            if (!isUser)
            {
                bubble.Paint += (s, e) =>
                {
                    using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, bubble.Width - 1, bubble.Height - 1);
                };
            }

            var row = new FlowLayoutPanel
            {
                Width = containerWidth,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = isUser ? FlowDirection.RightToLeft : FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0),
                Margin = new Padding(0, 3, 0, 3),
                BackColor = Color.Transparent
            };
            row.Controls.Add(bubble);

            flowMsgs.Controls.Add(row);

            // Tag hiển thị nguồn trả lời (Gemini / OpenAI)
            if (!isUser && !string.IsNullOrEmpty(provider) && !provider.Contains("Offline") && !provider.Contains("Fallback"))
            {
                var lblTag = new Label
                {
                    Text = $"✨ {provider}",
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    AutoSize = true,
                    Margin = new Padding(4, 0, 0, 4)
                };
                flowMsgs.Controls.Add(lblTag);
            }

            ScrollBottom();
        }

        private void ClearHistory(object? s, EventArgs e)
        {
            flowMsgs.Controls.Clear();
            _history.Clear();
            AddBotMessage("🔄 Cuộc hội thoại đã được đặt lại. Bạn cần nhà xe tư vấn gì ạ?");
        }

        // ===================================================================
        // LOADING & TYPING INDICATOR
        // ===================================================================
        private void SetWaiting(bool on)
        {
            if (InvokeRequired) { Invoke(() => SetWaiting(on)); return; }
            _waiting = on;
            btnSend.Enabled = !on;
            txtInput.Enabled = !on;
            pbLoading.Visible = on;
            lblTyping.Visible = on;
            if (on) { _dotCount = 0; StartDotTimer(); }
            else { StopDotTimer(); lblTyping.Text = string.Empty; }
        }

        private void StartDotTimer()
        {
            _dotTimer?.Dispose();
            _dotTimer = new System.Windows.Forms.Timer { Interval = 400 };
            _dotTimer.Tick += (s, e) =>
            {
                _dotCount = (_dotCount % 3) + 1;
                lblTyping.Text = "Trợ lý đang soạn câu trả lời" + new string('.', _dotCount);
            };
            _dotTimer.Start();
        }

        private void StopDotTimer()
        {
            _dotTimer?.Stop();
            _dotTimer?.Dispose();
            _dotTimer = null;
        }

        private void ScrollBottom()
        {
            if (flowMsgs.Controls.Count > 0)
            {
                flowMsgs.ScrollControlIntoView(flowMsgs.Controls[flowMsgs.Controls.Count - 1]);
            }
        }

        private static void ApplyCircleRegion(Control c)
        {
            using var path = new GraphicsPath();
            path.AddEllipse(0, 0, c.Width, c.Height);
            c.Region = new Region(path);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cts?.Cancel();
                _cts?.Dispose();
                _dotTimer?.Dispose();
                toolTip?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
