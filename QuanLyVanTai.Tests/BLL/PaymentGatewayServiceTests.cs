using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;
using Xunit;

namespace QuanLyVanTai.Tests.BLL
{
    public class PaymentGatewayServiceTests
    {
        [Fact]
        public async Task VnPay_CreatePaymentAsync_GeneratesValidUrlWithHmacSha512()
        {
            var config = new VnPayConfig
            {
                TmnCode = "TEST_TMN",
                HashSecret = "TEST_HASH_SECRET_123456",
                BaseUrl = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html",
                ReturnUrl = "http://localhost:8888/payment-callback/vnpay"
            };

            var service = new VnPayGatewayService(config);

            var req = new PaymentCreateRequestDto
            {
                Gateway = "VNPay",
                OrderCode = "ORDER-TK20261017-001",
                Amount = 350000m,
                OrderInfo = "Thanh toan ve xe HN-DN",
                CustomerName = "Nguyen Van Test",
                CustomerPhone = "0912345678",
                IpAddress = "127.0.0.1"
            };

            var res = await service.CreatePaymentAsync(req);

            Assert.True(res.Success);
            Assert.Equal("VNPay", res.Gateway);
            Assert.Contains("vnp_TmnCode=TEST_TMN", res.PaymentUrl);
            Assert.Contains("vnp_Amount=35000000", res.PaymentUrl); // x100
            Assert.Contains("vnp_TxnRef=ORDER-TK20261017-001", res.PaymentUrl);
            Assert.Contains("vnp_SecureHash=", res.PaymentUrl);
            Assert.Equal(res.PaymentUrl, res.QrCodeContent);
        }

        [Fact]
        public void VnPay_ProcessCallback_Success_WhenCode00AndValidChecksum()
        {
            var config = new VnPayConfig
            {
                TmnCode = "TEST_TMN",
                HashSecret = "SECRET_KEY_999"
            };
            var service = new VnPayGatewayService(config);

            string rawToHash = "vnp_Amount=25000000&vnp_OrderInfo=Thanh+toan&vnp_ResponseCode=00&vnp_TmnCode=TEST_TMN&vnp_TransactionNo=14567890&vnp_TxnRef=ORDER123";
            string hash = VnPayGatewayService.ComputeHmacSha512(config.HashSecret, rawToHash);

            var queryParams = new Dictionary<string, string>
            {
                { "vnp_Amount", "25000000" },
                { "vnp_OrderInfo", "Thanh toan" },
                { "vnp_ResponseCode", "00" },
                { "vnp_TmnCode", "TEST_TMN" },
                { "vnp_TransactionNo", "14567890" },
                { "vnp_TxnRef", "ORDER123" },
                { "vnp_SecureHash", hash }
            };

            var result = service.ProcessCallback(queryParams);

            Assert.True(result.Success);
            Assert.True(result.IsSignatureValid);
            Assert.Equal("00", result.ResponseCode);
            Assert.Equal(250000m, result.Amount);
            Assert.Equal("ORDER123", result.OrderCode);
            Assert.Equal("14567890", result.TransactionCode);
        }

        [Fact]
        public void VnPay_ProcessCallback_Failed_WhenResponseCodeIsNot00()
        {
            var config = new VnPayConfig { HashSecret = "SECRET" };
            var service = new VnPayGatewayService(config);

            var queryParams = new Dictionary<string, string>
            {
                { "vnp_Amount", "25000000" },
                { "vnp_ResponseCode", "24" }, // 24 = Khách hủy
                { "vnp_TxnRef", "ORDER999" },
                { "simulated", "true" }
            };

            var result = service.ProcessCallback(queryParams);

            Assert.False(result.Success);
            Assert.Equal("24", result.ResponseCode);
            Assert.Contains("hủy", result.Message.ToLower());
        }

        [Fact]
        public void VnPay_ProcessCallback_RejectsTamperedChecksum()
        {
            var config = new VnPayConfig { HashSecret = "SECRET" };
            var service = new VnPayGatewayService(config);

            var queryParams = new Dictionary<string, string>
            {
                { "vnp_Amount", "25000000" },
                { "vnp_ResponseCode", "00" },
                { "vnp_TxnRef", "ORDER_TAMPER" },
                { "vnp_SecureHash", "INVALID_CHECKSUM_XYZ" }
            };

            var result = service.ProcessCallback(queryParams);

            Assert.False(result.Success);
            Assert.False(result.IsSignatureValid);
            Assert.Contains("không hợp lệ", result.Message);
        }

