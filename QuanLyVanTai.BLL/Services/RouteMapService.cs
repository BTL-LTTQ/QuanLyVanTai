using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using QuanLyVanTai.BLL.DTOs;
using QuanLyVanTai.BLL.Exceptions;
using QuanLyVanTai.DAL;
using QuanLyVanTai.DAL.Models;

namespace QuanLyVanTai.BLL.Services
{
    /// <summary>
    /// Service nghiệp vụ xử lý dữ liệu Bản đồ số, tọa độ GPS các trạm dừng và sinh mã hiển thị Map API (WebView2).
    /// </summary>
    public class RouteMapService
    {
        private readonly Func<AppDbContext> _contextFactory;

        public RouteMapService(Func<AppDbContext>? contextFactory = null)
        {
            _contextFactory = contextFactory ?? (() => new AppDbContext());
        }

        private AppDbContext CreateContext() => _contextFactory();

        /// <summary>
        /// Lấy danh sách toàn bộ tuyến xe đang hoạt động kèm trạm dừng để hiển thị trên bản đồ.
        /// </summary>
        public async Task<List<RouteListDto>> GetActiveRoutesWithStationsAsync()
        {
            try
            {
                using var db = CreateContext();
                var routes = await db.Routes
                    .Include(r => r.Stations)
                    .Include(r => r.Vehicles)
                    .Where(r => r.Status == "Active")
                    .OrderBy(r => r.RouteName)
                    .AsNoTracking()
                    .ToListAsync();

                return routes.Select(r => new RouteListDto
                {
                    Id = r.Id,
                    RouteCode = r.RouteCode,
                    RouteName = r.RouteName,
                    DistanceKm = r.DistanceKm,
                    EstimatedHours = r.EstimatedHours,
                    BasePrice = r.BasePrice,
                    Status = r.Status,
                    StationCount = r.Stations.Count,
                    StationNames = string.Join(" ➔ ", r.Stations.Select(s => s.StationName)),
                    VehicleCount = r.Vehicles.Count,
                    CreatedAt = r.CreatedAt,
                    CreatedBy = r.CreatedBy
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "GetActiveRoutesWithStationsAsync", ex);
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết một tuyến xe kèm tọa độ GPS các trạm dừng theo thứ tự hành trình di chuyển.
        /// </summary>
        public async Task<RouteMapDto?> GetRouteMapDataAsync(int routeId)
        {
            try
            {
                using var db = CreateContext();
                var route = await db.Routes
                    .Include(r => r.Stations)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == routeId);

                if (route == null) return null;

                // Lọc các trạm dừng hợp lệ có tọa độ GPS
                var validStations = route.Stations
                    .Where(s => s.Latitude.HasValue && s.Longitude.HasValue)
                    .ToList();

                // Sắp xếp trạm dừng theo hướng di chuyển của tuyến:
                // Nếu tuyến từ Bắc vào Nam (HN -> SG, HN -> DN): sắp xếp vĩ độ giảm dần
                // Nếu tuyến từ Nam ra Bắc / Cao nguyên (SG -> DL): sắp xếp hợp lý theo hành trình
                var orderedStations = OrderStationsForRoute(route.RouteCode, validStations);

                var mapDto = new RouteMapDto
                {
                    RouteId = route.Id,
                    RouteCode = route.RouteCode,
                    RouteName = route.RouteName,
                    DistanceKm = route.DistanceKm,
                    EstimatedHours = route.EstimatedHours,
                    BasePrice = route.BasePrice,
                    Status = route.Status
                };

                for (int i = 0; i < orderedStations.Count; i++)
                {
                    var st = orderedStations[i];
                    mapDto.Stations.Add(new RouteStationMapPointDto
                    {
                        StationId = st.Id,
                        StationCode = st.StationCode,
                        StationName = st.StationName,
                        Address = st.Address,
                        City = st.City,
                        Latitude = st.Latitude!.Value,
                        Longitude = st.Longitude!.Value,
                        StopOrder = i + 1,
                        IsDeparture = i == 0,
                        IsDestination = i == orderedStations.Count - 1 && orderedStations.Count > 1
                    });
                }

                return mapDto;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Route", "GetRouteMapDataAsync", ex);
            }
        }

        /// <summary>
        /// Sắp xếp các trạm dừng theo chuỗi hành trình di chuyển thực tế.
        /// </summary>
        private static List<Station> OrderStationsForRoute(string routeCode, List<Station> stations)
        {
            return routeCode switch
            {
                "HN-DN-01" or "HN-SG-01" or "HN-HP-01" =>
                    stations.OrderByDescending(s => s.Latitude).ToList(),

                "SG-DL-01" =>
                    stations.OrderBy(s => s.Latitude).ToList(),

                "SG-VT-01" =>
                    stations.OrderByDescending(s => s.Latitude).ToList(),

                _ => stations.OrderByDescending(s => s.Latitude).ToList()
            };
        }

        /// <summary>
        /// Sinh URL dẫn sang Google Maps Directions để xem chỉ đường trực tiếp trên trình duyệt hoặc Google Maps App.
        /// </summary>
        public string GetGoogleMapsDirectionsUrl(RouteMapDto route)
        {
            if (route.Stations.Count < 2)
            {
                var single = route.Stations.FirstOrDefault();
                if (single != null)
                {
                    return $"https://www.google.com/maps/search/?api=1&query={single.Latitude},{single.Longitude}";
                }
                return "https://www.google.com/maps";
            }

            var origin = route.Stations.First();
            var destination = route.Stations.Last();

            var waypoints = route.Stations
                .Skip(1)
                .Take(route.Stations.Count - 2)
                .Select(s => $"{s.Latitude},{s.Longitude}");

            string waypointsParam = waypoints.Any() ? $"&waypoints={Uri.EscapeDataString(string.Join("|", waypoints))}" : "";

            return $"https://www.google.com/maps/dir/?api=1&origin={origin.Latitude},{origin.Longitude}&destination={destination.Latitude},{destination.Longitude}{waypointsParam}&travelmode=driving";
        }

        /// <summary>
        /// Sinh mã HTML5 + CSS3 + Leaflet/OpenStreetMap nhúng trong Microsoft WebView2.
        /// Hỗ trợ chuyển đổi các lớp bản đồ đường phố, vệ tinh, đánh dấu các trạm dừng với badge màu,
        /// vẽ đường polyline nối các trạm, tương tác click 2 chiều với WinForms qua WebMessage.
        /// </summary>
        public string GenerateInteractiveMapHtml(RouteMapDto routeMap)
        {
            var stationsJson = JsonSerializer.Serialize(routeMap.Stations);
            string routeNameEscaped = System.Web.HttpUtility.HtmlEncode(routeMap.RouteName);
            string routeCodeEscaped = System.Web.HttpUtility.HtmlEncode(routeMap.RouteCode);

            return $$"""
<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Bản Đồ Lộ Trình - {{routeCodeEscaped}}</title>
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" 
          integrity="sha256-p4NxAoJBhIIN+hmNHrzRCf9tD/miZyoHS5obTRR9BMY=" crossorigin=""/>
    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js" 
            integrity="sha256-20nQCchB9co0qIjJZRGuk2/Z9VM+kNiyxNV1lvTlZBo=" crossorigin=""></script>
    <style>
        * {
            box-sizing: border-box;
            margin: 0;
            padding: 0;
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif;
        }
        html, body {
            width: 100%;
            height: 100%;
            overflow: hidden;
            background-color: #0f172a;
        }
        #map {
            width: 100%;
            height: 100%;
            z-index: 1;
        }

        /* Banner thông tin nổi trên bản đồ */
        .route-overlay-card {
            position: absolute;
            top: 14px;
            left: 14px;
            z-index: 1000;
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(8px);
            border-radius: 12px;
            padding: 12px 18px;
            box-shadow: 0 10px 25px -5px rgba(0, 0, 0, 0.2), 0 8px 10px -6px rgba(0, 0, 0, 0.1);
            max-width: 420px;
            border: 1px solid rgba(226, 232, 240, 0.8);
            transition: all 0.2s ease;
        }
        .route-overlay-card:hover {
            box-shadow: 0 20px 25px -5px rgba(0, 0, 0, 0.25);
        }
        .route-badge {
            display: inline-block;
            background: linear-gradient(135deg, #2563eb, #1d4ed8);
            color: #ffffff;
            font-size: 11px;
            font-weight: 700;
            letter-spacing: 0.5px;
            padding: 3px 8px;
            border-radius: 6px;
            text-transform: uppercase;
            margin-bottom: 6px;
        }
        .route-title {
            font-size: 15px;
            font-weight: 700;
            color: #0f172a;
            margin-bottom: 6px;
            line-height: 1.3;
        }
        .route-stats {
            display: flex;
            gap: 14px;
            font-size: 12px;
            color: #475569;
            font-weight: 500;
        }
        .route-stats span {
            display: flex;
            align-items: center;
            gap: 4px;
        }

        /* Marker Trạm dừng tùy chỉnh */
        .custom-pin {
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 50%;
            color: #ffffff;
            font-weight: bold;
            font-size: 12px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.35);
            border: 2.5px solid #ffffff;
            transition: transform 0.2s ease;
            cursor: pointer;
        }
        .custom-pin:hover {
            transform: scale(1.22);
            z-index: 9999 !important;
        }
        .pin-departure {
            background: linear-gradient(135deg, #10b981, #059669);
            box-shadow: 0 0 16px rgba(16, 185, 129, 0.6);
        }
        .pin-waypoint {
            background: linear-gradient(135deg, #0284c7, #0369a1);
        }
        .pin-destination {
            background: linear-gradient(135deg, #ef4444, #dc2626);
            box-shadow: 0 0 16px rgba(239, 68, 68, 0.6);
        }

        /* Popup thông tin trạm dừng */
        .leaflet-popup-content-wrapper {
            border-radius: 12px;
            box-shadow: 0 15px 30px rgba(0,0,0,0.25);
            padding: 4px;
        }
        .station-popup {
            padding: 8px 6px;
            min-width: 220px;
        }
        .station-popup-badge {
            display: inline-block;
            font-size: 10px;
            font-weight: 700;
            padding: 2px 7px;
            border-radius: 4px;
            margin-bottom: 6px;
            text-transform: uppercase;
        }
        .badge-dep { background: #dcfce7; color: #166534; }
        .badge-way { background: #e0f2fe; color: #075985; }
        .badge-des { background: #fee2e2; color: #991b1b; }

        .station-popup h4 {
            font-size: 14px;
            font-weight: 700;
            color: #0f172a;
            margin-bottom: 4px;
        }
        .station-popup p {
            font-size: 12px;
            color: #475569;
            margin-bottom: 3px;
            line-height: 1.4;
        }
        .station-popup .coords {
            font-size: 11px;
            color: #64748b;
            font-family: monospace;
            background: #f1f5f9;
            padding: 3px 6px;
            border-radius: 4px;
            margin-top: 6px;
            display: inline-block;
        }

        /* Controls góc phải */
        .leaflet-control-layers {
            border-radius: 8px !important;
            box-shadow: 0 4px 12px rgba(0,0,0,0.15) !important;
            border: 1px solid #cbd5e1 !important;
        }
    </style>
</head>
<body>
    <div class="route-overlay-card">
        <span class="route-badge">{{routeCodeEscaped}}</span>
        <div class="route-title">{{routeNameEscaped}}</div>
        <div class="route-stats">
            <span>📍 {{routeMap.Stations.Count}} trạm dừng</span>
            <span>📏 {{routeMap.DistanceKm:N0}} km</span>
            <span>⏱️ {{routeMap.EstimatedHours:N1}} giờ</span>
            <span>💵 {{routeMap.BasePrice:N0}} đ</span>
        </div>
    </div>

    <div id="map"></div>

    <script>
        const stations = {{stationsJson}};
        const markers = {};

        // 1. Khởi tạo các lớp bản đồ (Base Layers)
        const streetLayer = L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '© OpenStreetMap contributors'
        });

        const satelliteLayer = L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}', {
            maxZoom: 19,
            attribution: 'Tiles &copy; Esri'
        });

        const cartoVoyager = L.tileLayer('https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png', {
            maxZoom: 19,
            attribution: '© CARTO'
        });

        // 2. Tạo bản đồ với chế độ mặc định
        const map = L.map('map', {
            center: [16.0, 107.5],
            zoom: 6,
            layers: [streetLayer]
        });

        const baseMaps = {
            "🗺️ Đường phố (Street Map)": streetLayer,
            "🛰️ Ảnh vệ tinh (Satellite)": satelliteLayer,
            "🎨 Sáng hiện đại (Voyager)": cartoVoyager
        };

        L.control.layers(baseMaps, null, { position: 'topright' }).addTo(map);

        // 3. Thêm các Marker và đường Polyline kết nối
        const latLngs = [];

        stations.forEach((st, index) => {
            const latLng = [st.Latitude, st.Longitude];
            latLngs.push(latLng);

            // Phân loại pin màu
            let pinClass = "pin-waypoint";
            let badgeClass = "badge-way";
            let roleText = "Trạm trung gian";

            if (st.IsDeparture) {
                pinClass = "pin-departure";
                badgeClass = "badge-dep";
                roleText = "Điểm khởi hành";
            } else if (st.IsDestination) {
                pinClass = "pin-destination";
                badgeClass = "badge-des";
                roleText = "Bến đến";
            }

            // Tạo icon HTML
            const pinIcon = L.divIcon({
                className: 'custom-pin-container',
                html: `<div class="custom-pin ${pinClass}" style="width: 32px; height: 32px;">${st.StopOrder}</div>`,
                iconSize: [32, 32],
                iconAnchor: [16, 16],
                popupAnchor: [0, -18]
            });

            const marker = L.marker(latLng, { icon: pinIcon }).addTo(map);
            markers[st.StationId] = marker;

            // Popup nội dung chi tiết
            const popupContent = `
                <div class="station-popup">
                    <span class="station-popup-badge ${badgeClass}">${roleText}</span>
                    <h4>${st.StopOrder}. ${st.StationName}</h4>
                    <p><strong>Mã trạm:</strong> ${st.StationCode}</p>
                    <p><strong>Địa chỉ:</strong> ${st.Address}, ${st.City}</p>
                    <div class="coords">GPS: ${st.Latitude.toFixed(6)}, ${st.Longitude.toFixed(6)}</div>
                </div>
            `;
            marker.bindPopup(popupContent);

            // Bắn sự kiện ngược lại WinForms khi click vào marker
            marker.on('click', () => {
                if (window.chrome && window.chrome.webview) {
                    window.chrome.webview.postMessage({
                        type: 'stationSelected',
                        stationId: st.StationId,
                        order: st.StopOrder,
                        name: st.StationName
                    });
                }
            });
        });

        // 4. Vẽ đường Polyline lộ trình nối các trạm
        if (latLngs.length > 1) {
            // Lớp viền phát sáng ngoài
            L.polyline(latLngs, {
                color: '#2563eb',
                weight: 8,
                opacity: 0.35,
                lineCap: 'round',
                lineJoin: 'round'
            }).addTo(map);

            // Lớp tim đường chính
            const polyline = L.polyline(latLngs, {
                color: '#1d4ed8',
                weight: 4,
                opacity: 0.9,
                dashArray: '8, 8',
                lineCap: 'round',
                lineJoin: 'round'
            }).addTo(map);

            // Tự động căn chỉnh màn hình vừa khớp toàn bộ lộ trình
            map.fitBounds(polyline.getBounds(), { padding: [50, 50] });
        } else if (latLngs.length === 1) {
            map.setView(latLngs[0], 13);
        }

        // 5. Các hàm JavaScript để WinForms C# gọi điều khiển
        function focusStation(stationId) {
            const marker = markers[stationId];
            if (marker) {
                map.flyTo(marker.getLatLng(), 14, { duration: 1.2 });
                marker.openPopup();
            }
        }

        function fitRouteBounds() {
            if (latLngs.length > 1) {
                map.flyToBounds(L.latLngBounds(latLngs), { padding: [60, 60], duration: 1.0 });
            }
        }

        function setMapLayer(layerName) {
            if (layerName === 'satellite') {
                map.removeLayer(streetLayer);
                map.addLayer(satelliteLayer);
            } else {
                map.removeLayer(satelliteLayer);
                map.addLayer(streetLayer);
            }
        }
    </script>
</body>
</html>
""";
        }
    }
}
