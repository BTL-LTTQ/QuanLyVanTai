using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Giao diện chuẩn cho các cổng thanh toán trực tuyến (Clean OOP / Strategy Pattern)
    /// </summary>
    public interface IPaymentGatewayService
    {
        /// <summary>
        /// Tên cổng thanh toán (VD: VNPay, MoMo)
        /// </summary>
        string GatewayName { get; }

        /// <summary>
        /// Khởi tạo phiên thanh toán và tạo URL / QR Code thanh toán
        /// </summary>
        Task<PaymentCreateResponseDto> CreatePaymentAsync(PaymentCreateRequestDto request);

        /// <summary>
        /// Xử lý dữ liệu callback / redirect trả về từ cổng thanh toán
        /// </summary>
        PaymentCallbackResultDto ProcessCallback(IDictionary<string, string> queryParams);

        /// <summary>
        /// Truy vấn trạng thái giao dịch từ API cổng thanh toán bằng HttpClient
        /// </summary>
        Task<PaymentCallbackResultDto> QueryTransactionStatusAsync(string orderCode, string transactionCode);
    }
}
