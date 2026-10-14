using System.Drawing;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using Xunit;

namespace QuanLyVanTai.Tests.BLL
{
    public class CustomerBookingAndSeatMapTests
    {
        [Theory]
        [InlineData("Ghế ngồi 28 chỗ", 28, VehicleLayoutType.Seater28)]
        [InlineData("Ghế ngồi 45 chỗ", 45, VehicleLayoutType.Seater45)]
        [InlineData("Limousine 9 chỗ", 9, VehicleLayoutType.Seater9)]
        [InlineData("Giường nằm 34 chỗ", 34, VehicleLayoutType.Sleeper34)]
        [InlineData("Giường nằm 24 chỗ", 24, VehicleLayoutType.Sleeper24)]
        public void DetermineLayoutType_ReturnsCorrectLayoutType(string vehicleType, int totalSeats, VehicleLayoutType expected)
        {
            var actual = SeatMapService.DetermineLayoutType(vehicleType, totalSeats);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GenerateSeatTemplate_Seater28_GeneratesExpectedSeatCount()
        {
            var seats = SeatMapService.GenerateSeatTemplate(VehicleLayoutType.Seater28, 28, 150000);
            Assert.Equal(28, seats.Count);
            Assert.All(seats, s => Assert.Equal(1, s.Floor));
            Assert.Contains(seats, s => s.SeatCode == "01");
        }

        [Fact]
        public void GenerateSeatTemplate_Seater9_GeneratesExpectedSeats()
        {
            var seats = SeatMapService.GenerateSeatTemplate(VehicleLayoutType.Seater9, 9, 250000);
            Assert.Equal(9, seats.Count);
            Assert.Contains(seats, s => s.SeatCode == "01");
        }

        [Fact]
        public void GenerateSeatTemplate_Sleeper34_GeneratesTwoFloorsAAndB()
        {
            var seats = SeatMapService.GenerateSeatTemplate(VehicleLayoutType.Sleeper34, 34, 350000);
            Assert.Equal(34, seats.Count);

            var floor1 = seats.Where(s => s.Floor == 1).ToList();
            var floor2 = seats.Where(s => s.Floor == 2).ToList();

            Assert.Equal(17, floor1.Count);
            Assert.Equal(17, floor2.Count);

            Assert.All(floor1, s => Assert.StartsWith("A", s.SeatCode));
            Assert.All(floor2, s => Assert.StartsWith("B", s.SeatCode));
        }

        [Fact]
        public void GenerateSeatTemplate_Sleeper24_GeneratesTwoFloorsAndCabins()
        {
            var seats = SeatMapService.GenerateSeatTemplate(VehicleLayoutType.Sleeper24, 24, 450000);
            Assert.Equal(24, seats.Count);

            var floor1 = seats.Where(s => s.Floor == 1).ToList();
            var floor2 = seats.Where(s => s.Floor == 2).ToList();

            Assert.Equal(12, floor1.Count);
            Assert.Equal(12, floor2.Count);

            Assert.All(seats, s => Assert.StartsWith("P", s.SeatCode));
            Assert.Contains(seats, s => s.SeatCode == "P01");
            Assert.Contains(seats, s => s.SeatCode == "P24");
        }

        [Theory]
        [InlineData("TET2026", 1000000, true, 200000, 800000)]
        [InlineData("VIP10", 500000, true, 50000, 450000)]
        [InlineData("GIAM50K", 300000, true, 50000, 250000)]
        [InlineData("CHAOBANMOI", 200000, true, 30000, 170000)]
        [InlineData("INVALID_CODE", 500000, false, 0, 500000)]
        [InlineData("", 500000, false, 0, 500000)]
        public async Task VerifyPromotionAsync_CalculatesCorrectDiscount(
            string code, decimal subTotal, bool expectedValid, decimal expectedDiscount, decimal expectedFinal)
        {
            var service = new CustomerBookingService();
            var result = await service.VerifyPromotionAsync(code, subTotal);

            Assert.Equal(expectedValid, result.IsValid);
            Assert.Equal(expectedDiscount, result.DiscountAmount);
            Assert.Equal(expectedFinal, result.FinalTotal);
        }

        [Fact]
        public void GenerateQrCode_ReturnsValidBitmap()
        {
            string sampleData = "QLVT|MAVE:TK20261010001|TUYEN:HN-DN|GHE:A01|GIA:350000";
            using var bitmap = TicketPdfExportService.GenerateQrCode(sampleData, 3);

            Assert.NotNull(bitmap);
            Assert.True(bitmap.Width > 50);
            Assert.True(bitmap.Height > 50);
        }

        [Fact]
        public void DrawTicket_RendersWithoutException()
        {
            var ticket = new TicketReceiptDto
            {
                TicketCode = "TK20261010001",
                CustomerName = "Nguyễn Văn Test",
                CustomerPhone = "0912345678",
                RouteName = "Hà Nội - Đà Nẵng",
                RouteCode = "HN-DN-01",
                LicensePlate = "29B-123.45",
                VehicleType = "Giường nằm 40 chỗ",
                SeatNumber = "A01",
                Floor = 1,
                DepartureTime = DateTime.Now.AddDays(1),
                OriginalPrice = 350000,
                FinalPrice = 350000,
                PaymentStatus = "Paid",
                QrCodeContent = "QLVT|TEST"
            };

            using var bmp = new Bitmap(800, 600);
            using var g = Graphics.FromImage(bmp);

            var ex = Record.Exception(() =>
            {
                TicketPdfExportService.DrawTicket(g, ticket, new Rectangle(0, 0, bmp.Width, bmp.Height));
            });

            Assert.Null(ex);
        }

        [Fact]
        public async Task RegisterAsync_CreatesAccountWithCustomerRole()
        {
            using var db = QuanLyVanTai.Tests.Helpers.TestDbContextFactory.Create();
            var authService = new AuthService(db);

            var req = new RegisterRequestDto
            {
                Username = "khachhang_test_" + Guid.NewGuid().ToString("N")[..8],
                Password = "Password123@",
                ConfirmPassword = "Password123@",
                FullName = "Nguyễn Khách Hàng",
                Email = "khachhang@test.com",
                PhoneNumber = "0912345678"
            };

            var result = await authService.RegisterAsync(req);

            Assert.True(result.Success);
            var account = await db.Accounts.FindAsync(result.CreatedAccountId);
            Assert.NotNull(account);
            Assert.Equal(Core.Security.SystemRoles.Customer, account.Role);
            Assert.Equal("Khách hàng", account.Role);
        }
    }
}
