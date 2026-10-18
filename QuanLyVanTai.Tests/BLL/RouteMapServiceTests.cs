using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Services;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;
using QuanLyVanTai.Tests.Helpers;

namespace QuanLyVanTai.Tests.BLL
{
    public class RouteMapServiceTests
    {
        [Fact]
        public async Task GetActiveRoutesWithStationsAsync_ReturnsActiveRoutesWithStationSummary()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using (var db = TestDbContextFactory.Create(dbName))
            {
                var r1 = new Route
                {
                    RouteCode = "HN-DN-01",
                    RouteName = "Hà Nội - Đà Nẵng",
                    DistanceKm = 760m,
                    EstimatedHours = 14.5m,
                    BasePrice = 380000m,
                    Status = "Active"
                };
                var r2 = new Route
                {
                    RouteCode = "HN-SG-01",
                    RouteName = "Hà Nội - Sài Gòn",
                    DistanceKm = 1720m,
                    EstimatedHours = 32m,
                    BasePrice = 750000m,
                    Status = "Suspended"
                };
                var st1 = new Station { StationCode = "BX-MD", StationName = "Mỹ Đình", Address = "Phạm Hùng", City = "Hà Nội", Status = "Active", Latitude = 21.028, Longitude = 105.778 };
                var st2 = new Station { StationCode = "BX-DN", StationName = "Đà Nẵng", Address = "Tôn Đức Thắng", City = "Đà Nẵng", Status = "Active", Latitude = 16.059, Longitude = 108.172 };

                r1.Stations.Add(st1);
                r1.Stations.Add(st2);

                db.Routes.AddRange(r1, r2);
                db.Stations.AddRange(st1, st2);
                await db.SaveChangesAsync();
            }

            var service = new RouteMapService(() => TestDbContextFactory.Create(dbName));

            // Act
            var routes = await service.GetActiveRoutesWithStationsAsync();

            // Assert
            Assert.Single(routes);
            var activeRoute = routes.First();
            Assert.Equal("HN-DN-01", activeRoute.RouteCode);
            Assert.Equal(2, activeRoute.StationCount);
            Assert.Contains("Mỹ Đình", activeRoute.StationNames);
            Assert.Contains("Đà Nẵng", activeRoute.StationNames);
        }

        [Fact]
        public async Task GetRouteMapDataAsync_OrdersStationsAndMarksDepartureAndDestination()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            int routeId = 0;
            using (var db = TestDbContextFactory.Create(dbName))
            {
                var route = new Route
                {
                    RouteCode = "HN-DN-01",
                    RouteName = "Hà Nội - Đà Nẵng",
                    DistanceKm = 760m,
                    EstimatedHours = 14.5m,
                    BasePrice = 380000m,
                    Status = "Active"
                };

                // Trạm dừng không theo thứ tự vĩ độ
                var stDaNang = new Station { StationCode = "BX-DN", StationName = "Đà Nẵng", Address = "Tôn Đức Thắng", City = "Đà Nẵng", Status = "Active", Latitude = 16.059, Longitude = 108.172 };
                var stMyDinh = new Station { StationCode = "BX-MD", StationName = "Mỹ Đình", Address = "Phạm Hùng", City = "Hà Nội", Status = "Active", Latitude = 21.028, Longitude = 105.778 };
                var stVinh = new Station { StationCode = "BX-VH", StationName = "Vinh", Address = "Lê Lợi", City = "Nghệ An", Status = "Active", Latitude = 18.679, Longitude = 105.681 };

                route.Stations.Add(stDaNang);
                route.Stations.Add(stMyDinh);
                route.Stations.Add(stVinh);

                db.Routes.Add(route);
                await db.SaveChangesAsync();
                routeId = route.Id;
            }

            var service = new RouteMapService(() => TestDbContextFactory.Create(dbName));

            // Act
            var mapDto = await service.GetRouteMapDataAsync(routeId);

            // Assert
            Assert.NotNull(mapDto);
            Assert.Equal(3, mapDto.Stations.Count);

            // Tuyến HN-DN được sắp xếp từ Bắc vào Nam (vĩ độ giảm dần): Mỹ Đình -> Vinh -> Đà Nẵng
            Assert.Equal("BX-MD", mapDto.Stations[0].StationCode);
            Assert.True(mapDto.Stations[0].IsDeparture);
            Assert.False(mapDto.Stations[0].IsDestination);
            Assert.Equal(1, mapDto.Stations[0].StopOrder);

