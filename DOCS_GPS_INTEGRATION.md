# 📡 Hướng Dẫn Tích Hợp GPS Tracking System

## 🗺️ Tổng Quan

Dashboard hiện tại đã được thiết kế sẵn sàng để tích hợp với hệ thống GPS tracking thực tế. Bản đồ hiện đang hiển thị dữ liệu demo với tọa độ GPS thực tế của các tuyến đường TP.HCM.

---

## 📊 Model Dữ Liệu GPS

### `BusGPSData` Class

```csharp
public class BusGPSData
{
    public string PlateNumber { get; set; }        // Biển số xe: "51B-184.26"
    public string RouteCode { get; set; }          // Mã tuyến: "T-02"
    public string RouteName { get; set; }          // Tên tuyến: "Bến Thành → Củ Chi"
    
    // GPS Coordinates
    public double Latitude { get; set; }           // Vĩ độ: 10.762622
    public double Longitude { get; set; }          // Kinh độ: 106.660172
    
    // Real-time Status
    public int CurrentSpeed { get; set; }          // Tốc độ (km/h): 62
    public BusStatus Status { get; set; }          // Trạng thái: Running/Stopped/Warning/Offline
    public DateTime LastUpdate { get; set; }       // Thời gian cập nhật cuối
    
    // Optional Extended Data
    public double? Heading { get; set; }           // Hướng di chuyển (0-360°)
    public string? DriverName { get; set; }        // Tên tài xế
    public int? PassengerCount { get; set; }       // Số hành khách hiện tại
}
```

### `BusStatus` Enum

```csharp
public enum BusStatus
{
    Running,    // 🟢 Đang chạy bình thường
    Stopped,    // 🟡 Dừng tạm thời (trạm, đèn đỏ)
    Warning,    // 🔴 Cảnh báo (quá tốc độ, lệch lộ trình)
    Offline     // ⚫ Mất kết nối GPS
}
```

---

## 🔌 Các Phương Thức Tích Hợp

### **Option 1: REST API** (Khuyến nghị cho polling)

```csharp
private async Task<List<BusGPSData>> FetchRealTimeGPSDataAsync()
{
    using HttpClient client = new HttpClient();
    client.BaseAddress = new Uri("https://api.yourcompany.com/");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_API_KEY");
    
    var response = await client.GetAsync("gps/active-buses");
    response.EnsureSuccessStatusCode();
    
    var gpsData = await response.Content.ReadFromJsonAsync<List<BusGPSData>>();
    return gpsData ?? new List<BusGPSData>();
}
```

**API Endpoint Specification:**
- **URL**: `GET /api/gps/active-buses`
- **Response**: JSON array của `BusGPSData`
- **Update Frequency**: Mỗi 10-30 giây

### **Option 2: WebSocket** (Real-time streaming)

```csharp
using System.Net.WebSockets;

private async Task ConnectToGPSWebSocket()
{
    using var ws = new ClientWebSocket();
    await ws.ConnectAsync(new Uri("wss://api.yourcompany.com/gps/stream"), CancellationToken.None);
    
    var buffer = new byte[4096];
    while (ws.State == WebSocketState.Open)
    {
        var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
        if (result.MessageType == WebSocketMessageType.Text)
        {
            string json = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var gpsUpdate = JsonSerializer.Deserialize<BusGPSData>(json);
            
            // Update UI on main thread
            this.Invoke(() => UpdateBusOnMap(gpsUpdate));
        }
    }
}
```

### **Option 3: Database Polling** (Dùng SQL Server)

```csharp
// Trong BLL layer, tạo GPSService.cs
public class GPSService
{
    private readonly ApplicationDbContext _context;
    
    public async Task<List<BusGPSData>> GetActiveBusesAsync()
    {
        return await _context.GPSTracking
            .Where(g => g.LastUpdate >= DateTime.Now.AddMinutes(-5)) // Active trong 5 phút
            .Join(_context.Vehicles, 
                  gps => gps.VehicleId, 
                  veh => veh.Id, 
                  (gps, veh) => new BusGPSData
                  {
                      PlateNumber = veh.LicensePlate,
                      RouteCode = veh.CurrentRoute.RouteCode,
                      RouteName = veh.CurrentRoute.RouteName,
                      Latitude = gps.Latitude,
                      Longitude = gps.Longitude,
                      CurrentSpeed = gps.Speed,
                      Status = DetermineStatus(gps),
                      LastUpdate = gps.Timestamp
                  })
            .ToListAsync();
    }
}
```

