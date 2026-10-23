namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// DTO yêu cầu khởi tạo giao dịch thanh toán
    /// </summary>
    public class PaymentCreateRequestDto
    {
        public string Gateway { get; set; } = "VNPay"; // VNPay, MoMo
        public string OrderCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string OrderInfo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public List<int> TicketIds { get; set; } = new();
        public string IpAddress { get; set; } = "127.0.0.1";
    }

    /// <summary>
    /// DTO kết quả khởi tạo giao dịch thanh toán gửi về cho UI
    /// </summary>
    public class PaymentCreateResponseDto
    {
        public bool Success { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
        public string QrCodeContent { get; set; } = string.Empty;
        public string TransactionCode { get; set; } = string.Empty;
        public string OrderCode { get; set; } = string.Empty;
        public string Gateway { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int ResultCode { get; set; }
    }

    /// <summary>
    /// DTO kết quả callback thanh toán nhận từ cổng (VNPay/MoMo)
    /// </summary>
    public class PaymentCallbackResultDto
    {
        public bool Success { get; set; }
        public string Gateway { get; set; } = string.Empty;
        public string OrderCode { get; set; } = string.Empty;
        public string TransactionCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string ResponseCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string RawData { get; set; } = string.Empty;
        public bool IsSignatureValid { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Cấu hình cổng VNPay Sandbox
    /// </summary>
    public class VnPayConfig
    {
        public string TmnCode { get; set; } = "CGXZLS0Z";
        public string HashSecret { get; set; } = "XNBCJFAKAZQSGZSSTGIRQUXZEAZPBTBY";
        public string BaseUrl { get; set; } = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public string QueryUrl { get; set; } = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
        public string ReturnUrl { get; set; } = "http://localhost:8888/payment-callback/vnpay";
        public string Version { get; set; } = "2.1.0";
        public string Command { get; set; } = "pay";
        public string CurrCode { get; set; } = "VND";
        public string Locale { get; set; } = "vn";
    }

    /// <summary>
    /// Cấu hình cổng MoMo Sandbox
    /// </summary>
    public class MoMoConfig
    {
        public string PartnerCode { get; set; } = "MOMOBKUN20180529";
        public string AccessKey { get; set; } = "klm05TvNBzhg7hTa";
        public string SecretKey { get; set; } = "at67qH6mk8w5Y1nAyMoYKMWACiEi2xYv";
        public string BaseUrl { get; set; } = "https://test-payment.momo.vn/v2/gateway/api/create";
        public string QueryUrl { get; set; } = "https://test-payment.momo.vn/v2/gateway/api/query";
        public string ReturnUrl { get; set; } = "http://localhost:8888/payment-callback/momo";
        public string IpnUrl { get; set; } = "http://localhost:8888/payment-callback/momo-ipn";
        public string RequestType { get; set; } = "captureWallet";
    }

    /// <summary>
    /// Cấu hình tổng hợp các cổng thanh toán
    /// </summary>
    public class PaymentGatewaysSettings
    {
        public int CallbackPort { get; set; } = 8888;
        public string CallbackBaseUrl { get; set; } = "http://localhost:8888";
        public VnPayConfig VNPay { get; set; } = new();
        public MoMoConfig MoMo { get; set; } = new();
    }
}
