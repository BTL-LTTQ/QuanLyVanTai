using Core.Helpers;
using Core.Logging;
using Core.Security;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.DAL
{
    public static class DatabaseSeeder
    {
        public static void SeedSampleData(AppDbContext db)
        {
            try
            {
                // 1. Seed Trạm dừng nếu chưa có
                if (!db.Stations.Any())
                {
                    var stations = new List<Station>
                    {
                        new Station { StationCode = "BX-MYDINH", StationName = "Bến xe Mỹ Đình", Address = "Số 20 Phạm Hùng, Nam Từ Liêm", City = "Hà Nội", Status = "Active" },
                        new Station { StationCode = "BX-GIAPBAT", StationName = "Bến xe Giáp Bát", Address = "Km6 Giải Phóng, Hoàng Mai", City = "Hà Nội", Status = "Active" },
                        new Station { StationCode = "BX-DANANG", StationName = "Bến xe Trung tâm Đà Nẵng", Address = "Tôn Đức Thắng, Liên Chiểu", City = "Đà Nẵng", Status = "Active" },
                        new Station { StationCode = "BX-MIENDONG", StationName = "Bến xe Miền Đông mới", Address = "501 Hoàng Hữu Nam, TP. Thủ Đức", City = "TP. Hồ Chí Minh", Status = "Active" },
                        new Station { StationCode = "BX-DALAT", StationName = "Bến xe Liên tỉnh Đà Lạt", Address = "Số 1 Tô Hiến Thành, P.3", City = "Lâm Đồng", Status = "Active" },
                        new Station { StationCode = "BX-NIEMNGHIA", StationName = "Bến xe Niệm Nghĩa", Address = "Trần Nguyên Hãn, Lê Chân", City = "Hải Phòng", Status = "Active" }
                    };
                    db.Stations.AddRange(stations);
                    db.SaveChanges();
                }

                // 2. Seed Xe khách nếu chưa có
                if (!db.Vehicles.Any())
                {
                    var vehicles = new List<Vehicle>
                    {
                        new Vehicle { LicensePlate = "29B-123.45", VehicleType = "Giường nằm 36 chỗ", TotalSeats = 36, Status = "Active" },
                        new Vehicle { LicensePlate = "29B-678.90", VehicleType = "Limousine VIP 22 phòng", TotalSeats = 22, Status = "Active" },
                        new Vehicle { LicensePlate = "43B-555.88", VehicleType = "Giường nằm 40 chỗ", TotalSeats = 40, Status = "Active" },
                        new Vehicle { LicensePlate = "51B-999.66", VehicleType = "Limousine VIP 24 phòng", TotalSeats = 24, Status = "Active" }
                    };
                    db.Vehicles.AddRange(vehicles);
                    db.SaveChanges();
                }

                // 3. Seed Tuyến xe nếu chưa có
                if (!db.Routes.Any())
                {
                    var routes = new List<Route>
                    {
                        new Route
                        {
                            RouteCode = "HN-DN-01",
                            RouteName = "Hà Nội - Đà Nẵng (Cao tốc Bắc Nam)",
                            DistanceKm = 760m,
                            EstimatedHours = 14.5m,
                            BasePrice = 380000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "HN-SG-01",
                            RouteName = "Hà Nội - TP. Hồ Chí Minh (Xuyên Việt)",
                            DistanceKm = 1720m,
                            EstimatedHours = 32.0m,
                            BasePrice = 750000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "SG-DL-01",
                            RouteName = "TP. Hồ Chí Minh - Đà Lạt",
                            DistanceKm = 305m,
                            EstimatedHours = 6.5m,
                            BasePrice = 280000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "HN-HP-01",
                            RouteName = "Hà Nội - Hải Phòng (Cao tốc 5B)",
                            DistanceKm = 120m,
                            EstimatedHours = 1.8m,
                            BasePrice = 120000m,
                            Status = "Active"
                        },
                        new Route
                        {
                            RouteCode = "SG-VT-01",
                            RouteName = "TP. Hồ Chí Minh - Vũng Tàu",
                            DistanceKm = 95m,
                            EstimatedHours = 2.0m,
                            BasePrice = 150000m,
                            Status = "Active"
                        }
                    };
                    db.Routes.AddRange(routes);
                    db.SaveChanges();
                }

                // 4. Seed Tài xế nếu chưa có
                if (!db.Drivers.Any())
                {
                    var drivers = new List<Driver>
                    {
                        new Driver { DriverCode = "TX-001", FullName = "Nguyễn Văn Hùng", PhoneNumber = "0901234567", LicenseNumber = "012345678901", LicenseClass = "E", LicenseExpiryDate = new DateTime(2030, 12, 31), Status = "Active", CreatedBy = "System" },
                        new Driver { DriverCode = "TX-002", FullName = "Trần Minh Tâm", PhoneNumber = "0912345678", LicenseNumber = "012345678902", LicenseClass = "D", LicenseExpiryDate = new DateTime(2029, 6, 30), Status = "Active", CreatedBy = "System" },
                        new Driver { DriverCode = "TX-003", FullName = "Lê Quang Dũng", PhoneNumber = "0923456789", LicenseNumber = "012345678903", LicenseClass = "D", LicenseExpiryDate = new DateTime(2028, 3, 15), Status = "Active", CreatedBy = "System" },
                        new Driver { DriverCode = "TX-004", FullName = "Phạm Văn Sơn", PhoneNumber = "0934567890", LicenseNumber = "012345678904", LicenseClass = "E", LicenseExpiryDate = new DateTime(2031, 9, 20), Status = "Active", CreatedBy = "System" },
                        // TX-005 đang nghỉ phép -> dùng để kiểm tra việc lọc tài xế khả dụng khi phân công
                        new Driver { DriverCode = "TX-005", FullName = "Hoàng Đức Long", PhoneNumber = "0945678901", LicenseNumber = "012345678905", LicenseClass = "C", LicenseExpiryDate = new DateTime(2027, 11, 1), Status = "OnLeave", CreatedBy = "System" }
                    };
                    db.Drivers.AddRange(drivers);
                    db.SaveChanges();
                }

                // 5. Seed Lịch trình (Schedule) kèm Phân công tài xế (DriverAssignment) nếu chưa có.
                //    Dữ liệu trải đều 3 trạng thái (Completed / InProgress / Scheduled) để test bộ lọc,
                //    và mỗi xe - mỗi tài xế chỉ nằm trong 1 chuyến tại một thời điểm nên không vi phạm
                //    quy tắc kiểm tra chồng lấn (kể cả thời gian đệm) của ScheduleService.
                if (!db.Schedules.Any())
                {
                    var now = DateTime.Now;
                    var today = now.Date;

                    var routes = db.Routes.ToDictionary(r => r.RouteCode);
                    var vehicles = db.Vehicles.ToDictionary(v => v.LicensePlate);
                    var drivers = db.Drivers.ToDictionary(d => d.DriverCode);

                    // Trả về null nếu thiếu Tuyến/Xe tương ứng để chặn việc seed thiếu dữ liệu.
                    Schedule? BuildSchedule(string code, string routeCode, string licensePlate,
                                            DateTime departure, string status, string? notes)
                    {
                        if (!routes.TryGetValue(routeCode, out var route)) return null;
                        if (!vehicles.TryGetValue(licensePlate, out var vehicle)) return null;

                        var arrival = departure.AddHours((double)route.EstimatedHours);

                        return new Schedule
                        {
                            ScheduleCode = code,
                            RouteId = route.Id,
                            VehicleId = vehicle.Id,
                            DepartureTime = departure,
                            EstimatedArrivalTime = arrival,
                            ActualArrivalTime = status == "Completed" ? arrival.AddMinutes(15) : null,
                            Status = status,
                            Notes = notes,
                            CreatedBy = "System"
                        };
                    }

                    void Assign(Schedule? schedule, string driverCode, string role)
                    {
                        if (schedule == null || !drivers.TryGetValue(driverCode, out var driver)) return;

                        schedule.DriverAssignments.Add(new DriverAssignment
                        {
                            DriverId = driver.Id,
                            Role = role,
                            AssignmentStatus = "Assigned",
                            CreatedBy = "System"
                        });
                    }

                    var yesterdayHaiPhong = BuildSchedule($"SCH-{today.AddDays(-1):yyyyMMdd}-001", "HN-HP-01", "29B-123.45", today.AddDays(-1).AddHours(7), "Completed", "Chạy đúng giờ");
                    var yesterdayVungTau  = BuildSchedule($"SCH-{today.AddDays(-1):yyyyMMdd}-002", "SG-VT-01", "51B-999.66", today.AddDays(-1).AddHours(13), "Completed", null);
                    var runningDaNang     = BuildSchedule($"SCH-{today:yyyyMMdd}-001", "HN-DN-01", "43B-555.88", now.AddHours(-3), "InProgress", "Đang trên đường");
                    var tomorrowSaiGon    = BuildSchedule($"SCH-{today.AddDays(1):yyyyMMdd}-001", "HN-SG-01", "29B-678.90", today.AddDays(1).AddHours(8), "Scheduled", null);
                    var nextDayHaiPhong   = BuildSchedule($"SCH-{today.AddDays(2):yyyyMMdd}-001", "HN-HP-01", "29B-123.45", today.AddDays(2).AddHours(6), "Scheduled", null);

                    Assign(yesterdayHaiPhong, "TX-001", "Primary");
                    Assign(yesterdayHaiPhong, "TX-002", "Secondary");
                    Assign(yesterdayVungTau, "TX-003", "Primary");
                    Assign(runningDaNang, "TX-004", "Primary");
                    Assign(tomorrowSaiGon, "TX-001", "Primary");
                    Assign(tomorrowSaiGon, "TX-003", "Secondary");
                    Assign(nextDayHaiPhong, "TX-002", "Primary");

                    var schedules = new[] { yesterdayHaiPhong, yesterdayVungTau, runningDaNang, tomorrowSaiGon, nextDayHaiPhong }
                        .Where(s => s != null)
                        .Cast<Schedule>()
                        .ToList();

                    if (schedules.Count > 0)
                    {
                        db.Schedules.AddRange(schedules);
                        db.SaveChanges();
                    }
                }

                // 6. Seed tài khoản Khách hàng mẫu nếu chưa có.
                //    DatabaseInitializer cũng tạo tài khoản này nhưng chỉ chạy sau khi đăng nhập;
                //    đặt ở đây để tài khoản có sẵn ngay từ lúc khởi động ứng dụng.
                if (!db.Accounts.Any(a => a.Username == "khachhang"))
                {
                    string salt = SecurityHelper.GenerateSalt();

                    db.Accounts.Add(new Account
                    {
                        Username = "khachhang",
                        PasswordHash = SecurityHelper.HashPassword("khachhang123", salt),
                        PasswordSalt = salt,
                        FullName = "Khách Hàng Mẫu",
                        Email = "khachhang@vantai.com",
                        PhoneNumber = "0912345678",
                        Role = SystemRoles.Customer,
                        Status = "Active",
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "System"
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // Không nuốt lỗi âm thầm: ghi log để phát hiện sớm trường hợp Database
                // chưa được Migration tạo bảng (lỗi "Invalid object name 'Stations'").
                LoggerService.Error(ex, "Lỗi khi nạp dữ liệu mẫu (seed data).");
            }
        }
    }
}