**Database Schema:**
```sql
CREATE TABLE GPSTracking (
    Id INT PRIMARY KEY IDENTITY,
    VehicleId INT NOT NULL,
    Latitude DECIMAL(10, 7) NOT NULL,
    Longitude DECIMAL(10, 7) NOT NULL,
    Speed INT NOT NULL,
    Heading DECIMAL(5, 2),
    Timestamp DATETIME2 NOT NULL DEFAULT GETDATE(),
    
    FOREIGN KEY (VehicleId) REFERENCES Vehicles(Id)
);

CREATE INDEX IX_GPSTracking_VehicleId_Timestamp 
ON GPSTracking(VehicleId, Timestamp DESC);
```

---

## ⏱️ Auto-Refresh Implementation

### Timer-based Refresh (Trong UcDashboard)

```csharp
private System.Windows.Forms.Timer? _gpsRefreshTimer;

public UcDashboard()
{
    InitializeComponent();
    LoadChartData();
    BuildDashboardLayout();
    StartGPSAutoRefresh(); // Bật auto-refresh
}

private void StartGPSAutoRefresh()
{
    _gpsRefreshTimer = new System.Windows.Forms.Timer
    {
        Interval = 30000 // 30 seconds
    };
    
    _gpsRefreshTimer.Tick += async (s, e) => 
    {
        try
        {
            var liveData = await FetchRealTimeGPSDataAsync();
            
            // Update map on UI thread
            this.Invoke(() => 
            {
                DrawMapWithBuses(liveData.ToArray());
            });
        }
        catch (Exception ex)
        {
            // Log error, don't stop timer
            Debug.WriteLine($"GPS refresh error: {ex.Message}");
        }
    };
    
    _gpsRefreshTimer.Start();
}

protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        _gpsRefreshTimer?.Stop();
        _gpsRefreshTimer?.Dispose();
    }
    base.Dispose(disposing);
}
```

---

## 🗺️ Map Projection & Coordinate Conversion

### Current Implementation (Simple Linear)

```csharp
private Point ConvertGPSToScreen(double latitude, double longitude)
{
    // Map bounds for TP.HCM region
    const double MIN_LAT = 10.0;
    const double MAX_LAT = 11.0;
    const double MIN_LON = 105.5;
    const double MAX_LON = 107.0;
    
    // Screen dimensions
    const int MAP_WIDTH = 380;
    const int MAP_HEIGHT = 280;
    
    // Linear mapping
    int x = (int)((longitude - MIN_LON) / (MAX_LON - MIN_LON) * MAP_WIDTH);
    int y = (int)((MAX_LAT - latitude) / (MAX_LAT - MIN_LAT) * MAP_HEIGHT);
    
    // Clamp to screen bounds
    x = Math.Max(10, Math.Min(MAP_WIDTH - 10, x));
    y = Math.Max(10, Math.Min(MAP_HEIGHT - 10, y));
    
    return new Point(x, y);
}
```

### Advanced: Mercator Projection (Cho độ chính xác cao)

```csharp
private Point ConvertGPSToScreenMercator(double latitude, double longitude)
{
    // Web Mercator projection (EPSG:3857)
    const double EARTH_RADIUS = 6378137.0;
    
    double x = longitude * Math.PI / 180.0 * EARTH_RADIUS;
    double y = Math.Log(Math.Tan((90.0 + latitude) * Math.PI / 360.0)) * EARTH_RADIUS;
    
    // Scale to map bounds
    // ... (implementation details)
    
    return new Point((int)screenX, (int)screenY);
}
```

---

## 🎨 Map Visualization Enhancements

### 1. Thêm Polyline cho Route

```csharp
private void DrawRoute(Graphics g, List<PointF> routePoints)
{
    using (Pen routePen = new Pen(Color.FromArgb(100, 59, 130, 246), 3))
    {
        routePen.DashStyle = DashStyle.Solid;
        routePen.StartCap = LineCap.Round;
        routePen.EndCap = LineCap.ArrowAnchor;
        
        g.DrawLines(routePen, routePoints.ToArray());
    }
}
```

### 2. Animated Bus Movement

