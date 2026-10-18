using System.Net;
using System.Text;
using System.Text.Json;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Lightweight HTTP Listener server chạy ngầm trên máy Desktop để lắng nghe callback thanh toán từ VNPay / MoMo
    /// </summary>
    public class PaymentCallbackServer : IDisposable
    {
        private HttpListener? _listener;
        private CancellationTokenSource? _cts;
        private bool _isRunning;
        private readonly int _port;
        private readonly VnPayGatewayService _vnPayService;
        private readonly MoMoGatewayService _moMoService;

        public static event Action<PaymentCallbackResultDto>? OnCallbackReceived;

        public bool IsRunning => _isRunning;
        public int Port => _port;

        public PaymentCallbackServer(int port = 8888, VnPayConfig? vnPayConfig = null, MoMoConfig? moMoConfig = null)
        {
            _port = port;
            _vnPayService = new VnPayGatewayService(vnPayConfig);
            _moMoService = new MoMoGatewayService(moMoConfig);
        }

        public void Start()
        {
            if (_isRunning) return;

            try
            {
                _listener = new HttpListener();
                _listener.Prefixes.Add($"http://localhost:{_port}/payment-callback/");
                _listener.Prefixes.Add($"http://127.0.0.1:{_port}/payment-callback/");
                _listener.Start();

                _cts = new CancellationTokenSource();
                _isRunning = true;

                Task.Run(() => ListenLoopAsync(_cts.Token));
            }
            catch (Exception ex)
            {
                _isRunning = false;
                System.Diagnostics.Debug.WriteLine($"[PaymentCallbackServer] Không thể khởi động listener trên cổng {_port}: {ex.Message}");
            }
        }

        private async Task ListenLoopAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested && _listener != null && _listener.IsListening)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequestAsync(context), token);
                }
                catch (HttpListenerException)
                {
                    // Listener bị đóng khi Stop()
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[PaymentCallbackServer] Lỗi nhận request: {ex.Message}");
                }
            }
        }

        private async Task HandleRequestAsync(HttpListenerContext context)
        {
            try
            {
                var request = context.Request;
                var response = context.Response;

                if (request.Url == null || request.Url.AbsolutePath.Contains("favicon.ico"))
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Close();
                    return;
                }

                var queryParams = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // 1. Đọc dữ liệu từ QueryString (GET redirect)
                foreach (string? key in request.QueryString.AllKeys)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        queryParams[key] = request.QueryString[key] ?? "";
                    }
                }

                // 2. Đọc dữ liệu từ Body nếu là POST (IPN callback)
                if (request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) && request.HasEntityBody)
                {
                    using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                    string body = await reader.ReadToEndAsync();

                    if (!string.IsNullOrWhiteSpace(body))
                    {
                        if (body.TrimStart().StartsWith("{"))
                        {
                            try
                            {
                                using var doc = JsonDocument.Parse(body);
                                foreach (var prop in doc.RootElement.EnumerateObject())
                                {
                                    queryParams[prop.Name] = prop.Value.ToString();
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            var parts = body.Split('&');
                            foreach (var part in parts)
                            {
                                var kv = part.Split('=');
                                if (kv.Length == 2)
                                {
                                    queryParams[WebUtility.UrlDecode(kv[0])] = WebUtility.UrlDecode(kv[1]);
                                }
                            }
                        }
                    }
                }

                // 3. Phân định cổng thanh toán (VNPay hay MoMo)
                string gateway = "VNPay";
                if (queryParams.ContainsKey("resultCode") || queryParams.ContainsKey("partnerCode") ||
                    (request.Url?.AbsolutePath.Contains("momo", StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    gateway = "MoMo";
                }

                // 4. Gọi Service tương ứng xử lý
                PaymentCallbackResultDto callbackResult;
                if (gateway == "MoMo")
                {
                    callbackResult = _moMoService.ProcessCallback(queryParams);
                }
                else
                {
                    callbackResult = _vnPayService.ProcessCallback(queryParams);
                }

                // 5. Tạo trang HTML phản hồi đẹp mắt cho người dùng trên trình duyệt
                string htmlResponse = GenerateHtmlResultPage(callbackResult);
                byte[] buffer = Encoding.UTF8.GetBytes(htmlResponse);

                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength64 = buffer.Length;
                response.StatusCode = (int)HttpStatusCode.OK;

                await response.OutputStream.WriteAsync(buffer);
                response.OutputStream.Close();

                // 6. Kích hoạt sự kiện thông báo cho ứng dụng WinForms
                OnCallbackReceived?.Invoke(callbackResult);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PaymentCallbackServer] Lỗi xử lý callback: {ex.Message}");
            }
        }

        private static string GenerateHtmlResultPage(PaymentCallbackResultDto result)
        {
            string statusIcon = result.Success ? "✅" : "❌";
            string title = result.Success ? "THANH TOÁN THÀNH CÔNG!" : "THANH TOÁN THẤT BẠI!";
            string badgeColor = result.Success ? "#10B981" : "#EF4444";
            string badgeBg = result.Success ? "#ECFDF5" : "#FEF2F2";
            string statusText = result.Success ? "ĐÃ THANH TOÁN THÀNH CÔNG" : "GIAO DỊCH KHÔNG THÀNH CÔNG";
            string desc = result.Success
                ? "Giao dịch thanh toán trực tuyến Sandbox đã được hệ thống ghi nhận thành công."
                : $"Lý do: {result.Message}";

            return $@"<!DOCTYPE html>
<html lang=""vi"">
<head>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{title} - Quản Lý Vận Tải</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #F1F5F9 0%, #E2E8F0 100%);
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            margin: 0;
            padding: 20px;
        }}
        .card {{
            background: #ffffff;
            border-radius: 16px;
            box-shadow: 0 10px 25px -5px rgba(0, 0, 0, 0.1), 0 8px 10px -6px rgba(0, 0, 0, 0.1);
            max-width: 500px;
            width: 100%;
            padding: 36px 30px;
            text-align: center;
            box-sizing: border-box;
        }}
        .icon {{
            font-size: 64px;
            margin-bottom: 16px;
        }}
        h1 {{
            color: #0F172A;
            font-size: 22px;
            margin: 0 0 10px 0;
            font-weight: 700;
        }}
        .badge {{
            display: inline-block;
            background: {badgeBg};
            color: {badgeColor};
            padding: 6px 14px;
            border-radius: 20px;
            font-size: 13px;
            font-weight: 600;
            margin-bottom: 20px;
            border: 1px solid {badgeColor}40;
        }}
        .info-table {{
            width: 100%;
            background: #F8FAFC;
            border-radius: 10px;
            border: 1px solid #E2E8F0;
            margin-bottom: 24px;
            padding: 12px 16px;
            text-align: left;
            box-sizing: border-box;
        }}
        .info-row {{
            display: flex;
            justify-content: space-between;
            padding: 8px 0;
            border-bottom: 1px dashed #CBD5E1;
            font-size: 14px;
        }}
        .info-row:last-child {{
            border-bottom: none;
        }}
        .info-label {{
            color: #64748B;
        }}
        .info-value {{
            font-weight: 600;
            color: #1E293B;
        }}
        .amount-highlight {{
            color: #2563EB;
            font-size: 16px;
            font-weight: 700;
        }}
        p.desc {{
            color: #475569;
            font-size: 14px;
            line-height: 1.5;
            margin: 0 0 24px 0;
        }}
        .btn {{
            display: inline-block;
            background: #2563EB;
            color: #ffffff;
            padding: 12px 28px;
            border-radius: 8px;
            text-decoration: none;
            font-weight: 600;
            font-size: 14px;
            cursor: pointer;
            border: none;
            transition: background 0.2s;
        }}
        .btn:hover {{
            background: #1D4ED8;
        }}
        .footer-hint {{
            margin-top: 18px;
            font-size: 12px;
            color: #94A3B8;
        }}
    </style>
