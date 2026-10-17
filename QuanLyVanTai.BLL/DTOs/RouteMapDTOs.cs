namespace QuanLyVanTai.BLL.DTOs
{
    /// <summary>
    /// DTO chứa thông tin đầy đủ để hiển thị lộ trình trên Bản đồ số (Map API / WebView2).
    /// </summary>
    public class RouteMapDto
    {
        public int RouteId { get; set; }
        public string RouteCode { get; set; } = string.Empty;
        public string RouteName { get; set; } = string.Empty;
        public decimal DistanceKm { get; set; }
        public decimal EstimatedHours { get; set; }
        public decimal BasePrice { get; set; }
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Danh sách các trạm dừng được sắp xếp tuần tự theo hành trình từ điểm xuất phát đến đích.
        /// </summary>
        public List<RouteStationMapPointDto> Stations { get; set; } = new();

        public RouteStationMapPointDto? DepartureStation => Stations.FirstOrDefault();
        public RouteStationMapPointDto? DestinationStation => Stations.Count > 1 ? Stations.LastOrDefault() : null;
    }

    /// <summary>
    /// Tọa độ và thông tin hiển thị của một trạm dừng / điểm đón trên bản đồ.
    /// </summary>
    public class RouteStationMapPointDto
    {
        public int StationId { get; set; }
        public string StationCode { get; set; } = string.Empty;
        public string StationName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int StopOrder { get; set; }
        public bool IsDeparture { get; set; }
        public bool IsDestination { get; set; }

        public string DisplayText => $"{StopOrder}. {StationName} ({City})";
    }
}
