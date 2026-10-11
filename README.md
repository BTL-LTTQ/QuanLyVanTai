# Hệ Thống Quản Lý Vận Tải Hành Khách (QuanLyVanTai)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen?logo=githubactions&logoColor=white)](.github/workflows/build.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> **Bài tập lớn môn Lập trình trực quan (LTTQ)**  
> Ứng dụng desktop Windows Forms quản lý hệ thống vận tải hành khách liên tỉnh, bán vé, quản lý tuyến xe, phương tiện và nhân sự.

---

## 📌 Công Nghệ Sử Dụng

- **Ngôn ngữ**: C# (.NET 10.0)
- **Giao diện**: Windows Forms (WinForms)
- **Kiến trúc**: 3 lớp (3-Tier Architecture)
  - `QuanLyVanTai.UI`: Giao diện người dùng
  - `QuanLyVanTai.BLL`: Xử lý nghiệp vụ (Business Logic Layer)
  - `QuanLyVanTai.DAL`: Tương tác cơ sở dữ liệu (Data Access Layer)
- **ORM**: Entity Framework Core 10 (Code-First)
- **Hệ quản trị CSDL**: Microsoft SQL Server

---

## 🏛️ Cấu Trúc Cơ Sở Dữ Liệu (Database Design)

Hệ thống được thiết kế chuẩn hóa (3NF) với 5 thực thể chính và các mối quan hệ chặt chẽ:

1. **`Accounts` (Tài khoản & Nhân sự)**
   - Quản lý tài khoản đăng nhập, phân quyền (`Admin`, `Manager`, `Staff`, `Driver`) và nhân sự trong hệ thống.
   - Hỗ trợ Unique Index trên `Username` và `Email`.

2. **`Vehicles` (Phương tiện vận tải)**
   - Quản lý thông tin xe khách: Biển số (`LicensePlate` - Unique), loại xe (giường nằm, ghế ngồi, limousine), số lượng ghế/chỗ và trạng thái hoạt động.

3. **`Stations` (Trạm dừng / Bến xe)**
   - Quản lý các bến xe và điểm đón/trả khách trên toàn quốc: Mã trạm (`StationCode` - Unique), tên bến, địa chỉ, tỉnh/thành phố.

4. **`Routes` (Tuyến xe)**
   - Quản lý các lộ trình xe chạy: Mã tuyến (`RouteCode` - Unique), tên tuyến, cự ly (km), thời gian di chuyển dự kiến, giá cước cơ bản.
   - **Quan hệ N - N với Stations**: Tuyến xe đi qua nhiều trạm dừng (bảng liên kết `RouteStations`).

5. **`Tickets` (Vé xe / Hóa đơn)**
   - Lưu trữ thông tin vé: Mã tra cứu vé (`TicketCode` - Unique), thông tin hành khách, số ghế, giá vé thực tế, thời gian xuất bến, trạng thái thanh toán và trạng thái vé.
   - Khóa ngoại liên kết chặt chẽ tới `Route`, `Vehicle` và `Account` (nhân viên xuất vé).

---

## 🌟 Tính Năng Nổi Bật (Phân Hệ Khách Hàng & Đặt Vé)

- **Màn hình Đặt vé trực quan (`FormMain_KhachHang`)**:
  - Chọn tuyến đường, ngày đi, xem sơ đồ ghế động theo từng tầng (Tầng 1 / Tầng 2).
  - Tự động kiểm tra xung đột chỗ ngồi thời gian thực, tính cước vé chuẩn xác.
- **Lịch sử đặt vé & Tra cứu nhanh (`FrmCustomerTicketHistory`)**:
  - Giao diện chuẩn Desktop Windows Forms (MenuStrip, StatusStrip, phím tắt `F5`, `Ctrl+D`, `Ctrl+P`, `Delete`).
  - Lưới hiển thị tinh gọn 5 cột chủ chốt: Mã vé, Tuyến đường, Ghế, Giờ khởi hành, Trạng thái.
  - Tìm kiếm linh hoạt theo mã vé, tên khách, số điện thoại hoặc lọc theo trạng thái vé.
- **Hộp thoại Chi tiết vé xe (`FrmTicketDetail`)**:
  - Xem tường minh thông tin hành khách, thông tin chuyến xe & chỗ ngồi, thông tin thanh toán.
  - Tích hợp nút tác vụ In thẻ lên xe và Hủy vé xe trực tiếp trên form chi tiết.
- **In Thẻ lên xe & Xuất vé điện tử GDI+ (`TicketPdfExportService` & `FrmTicketPrintPreview`)**:
  - Thiết kế theo nhận diện thương hiệu chuẩn **QLVT Bus Lines** (kích thước khổ in nhiệt đứng 600x860).
  - Tích hợp mã QR Code quét bằng điện thoại, lấy bến đi / bến đến động từ dữ liệu tuyến xe.
  - Đóng dấu watermark cảnh báo `VÉ ĐÃ HỦY / VOID` nếu vé không còn hiệu lực.
- **Ràng buộc nghiệp vụ Hủy vé chặt chẽ**:
  - Chỉ cho phép khách hàng hủy vé **trước giờ xe khởi hành tối thiểu 60 phút (1 giờ)**.
  - Tự động vô hiệu hóa nút In vé và chặn tuyệt đối việc in thẻ lên xe đối với vé đã hủy.

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### 1. Yêu cầu hệ thống
- [.NET 10.0 SDK](https://dotnet.microsoft.com/)
- [Visual Studio 2022 / 2025](https://visualstudio.microsoft.com/) (chọn workload **.NET Desktop Development**)
- **Microsoft SQL Server** (LocalDB, SQL Express hoặc SQL Server Developer)

### 2. Cấu hình chuỗi kết nối
Mở file `QuanLyVanTai.UI/appsettings.json` và kiểm tra chuỗi kết nối phù hợp với máy của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=QuanLyVanTaiDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
*(Nếu bạn dùng SQL Server Express, đổi `Server=.` thành `Server=.\\SQLEXPRESS`)*.

### 3. Chạy dự án (Tự động sinh Database)
1. Mở giải pháp `QuanLyVanTai.slnx` bằng Visual Studio.
2. Đặt `QuanLyVanTai.UI` làm **Startup Project**.
3. Nhấn **F5** (hoặc nút **Start**).
4. 🎉 **Hệ thống sẽ tự động tạo cơ sở dữ liệu `QuanLyVanTaiDb` và đầy đủ các bảng cùng dữ liệu mẫu ban đầu!**

---

## 🔑 Tài Khoản Mặc Định (Seed Data)

Khi khởi tạo database lần đầu, hệ thống đã nạp sẵn tài khoản quản trị viên:
- **Tài khoản**: `admin`
- **Mật khẩu**: `admin123`
- **Vai trò**: `Admin`

---

## 📄 Giấy Phép (License)

Dự án được phát hành theo giấy phép [MIT License](LICENSE).