        [Fact]
        public async Task MoMo_CreatePaymentAsync_BuildsSignatureAndReturnsSession()
        {
            var config = new MoMoConfig
            {
                PartnerCode = "MOMO_TEST",
                AccessKey = "ACCESS_123",
                SecretKey = "SECRET_456"
            };
            var service = new MoMoGatewayService(config);

            var req = new PaymentCreateRequestDto
            {
                Gateway = "MoMo",
                OrderCode = "MOMO-ORDER-001",
                Amount = 450000m,
                OrderInfo = "Ve xe giuong nam"
            };

            var res = await service.CreatePaymentAsync(req);

            Assert.True(res.Success);
            Assert.Equal("MoMo", res.Gateway);
            Assert.False(string.IsNullOrEmpty(res.PaymentUrl));
            Assert.Contains("MOMO-ORDER-001", res.PaymentUrl);
        }

        [Fact]
        public void MoMo_ProcessCallback_Success_WhenResultCodeIs0()
        {
            var config = new MoMoConfig();
            var service = new MoMoGatewayService(config);

            var queryParams = new Dictionary<string, string>
            {
                { "orderId", "MOMO_ORD_777" },
                { "transId", "9988776655" },
                { "resultCode", "0" },
                { "amount", "500000" },
                { "message", "Thành công." },
                { "simulated", "true" }
            };

            var result = service.ProcessCallback(queryParams);

            Assert.True(result.Success);
            Assert.Equal("0", result.ResponseCode);
            Assert.Equal(500000m, result.Amount);
            Assert.Equal("MOMO_ORD_777", result.OrderCode);
            Assert.Equal("9988776655", result.TransactionCode);
        }

        [Fact]
        public void MoMo_ProcessCallback_Failed_WhenResultCodeIsNonZero()
        {
            var config = new MoMoConfig();
            var service = new MoMoGatewayService(config);

            var queryParams = new Dictionary<string, string>
            {
                { "orderId", "MOMO_ORD_888" },
                { "resultCode", "1001" }, // Không đủ số dư
                { "simulated", "true" }
            };

            var result = service.ProcessCallback(queryParams);

            Assert.False(result.Success);
            Assert.Equal("1001", result.ResponseCode);
            Assert.Contains("số dư", result.Message.ToLower());
        }

