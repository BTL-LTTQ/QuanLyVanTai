using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;
using Xunit;

namespace QuanLyVanTai.Tests.BLL
{
    public class PassengerCheckInServiceTests
    {
        [Fact]
        public void ParseTicketCode_WithStandardPayload_ExtractsTicketCode()
        {
            var service = new PassengerCheckInService();
            string qrPayload = "QLVT|MAVE:TK20261017001|TUYEN:HN-DN|GHE:A01|GIA:350000";

            string code = service.ParseTicketCode(qrPayload);

            Assert.Equal("TK20261017001", code);
        }

        [Fact]
        public void ParseTicketCode_WithEqualFormat_ExtractsTicketCode()
        {
            var service = new PassengerCheckInService();
            string qrPayload = "MAVE=TK20261017999;ROUTE=HN-DN";

            string code = service.ParseTicketCode(qrPayload);

            Assert.Equal("TK20261017999", code);
        }

        [Fact]
        public void ParseTicketCode_WithDirectTicketCode_ReturnsUppercaseCode()
        {
            var service = new PassengerCheckInService();
            string code = service.ParseTicketCode("tk20261017005");

            Assert.Equal("TK20261017005", code);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ParseTicketCode_WithEmptyInput_ReturnsEmptyString(string? input)
        {
            var service = new PassengerCheckInService();
            string code = service.ParseTicketCode(input);

            Assert.Equal(string.Empty, code);
        }

        [Fact]
        public void DecodeQrFromBitmap_WithValidQrImage_ReturnsOriginalText()
        {
            var service = new PassengerCheckInService();
            string originalText = "QLVT|MAVE:TK20261017001|TUYEN:HN-DN|GHE:A01";

            using var bmp = TicketPdfExportService.GenerateQrCode(originalText, 4);
            Assert.NotNull(bmp);

            string? decoded = service.DecodeQrFromBitmap(bmp);

            Assert.Equal(originalText, decoded);
        }

        [Fact]
        public void DecodeQrFromBitmap_WithNullBitmap_ReturnsNull()
        {
            var service = new PassengerCheckInService();
            string? decoded = service.DecodeQrFromBitmap(null!);

            Assert.Null(decoded);
        }

        [Fact]
        public void DecodeQrFromFile_WithValidFile_DecodesSuccessfully()
        {
            var service = new PassengerCheckInService();
            string payload = "QLVT|MAVE:TK20261017888|TUYEN:HN-HP|GHE:B05";
            string tempFile = Path.Combine(Path.GetTempPath(), $"qr_test_{Guid.NewGuid():N}.png");

            try
            {
                using (var bmp = TicketPdfExportService.GenerateQrCode(payload, 4))
                {
                    bmp.Save(tempFile, System.Drawing.Imaging.ImageFormat.Png);
                }

                string? decoded = service.DecodeQrFromFile(tempFile);
                Assert.Equal(payload, decoded);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }

        [Fact]
        public void DecodeQrFromFile_WithNonExistentFile_ReturnsNull()
        {
            var service = new PassengerCheckInService();
            string? decoded = service.DecodeQrFromFile("C:\\non_existent_folder_xyz\\qr_missing.png");

            Assert.Null(decoded);
        }

        [Fact]
        public async Task ProcessCheckInAsync_WhenEmptyCode_ReturnsInvalidQr()
        {
            var service = new PassengerCheckInService();
            var result = await service.ProcessCheckInAsync("");

            Assert.False(result.Success);
            Assert.Equal(CheckInStatus.InvalidQr, result.Status);
        }

        [Fact]
        public async Task ProcessCheckInAsync_WhenTicketNotFound_ReturnsNotFoundStatus()
        {
            using var db = TestDbContextFactory.Create();
            var service = new PassengerCheckInService(db);

            var result = await service.ProcessCheckInAsync("TK_NOT_EXIST_999", "StaffTest");

            Assert.False(result.Success);
            Assert.Equal(CheckInStatus.NotFound, result.Status);
            Assert.Equal("TK_NOT_EXIST_999", result.TicketCode);
        }

        [Fact]
        public async Task ProcessCheckInAsync_WhenTicketCancelled_ReturnsCancelledStatus()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            var ticket = new Ticket
            {
                TicketCode = "TK20261017001",
                CustomerName = "Nguyễn Văn Hủy",
                CustomerPhone = "0912345678",
                RouteId = 1,
                VehicleId = 1,
                SeatNumber = "A01",
                Price = 350000,
                PaymentStatus = "Paid",
                TicketStatus = "Cancelled",
                DepartureTime = DateTime.UtcNow.AddDays(1)
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            var result = await service.ProcessCheckInAsync("QLVT|MAVE:TK20261017001", "Lơ xe");

            Assert.False(result.Success);
            Assert.Equal(CheckInStatus.Cancelled, result.Status);
            Assert.Contains("ĐÃ BỊ HỦY", result.Message);
        }

        [Fact]
        public async Task ProcessCheckInAsync_WhenTicketUnpaid_ReturnsUnpaidStatus()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            var ticket = new Ticket
            {
                TicketCode = "TK20261017002",
                CustomerName = "Lê Thị Chưa Trả Tiền",
                CustomerPhone = "0987654321",
                RouteId = 1,
                VehicleId = 1,
                SeatNumber = "A02",
                Price = 350000,
                PaymentStatus = "Pending",
                TicketStatus = "Booked",
                DepartureTime = DateTime.UtcNow.AddDays(1)
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            var result = await service.ProcessCheckInAsync("TK20261017002", "Lơ xe");

            Assert.False(result.Success);
            Assert.Equal(CheckInStatus.Unpaid, result.Status);
            Assert.Contains("CHƯA THANH TOÁN", result.Message);
        }

        [Fact]
        public async Task ProcessCheckInAsync_WhenValidTicket_ChangesStatusToBoarded()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            var ticket = new Ticket
            {
                TicketCode = "TK20261017003",
                CustomerName = "Trần Thị Hợp Lệ",
                CustomerPhone = "0901234567",
                RouteId = 1,
                VehicleId = 1,
                SeatNumber = "A05",
                Price = 350000,
                PaymentStatus = "Paid",
                TicketStatus = "Booked",
                DepartureTime = DateTime.UtcNow.AddDays(1)
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            var result = await service.ProcessCheckInAsync("QLVT|MAVE:TK20261017003|TUYEN:HN-DN|GHE:A05", "NhanVienSo1");

            Assert.True(result.Success);
            Assert.Equal(CheckInStatus.Success, result.Status);
            Assert.Equal("A05", result.SeatNumber);
            Assert.Equal("Trần Thị Hợp Lệ", result.CustomerName);
            Assert.Equal("NhanVienSo1", result.CheckedInBy);

            // Kiểm tra trạng thái trong DB đã chuyển thành Boarded
            var updatedTicket = await db.Tickets.FindAsync(ticket.Id);
            Assert.NotNull(updatedTicket);
            Assert.Equal("Boarded", updatedTicket.TicketStatus);
            Assert.Equal("NhanVienSo1", updatedTicket.UpdatedBy);
        }

        [Fact]
        public async Task ProcessCheckInAsync_WhenTicketAlreadyBoarded_ReturnsAlreadyBoardedStatus()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            var ticket = new Ticket
            {
                TicketCode = "TK20261017004",
                CustomerName = "Đặng Văn Đã Lên",
                CustomerPhone = "0933221100",
                RouteId = 1,
                VehicleId = 1,
                SeatNumber = "B01",
                Price = 350000,
                PaymentStatus = "Paid",
                TicketStatus = "Boarded",
                DepartureTime = DateTime.UtcNow.AddDays(1),
                UpdatedAt = DateTime.UtcNow.AddMinutes(-10),
                UpdatedBy = "LoXeCu"
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            var result = await service.ProcessCheckInAsync("TK20261017004", "LoXeMoi");

            Assert.False(result.Success);
            Assert.Equal(CheckInStatus.AlreadyBoarded, result.Status);
            Assert.Contains("CẢNH BÁO", result.Message);
        }

        [Fact]
        public async Task RevertCheckInAsync_WhenBoarded_RevertsToBooked()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            var ticket = new Ticket
            {
                TicketCode = "TK20261017005",
                CustomerName = "Vũ Quét Nhầm",
                CustomerPhone = "0977889900",
                RouteId = 1,
                VehicleId = 1,
                SeatNumber = "B02",
                Price = 350000,
                PaymentStatus = "Paid",
                TicketStatus = "Boarded",
                DepartureTime = DateTime.UtcNow.AddDays(1)
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            bool reverted = await service.RevertCheckInAsync("TK20261017005", "StaffUndo");

            Assert.True(reverted);

            var dbTicket = await db.Tickets.FindAsync(ticket.Id);
            Assert.NotNull(dbTicket);
            Assert.Equal("Booked", dbTicket.TicketStatus);
            Assert.Equal("StaffUndo", dbTicket.UpdatedBy);
        }

        [Fact]
        public async Task RevertCheckInAsync_WhenTicketNotBoarded_ReturnsFalse()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            var ticket = new Ticket
            {
                TicketCode = "TK20261017006",
                CustomerName = "Vé Chưa Boarded",
                TicketStatus = "Booked"
            };
            db.Tickets.Add(ticket);
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            bool reverted = await service.RevertCheckInAsync("TK20261017006", "StaffUndo");

            Assert.False(reverted);
        }

        [Fact]
        public async Task GetTodayCheckedInTicketsAsync_ReturnsOnlyBoardedTickets()
        {
            using var db = TestDbContextFactory.Create();
            await SeedData.SeedBasicAsync(db);

            db.Tickets.AddRange(
                new Ticket
                {
                    TicketCode = "TK_B1",
                    CustomerName = "Khách 1 Đã Lên",
                    RouteId = 1,
                    VehicleId = 1,
                    SeatNumber = "A01",
                    Price = 350000,
                    PaymentStatus = "Paid",
                    TicketStatus = "Boarded",
                    DepartureTime = DateTime.UtcNow
                },
                new Ticket
                {
                    TicketCode = "TK_B2",
                    CustomerName = "Khách 2 Chưa Lên",
                    RouteId = 1,
                    VehicleId = 1,
                    SeatNumber = "A02",
                    Price = 350000,
                    PaymentStatus = "Paid",
                    TicketStatus = "Booked",
                    DepartureTime = DateTime.UtcNow
                },
                new Ticket
                {
                    TicketCode = "TK_B3",
                    CustomerName = "Khách 3 Bị Hủy",
                    RouteId = 1,
                    VehicleId = 1,
                    SeatNumber = "A03",
                    Price = 350000,
                    PaymentStatus = "Paid",
                    TicketStatus = "Cancelled",
                    DepartureTime = DateTime.UtcNow
                }
            );
            await db.SaveChangesAsync();

            var service = new PassengerCheckInService(db);
            var list = await service.GetTodayCheckedInTicketsAsync();

            Assert.Single(list);
            Assert.Equal("TK_B1", list[0].TicketCode);
            Assert.Equal("Khách 1 Đã Lên", list[0].CustomerName);
        }
    }
}
