using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyVanTai.UI.Models
{
    /// <summary>
    /// Model mutable dùng cho data binding hai chiều (BindingSource) ở màn hình Tuyến xe.
    /// Khi người dùng nhập liệu, Binding ghi thẳng vào property và phát PropertyChanged
    /// để các control khác cùng nguồn cập nhật đồng bộ.
    /// </summary>
    public sealed class RouteEditModel : INotifyPropertyChanged
    {
        private string _routeCode = string.Empty;
        private string _routeName = string.Empty;
        private decimal _distanceKm;
        private decimal _estimatedHours;
        private decimal _basePrice;
        private string _status = "Active";

        public string RouteCode
        {
            get => _routeCode;
            set => SetField(ref _routeCode, value ?? string.Empty);
        }

        public string RouteName
        {
            get => _routeName;
            set => SetField(ref _routeName, value ?? string.Empty);
        }

        public decimal DistanceKm
        {
            get => _distanceKm;
            set => SetField(ref _distanceKm, value);
        }

        public decimal EstimatedHours
        {
            get => _estimatedHours;
            set => SetField(ref _estimatedHours, value);
        }

        public decimal BasePrice
        {
            get => _basePrice;
            set => SetField(ref _basePrice, value);
        }

        public string Status
        {
            get => _status;
            set => SetField(ref _status, string.IsNullOrWhiteSpace(value) ? "Active" : value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
