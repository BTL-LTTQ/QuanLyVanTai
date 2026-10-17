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
                        new Vehicle { LicensePlate = "29B-123.45", VehicleType = "Giường nằm 36 chỗ", TotalSeats = 36, Status = "Ready" },
                        new Vehicle { LicensePlate = "29B-678.90", VehicleType = "Limousine VIP 22 phòng", TotalSeats = 22, Status = "Ready" },
                        new Vehicle { LicensePlate = "43B-555.88", VehicleType = "Giường nằm 40 chỗ", TotalSeats = 40, Status = "Ready" },
                        new Vehicle { LicensePlate = "51B-999.66", VehicleType = "Limousine VIP 24 phòng", TotalSeats = 24, Status = "Ready" }
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

                // 5. Seed Lịch trình (Schedule) kèm Phân công tài xế (DriverAssignment)
                //    Dữ liệu trải đều 3 trạng thái (Completed / InProgress / Scheduled) để test bộ lọc.
                {
                    // Tự động làm sạch bảng Schedules để seed dữ liệu mới nhất (dành cho môi trường dev/test)
                    if (db.Schedules.Any())
                    {
                        db.Schedules.ExecuteDelete();
                        db.SaveChanges();
                    }

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

                        // Cột DepartureTime / EstimatedArrivalTime lưu UTC theo quy ước của entity Schedule.
                        return new Schedule
                        {
                            ScheduleCode = code,
                            RouteId = route.Id,
                            VehicleId = vehicle.Id,
                            DepartureTime = departure.ToUniversalTime(),
                            EstimatedArrivalTime = arrival.ToUniversalTime(),
                            ActualArrivalTime = status == "Completed" ? arrival.AddMinutes(15).ToUniversalTime() : null,
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
                    
                    // 3 chuyến "Scheduled" trong ngày hiện tại để màn hình đặt vé của khách hàng
                    // có chuyến hiển thị ngay; 1 chuyến +2 ngày để kiểm tra bộ lọc theo khoảng ngày.
                    var todayDaNang        = BuildSchedule($"SCH-{today:yyyyMMdd}-005", "HN-DN-01", "43B-555.88", today.AddHours(14), "Scheduled", "Chuyến đi Đà Nẵng chiều nay");
                    var todaySaiGon        = BuildSchedule($"SCH-{today:yyyyMMdd}-002", "HN-SG-01", "29B-678.90", today.AddHours(20), "Scheduled", "Chuyến tối đi Sài Gòn");
                    var todayVungTau       = BuildSchedule($"SCH-{today:yyyyMMdd}-003", "SG-VT-01", "51B-999.66", today.AddHours(8), "Scheduled", "Chuyến sáng đi Vũng Tàu");
                    var todayDaLat         = BuildSchedule($"SCH-{today:yyyyMMdd}-004", "SG-DL-01", "49B-222.33", today.AddHours(22), "Scheduled", "Chuyến đêm đi Đà Lạt");
                    var nextDayHaiPhong    = BuildSchedule($"SCH-{today.AddDays(2):yyyyMMdd}-001", "HN-HP-01", "29B-123.45", today.AddDays(2).AddHours(6), "Scheduled", null);

                    Assign(yesterdayHaiPhong, "TX-001", "Primary");
                    Assign(yesterdayHaiPhong, "TX-002", "Secondary");
                    Assign(yesterdayVungTau, "TX-003", "Primary");
                    Assign(runningDaNang, "TX-004", "Primary");
                    Assign(todayDaNang, "TX-004", "Primary");
                    Assign(todaySaiGon, "TX-001", "Primary");
                    Assign(todaySaiGon, "TX-003", "Secondary");
                    Assign(todayVungTau, "TX-002", "Primary");
                    Assign(todayDaLat, "TX-004", "Primary");
                    Assign(nextDayHaiPhong, "TX-002", "Primary");

                    var schedules = new[] { yesterdayHaiPhong, yesterdayVungTau, runningDaNang, todayDaNang, todaySaiGon, todayVungTau, todayDaLat, nextDayHaiPhong }
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

                // 7. Gán tuyến phụ trách cho xe và chuẩn hóa dữ liệu phương tiện.
                //    Màn hình đặt vé của khách hàng chỉ liệt kê các xe có RouteId trùng với tuyến
                //    đã chọn, nên xe thiếu RouteId sẽ khiến mục "Chuyến xe" bị bỏ trống.
                var routeByCode = db.Routes.ToDictionary(r => r.RouteCode);
                var vehiclesByPlate = db.Vehicles.ToDictionary(v => v.LicensePlate);
                bool vehicleChanged = false;

                // 7a. Chuẩn hóa trạng thái xe về đúng bộ giá trị hợp lệ (Ready / InTransit / Maintenance).
                foreach (var vehicle in vehiclesByPlate.Values)
                {
                    if (vehicle.Status == "Active")
                    {
                        vehicle.Status = "Ready";
                        vehicleChanged = true;
                    }
                }

                // 7b. Gán tuyến phụ trách cho từng xe theo biển số.
                void AssignVehicleRoute(string licensePlate, string routeCode)
                {
                    if (vehiclesByPlate.TryGetValue(licensePlate, out var vehicle) &&
                        vehicle.RouteId == null &&
                        routeByCode.TryGetValue(routeCode, out var route))
                    {
                        vehicle.RouteId = route.Id;
                        vehicleChanged = true;
                    }
                }

                AssignVehicleRoute("29B-123.45", "HN-HP-01");
                AssignVehicleRoute("29B-678.90", "HN-SG-01");
                AssignVehicleRoute("43B-555.88", "HN-DN-01");
                AssignVehicleRoute("51B-999.66", "SG-VT-01");

                // 7c. Bổ sung xe cho tuyến chưa có xe phụ trách để mọi tuyến đều đặt vé được.
                if (routeByCode.TryGetValue("SG-DL-01", out var daLatRoute) &&
                    !vehiclesByPlate.ContainsKey("49B-222.33") &&
                    !vehiclesByPlate.Values.Any(v => v.RouteId == daLatRoute.Id))
                {
                    db.Vehicles.Add(new Vehicle
                    {
                        LicensePlate = "49B-222.33",
                        VehicleType = "Giường nằm 34 chỗ",
                        TotalSeats = 34,
                        Status = "Ready",
                        RouteId = daLatRoute.Id,
                        CreatedBy = "System"
                    });
                    vehicleChanged = true;
                }

                if (vehicleChanged)
                    db.SaveChanges();

                // 8. Cập nhật tọa độ GPS trạm dừng & liên kết trạm dừng vào tuyến xe (phục vụ Bản đồ Map API)
                SeedOrUpdateStationCoordinatesAndRoutes(db);
            }
            catch (Exception ex)
            {
                // Không nuốt lỗi âm thầm: ghi log để phát hiện sớm trường hợp Database
                // chưa được Migration tạo bảng (lỗi "Invalid object name 'Stations'").
                LoggerService.Error(ex, "Lỗi khi nạp dữ liệu mẫu (seed data).");
            }
        }

        private static void SeedOrUpdateStationCoordinatesAndRoutes(AppDbContext db)
        {
            var stationData = new[]
            {
                new { Code = "BX-MYDINH", Name = "Bến xe Mỹ Đình", Addr = "Số 20 Phạm Hùng, Nam Từ Liêm", City = "Hà Nội", Lat = 21.028511, Lng = 105.778330 },
                new { Code = "BX-GIAPBAT", Name = "Bến xe Giáp Bát", Addr = "Km6 Giải Phóng, Hoàng Mai", City = "Hà Nội", Lat = 20.979882, Lng = 105.843702 },
                new { Code = "BX-HAIHAI", Name = "Trạm dừng Hải Dương (Cao tốc 5B)", Addr = "Gia Lộc, Hải Dương", City = "Hải Dương", Lat = 20.884500, Lng = 106.321400 },
                new { Code = "BX-NIEMNGHIA", Name = "Bến xe Niệm Nghĩa", Addr = "Trần Nguyên Hãn, Lê Chân", City = "Hải Phòng", Lat = 20.849310, Lng = 106.671510 },
                new { Code = "BX-VINH", Name = "Bến xe Trung tâm TP. Vinh", Addr = "Số 77 Lê Lợi, TP. Vinh", City = "Nghệ An", Lat = 18.679585, Lng = 105.681335 },
                new { Code = "BX-HUE", Name = "Bến xe Phía Nam Huế", Addr = "97 An Dương Vương, TP. Huế", City = "Thừa Thiên Huế", Lat = 16.443512, Lng = 107.599182 },
                new { Code = "BX-DANANG", Name = "Bến xe Trung tâm Đà Nẵng", Addr = "Tôn Đức Thắng, Liên Chiểu", City = "Đà Nẵng", Lat = 16.059424, Lng = 108.172459 },
                new { Code = "BX-NHATRANG", Name = "Bến xe Phía Nam Nha Trang", Addr = "Km 6 đường 23/10, Vĩnh Trung", City = "Khánh Hòa", Lat = 12.241520, Lng = 109.155820 },
                new { Code = "BX-DALAT", Name = "Bến xe Liên tỉnh Đà Lạt", Addr = "Số 1 Tô Hiến Thành, P.3", City = "Lâm Đồng", Lat = 11.925528, Lng = 108.444747 },
                new { Code = "BX-DAUGIAY", Name = "Trạm dừng nghỉ Dầu Giây", Addr = "Ngã 3 Dầu Giây, Thống Nhất", City = "Đồng Nai", Lat = 10.931215, Lng = 107.126430 },
                new { Code = "BX-MIENDONG", Name = "Bến xe Miền Đông mới", Addr = "501 Hoàng Hữu Nam, TP. Thủ Đức", City = "TP. Hồ Chí Minh", Lat = 10.875630, Lng = 106.815520 },
                new { Code = "BX-VUNGTAU", Name = "Bến xe TP. Vũng Tàu", Addr = "192 Nam Kỳ Khởi Nghĩa, Thắng Tam", City = "Bà Rịa - Vũng Tàu", Lat = 10.355600, Lng = 107.084300 }
            };

            var existingStations = db.Stations.ToDictionary(s => s.StationCode);
            bool hasChanges = false;

            foreach (var item in stationData)
            {
                if (existingStations.TryGetValue(item.Code, out var station))
                {
                    if (station.Latitude == null || station.Longitude == null)
                    {
                        station.Latitude = item.Lat;
                        station.Longitude = item.Lng;
                        hasChanges = true;
                    }
                }
                else
                {
                    var newStation = new Station
                    {
                        StationCode = item.Code,
                        StationName = item.Name,
                        Address = item.Addr,
                        City = item.City,
                        Status = "Active",
                        Latitude = item.Lat,
                        Longitude = item.Lng,
                        CreatedBy = "System"
                    };
                    db.Stations.Add(newStation);
                    existingStations[item.Code] = newStation;
                    hasChanges = true;
                }
            }

            if (hasChanges)
            {
                db.SaveChanges();
            }

            // Gán trạm dừng cho các tuyến xe nếu tuyến xe chưa có trạm
            var routes = db.Routes.Include(r => r.Stations).ToList();
            var routeStationMap = new Dictionary<string, string[]>
            {
                ["HN-DN-01"] = new[] { "BX-MYDINH", "BX-GIAPBAT", "BX-VINH", "BX-HUE", "BX-DANANG" },
                ["HN-SG-01"] = new[] { "BX-MYDINH", "BX-GIAPBAT", "BX-VINH", "BX-DANANG", "BX-NHATRANG", "BX-MIENDONG" },
                ["SG-DL-01"] = new[] { "BX-MIENDONG", "BX-DAUGIAY", "BX-DALAT" },
                ["HN-HP-01"] = new[] { "BX-MYDINH", "BX-GIAPBAT", "BX-HAIHAI", "BX-NIEMNGHIA" },
                ["SG-VT-01"] = new[] { "BX-MIENDONG", "BX-DAUGIAY", "BX-VUNGTAU" }
            };

            bool routeChanged = false;
            foreach (var route in routes)
            {
                if (routeStationMap.TryGetValue(route.RouteCode, out var stationCodes))
                {
                    if (route.Stations.Count == 0)
                    {
                        foreach (var code in stationCodes)
                        {
                            if (existingStations.TryGetValue(code, out var st))
                            {
                                route.Stations.Add(st);
                                routeChanged = true;
                            }
                        }
                    }
                }
            }

            if (routeChanged)
            {
                db.SaveChanges();
            }
        }
    }
}