            Assert.Equal("BX-VH", mapDto.Stations[1].StationCode);
            Assert.False(mapDto.Stations[1].IsDeparture);
            Assert.False(mapDto.Stations[1].IsDestination);
            Assert.Equal(2, mapDto.Stations[1].StopOrder);

            Assert.Equal("BX-DN", mapDto.Stations[2].StationCode);
            Assert.False(mapDto.Stations[2].IsDeparture);
            Assert.True(mapDto.Stations[2].IsDestination);
            Assert.Equal(3, mapDto.Stations[2].StopOrder);

            Assert.NotNull(mapDto.DepartureStation);
            Assert.Equal("BX-MD", mapDto.DepartureStation.StationCode);
            Assert.NotNull(mapDto.DestinationStation);
            Assert.Equal("BX-DN", mapDto.DestinationStation.StationCode);
        }

        [Fact]
        public void GenerateInteractiveMapHtml_ContainsLeafletAndCoordinates()
        {
            // Arrange
            var service = new RouteMapService();
            var route = new RouteMapDto
            {
                RouteId = 1,
                RouteCode = "HN-DN-01",
                RouteName = "Hà Nội - Đà Nẵng",
                DistanceKm = 760m,
                EstimatedHours = 14.5m,
                BasePrice = 380000m,
                Stations = new List<RouteStationMapPointDto>
                {
                    new RouteStationMapPointDto { StationId = 1, StationCode = "BX-MD", StationName = "Mỹ Đình", Address = "Phạm Hùng", City = "Hà Nội", Latitude = 21.028511, Longitude = 105.778330, StopOrder = 1, IsDeparture = true },
                    new RouteStationMapPointDto { StationId = 2, StationCode = "BX-DN", StationName = "Đà Nẵng", Address = "Tôn Đức Thắng", City = "Đà Nẵng", Latitude = 16.059424, Longitude = 108.172459, StopOrder = 2, IsDestination = true }
                }
            };

            // Act
            string html = service.GenerateInteractiveMapHtml(route);

            // Assert
            Assert.Contains("leaflet.js", html);
            Assert.Contains("HN-DN-01", html);
            Assert.Contains("21.028511", html);
            Assert.Contains("108.172459", html);
            Assert.Contains("L.polyline", html);
            Assert.Contains("chrome.webview.postMessage", html);
        }

        [Fact]
        public void GetGoogleMapsDirectionsUrl_GeneratesValidGoogleMapsUrl()
        {
            // Arrange
            var service = new RouteMapService();
            var route = new RouteMapDto
            {
                RouteId = 1,
                RouteCode = "HN-DN-01",
                RouteName = "Hà Nội - Đà Nẵng",
                Stations = new List<RouteStationMapPointDto>
                {
                    new RouteStationMapPointDto { StationId = 1, Latitude = 21.028, Longitude = 105.778, StopOrder = 1, IsDeparture = true },
                    new RouteStationMapPointDto { StationId = 2, Latitude = 18.679, Longitude = 105.681, StopOrder = 2 },
                    new RouteStationMapPointDto { StationId = 3, Latitude = 16.059, Longitude = 108.172, StopOrder = 3, IsDestination = true }
                }
            };

            // Act
            string url = service.GetGoogleMapsDirectionsUrl(route);

            // Assert
            Assert.StartsWith("https://www.google.com/maps/dir/", url);
            Assert.Contains("origin=21.028,105.778", url);
            Assert.Contains("destination=16.059,108.172", url);
            Assert.Contains("waypoints=", url);
            Assert.Contains("travelmode=driving", url);
        }

        [Fact]
        public void DatabaseSeeder_SeedsStationCoordinatesAndRouteLinks()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var db = TestDbContextFactory.Create(dbName))
            {
                DatabaseSeeder.SeedSampleData(db);

                var stationsWithCoords = db.Stations.Where(s => s.Latitude != null && s.Longitude != null).ToList();
                Assert.True(stationsWithCoords.Count >= 6);

                var routesWithStations = db.Routes.Include(r => r.Stations).Where(r => r.Stations.Any()).ToList();
                Assert.True(routesWithStations.Count >= 5);
            }
        }
    }
}