        [Fact]
        public async Task PaymentService_UpdatePaymentTransactionResultAsync_Success_UpdatesDb()
        {
            using var db = TestDbContextFactory.Create();

            var r = new Route { RouteCode = "HN-HP", RouteName = "Hà Nội - Hải Phòng", DistanceKm = 100, EstimatedHours = 2, BasePrice = 150000, Status = "Active" };
            var v = new Vehicle { LicensePlate = "29B-999.01", VehicleType = "Ghế ngồi", TotalSeats = 29, Manufacturer = "Hyundai", Status = "Ready" };
            db.Routes.Add(r);
            db.Vehicles.Add(v);
            await db.SaveChangesAsync();

            var ticket = new Ticket
            {
                TicketCode = "TK_PAY_001",
                CustomerName = "Khách Test",
                CustomerPhone = "0912345678",
                SeatNumber = "A01",
                Price = 150000,
                DepartureTime = DateTime.UtcNow.AddDays(1),
                PaymentStatus = "Pending",
                TicketStatus = "Booked",
                RouteId = r.Id,
                VehicleId = v.Id
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var paymentService = new PaymentService(db);

            var callbackResult = new PaymentCallbackResultDto
            {
                Success = true,
                Gateway = "VNPay",
                OrderCode = "ORDER-TK_PAY_001",
                TransactionCode = "VNP_123456",
                Amount = 150000m,
                ResponseCode = "00",
                Message = "Giao dịch thành công."
            };

            bool updateSuccess = await paymentService.UpdatePaymentTransactionResultAsync(
                callbackResult,
                new List<int> { ticket.Id },
                username: "Khách Test"
            );

            Assert.True(updateSuccess);

            // Kiểm tra trạng thái vé đã được cập nhật thành Đã thanh toán
            var updatedTicket = await db.Tickets.FindAsync(ticket.Id);
            Assert.NotNull(updatedTicket);
            Assert.Equal("Đã thanh toán", updatedTicket.PaymentStatus);
            Assert.Equal("Booked", updatedTicket.TicketStatus);

            // Kiểm tra bản ghi giao dịch trong bảng PaymentTransactions
            var tx = db.PaymentTransactions.FirstOrDefault(p => p.OrderCode == "ORDER-TK_PAY_001");
            Assert.NotNull(tx);
            Assert.Equal("VNPay", tx.Gateway);
            Assert.Equal("Đã thanh toán", tx.Status);
            Assert.Equal(150000m, tx.Amount);
            Assert.Equal("00", tx.ResponseCode);
        }

        [Fact]
        public async Task PaymentService_UpdatePaymentTransactionResultAsync_Failure_UpdatesToFailedAndCancelled()
        {
            using var db = TestDbContextFactory.Create();

            var r = new Route { RouteCode = "HN-DN", RouteName = "Hà Nội - Đà Nẵng", DistanceKm = 700, EstimatedHours = 12, BasePrice = 400000, Status = "Active" };
            var v = new Vehicle { LicensePlate = "43B-999.02", VehicleType = "Giường nằm", TotalSeats = 40, Manufacturer = "Thaco", Status = "Ready" };
            db.Routes.Add(r);
            db.Vehicles.Add(v);
            await db.SaveChangesAsync();

            var ticket = new Ticket
            {
                TicketCode = "TK_FAIL_001",
                CustomerName = "Khách Hủy",
                CustomerPhone = "0987654321",
                SeatNumber = "B01",
                Price = 400000,
                DepartureTime = DateTime.UtcNow.AddDays(1),
                PaymentStatus = "Pending",
                TicketStatus = "Booked",
                RouteId = r.Id,
                VehicleId = v.Id
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var paymentService = new PaymentService(db);

            var callbackResult = new PaymentCallbackResultDto
            {
                Success = false,
                Gateway = "MoMo",
                OrderCode = "ORDER-FAIL-001",
                TransactionCode = "MOMO_FAIL_99",
                Amount = 400000m,
                ResponseCode = "1000",
                Message = "Khách hàng hủy thanh toán"
            };

            bool updateSuccess = await paymentService.UpdatePaymentTransactionResultAsync(
                callbackResult,
                new List<int> { ticket.Id },
                username: "Khách Hủy"
            );

            Assert.True(updateSuccess);

            var updatedTicket = await db.Tickets.FindAsync(ticket.Id);
            Assert.NotNull(updatedTicket);
            Assert.Equal("Thất bại", updatedTicket.PaymentStatus);
            Assert.Equal("Cancelled", updatedTicket.TicketStatus);
        }

        [Fact]
        public async Task CustomerBookingService_BookTicketsAsync_OnlinePayment_SetsPendingStatus()
        {
            using var db = TestDbContextFactory.Create();

            var r = new Route { RouteCode = "HN-HP-TEST", RouteName = "Hà Nội - Hải Phòng", DistanceKm = 100, EstimatedHours = 2, BasePrice = 180000, Status = "Active" };
            var v = new Vehicle { LicensePlate = "29B-777.88", VehicleType = "Ghế ngồi cao cấp", TotalSeats = 28, Manufacturer = "Hyundai", Status = "Ready" };
            db.Routes.Add(r);
            db.Vehicles.Add(v);
            await db.SaveChangesAsync();

            var bookingService = new CustomerBookingService(db);

            var req = new BookingRequestDto
            {
                CustomerName = "Trần Trực Tuyến",
                CustomerPhone = "0933333333",
                RouteId = r.Id,
                VehicleId = v.Id,
                DepartureTime = DateTime.UtcNow.AddHours(5),
                SelectedSeats = new List<string> { "01", "02" },
                PaymentMethod = "VNPay Sandbox",
                PaymentStatus = "Pending"
            };

            var res = await bookingService.BookTicketsAsync(req);

            Assert.True(res.Success);
            Assert.Equal(2, res.CreatedTicketIds.Count);

            var tickets = db.Tickets.Where(t => res.CreatedTicketIds.Contains(t.Id)).ToList();
            Assert.All(tickets, t =>
            {
                Assert.Equal("Pending", t.PaymentStatus);
                Assert.Equal("Booked", t.TicketStatus);
            });
        }
    }
}