```csharp
private void AnimateBusMovement(BusGPSData from, BusGPSData to, int durationMs)
{
    Point startPos = ConvertGPSToScreen(from.Latitude, from.Longitude);
    Point endPos = ConvertGPSToScreen(to.Latitude, to.Longitude);
    
    var animTimer = new System.Windows.Forms.Timer { Interval = 16 }; // 60 FPS
    int elapsed = 0;
    
    animTimer.Tick += (s, e) =>
    {
        elapsed += 16;
        float progress = Math.Min(1.0f, elapsed / (float)durationMs);
        
        // Interpolate position
        int x = (int)(startPos.X + (endPos.X - startPos.X) * progress);
        int y = (int)(startPos.Y + (endPos.Y - startPos.Y) * progress);
        
        // Redraw map with updated position
        mapPanel.Invalidate();
        
        if (progress >= 1.0f)
        {
            animTimer.Stop();
            animTimer.Dispose();
        }
    };
    
    animTimer.Start();
}
```

### 3. Heatmap Layer (Density visualization)

```csharp
private void DrawHeatmap(Graphics g, List<BusGPSData> buses)
{
    // Group buses by proximity
    var clusters = ClusterBusesByLocation(buses, radiusKm: 5);
    
    foreach (var cluster in clusters)
    {
        Point center = ConvertGPSToScreen(cluster.CenterLat, cluster.CenterLon);
        int radius = Math.Min(50, cluster.BusCount * 10);
        
        // Draw gradient circle
        using (var path = new GraphicsPath())
        {
            path.AddEllipse(center.X - radius, center.Y - radius, radius * 2, radius * 2);
            
            using (var brush = new PathGradientBrush(path))
            {
                brush.CenterColor = Color.FromArgb(100, 239, 68, 68);
                brush.SurroundColors = new[] { Color.Transparent };
                
                g.FillPath(brush, path);
            }
        }
    }
}
```

---

## 🔔 Alert System Integration

### Geofencing Alerts

```csharp
public class GeofenceAlert
{
    public bool IsOutOfRoute(BusGPSData bus, List<PointF> allowedRoute)
    {
        Point busPos = ConvertGPSToScreen(bus.Latitude, bus.Longitude);
        
        // Check if bus is within 100m of route
        return !allowedRoute.Any(p => 
            Distance(busPos, p) < 100 // meters
        );
    }
    
    public bool IsSpeedingViolation(BusGPSData bus, int speedLimit)
    {
        return bus.CurrentSpeed > speedLimit;
    }
}
```

---

## 📦 Recommended GPS Hardware/Services

### GPS Tracking Devices
1. **Teltonika FMB920** - Enterprise GPS tracker
2. **Queclink GV300** - Vehicle tracking với OBDII
3. **Concox GT06N** - Budget-friendly option

### Cloud GPS Services
1. **Google Maps Platform** - Maps API + Geolocation
2. **Mapbox GL** - Custom map styling
3. **HERE Technologies** - Fleet tracking
4. **Vietmap API** - Vietnam-specific maps

### Vietnamese GPS Providers
1. **Viettel GPS** - https://gps.viettel.vn
2. **VNPT GPS** - https://smartgps.vnpt.vn
3. **VMS Mobifone** - https://vms.mobifone.vn

---

## 🚀 Quick Start Implementation

### Step 1: Thêm GPSService vào BLL

```bash
# Tạo file mới
QuanLyVanTai.BLL/Services/GPSService.cs
```

### Step 2: Tạo Migration cho GPS Table

```bash
dotnet ef migrations add AddGPSTrackingTable
dotnet ef database update
```

### Step 3: Update UcDashboard

```csharp
// Uncomment phần FetchRealTimeGPSDataAsync()
// Uncomment phần StartGPSAutoRefresh()
// Kết nối với GPSService
```

### Step 4: Test với Mock Data

```csharp
// Test endpoint: https://mocki.io/v1/your-mock-gps-data
```

---

## 📝 Testing Checklist

- [ ] GPS coordinates conversion chính xác
- [ ] Map refresh không lag UI
- [ ] Handle GPS connection timeout
- [ ] Handle empty/null GPS data
- [ ] Bus markers update smoothly
- [ ] Memory leak check (dispose timers)
- [ ] Error logging & monitoring

---

## 🎯 Future Enhancements

1. **Interactive Map**
   - Click vào bus để xem chi tiết
   - Zoom in/out
   - Pan/drag map

2. **Route Replay**
   - Xem lại lộ trình đã đi
   - Playback animation

3. **Traffic Layer**
   - Real-time traffic data
   - Optimal route suggestion

4. **3D Visualization**
   - Elevation profile
   - 3D bus models

---

## 📞 Support

Nếu cần hỗ trợ tích hợp GPS:
- Email: support@yourcompany.com
- Documentation: https://docs.yourcompany.com/gps-integration

---

**Last Updated**: 2024-10-07
**Version**: 1.0.0