</head>
<body>
    <div class=""card"">
        <div class=""icon"">{statusIcon}</div>
        <h1>{title}</h1>
        <div class=""badge"">{statusText}</div>

        <div class=""info-table"">
            <div class=""info-row"">
                <span class=""info-label"">Cổng thanh toán:</span>
                <span class=""info-value"">{result.Gateway} Sandbox</span>
            </div>
            <div class=""info-row"">
                <span class=""info-label"">Mã đơn hàng:</span>
                <span class=""info-value"">{result.OrderCode}</span>
            </div>
            <div class=""info-row"">
                <span class=""info-label"">Mã giao dịch:</span>
                <span class=""info-value"">{result.TransactionCode}</span>
            </div>
            <div class=""info-row"">
                <span class=""info-label"">Số tiền thanh toán:</span>
                <span class=""info-value amount-highlight"">{result.Amount:N0} VNĐ</span>
            </div>
            <div class=""info-row"">
                <span class=""info-label"">Thời gian:</span>
                <span class=""info-value"">{result.TransactionDate:dd/MM/yyyy HH:mm:ss}</span>
            </div>
        </div>

        <p class=""desc"">{desc}</p>
        <button class=""btn"" onclick=""window.close();"">Đóng Cửa Sổ Trình Duyệt</button>
        <div class=""footer-hint"">Quý khách vui lòng quay lại phần mềm <strong>Quản Lý Vận Tải</strong> để in thẻ lên xe.</div>
    </div>
</body>
</html>";
        }

        public void Stop()
        {
            _isRunning = false;
            _cts?.Cancel();

            try
            {
                if (_listener != null && _listener.IsListening)
                {
                    _listener.Stop();
                    _listener.Close();
                }
            }
            catch { }
            finally
            {
                _listener = null;
            }
        }

        public void Dispose()
        {
            Stop();
            _cts?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
