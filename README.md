# Hệ Thống Quản Lý Vận Tải Hành Khách (QuanLyVanTai)

[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen?logo=githubactions&logoColor=white)](.github/workflows/build.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

> **Bài tập lớn môn Lập trình trực quan (LTTQ)**
> Ứng dụng desktop Windows Forms quản lý hệ thống vận tải hành khách liên tỉnh: bán vé, điều độ chuyến xe, phân công tài xế, quản lý tuyến – trạm – phương tiện – nhân sự, phân quyền và nhật ký hoạt động.

---

## 📌 Công Nghệ Sử Dụng

| Thành phần | Công nghệ |
| --- | --- |
| Ngôn ngữ | C# (.NET 10.0) |
| Giao diện | Windows Forms (WinForms) |
| Kiến trúc | 3 lớp + thư viện dùng chung (`Core`) |
| ORM | Entity Framework Core 10 (Code-First + Migrations) |
| Hệ quản trị CSDL | Microsoft SQL Server |
| Thư viện hỗ trợ | ClosedXML (Excel), QRCoder (mã QR vé), FontAwesome.Sharp (icon) |
| Ghi log | Serilog (Console + File) |
| Kiểm thử | xUnit + EF Core InMemory |

---

## 🗂️ Cấu Trúc Solution

Solution gồm 5 project (`QuanLyVanTai.slnx`) với quan hệ phụ thuộc một chiều:

```
QuanLyVanTai.UI  ──►  QuanLyVanTai.BLL  ──►  QuanLyVanTai.DAL  ──►  Core
        │                    │                      │                  ▲
        └────────────────────┴──────────────────────┴──────────────────┘
QuanLyVanTai.Tests ──►  (Core + DAL + BLL)
```

| Project | Vai trò |
| --- | --- |
| `Core` | Thư viện dùng chung: helper (`SecurityHelper`, `SessionManager`, `DataExportHelper`, `CultureHelper`), logging (`LoggerService`), interfaces (`ISoftDelete`, `IAuditable`), phân quyền (`AuthorizationGuard`, `UserSession`, `SystemRoles`) và tài nguyên đa ngôn ngữ (`Resources`). |
| `QuanLyVanTai.DAL` | Tầng truy cập dữ liệu: `AppDbContext`, các entity (`Models`), repository (`Repositories`) và Migrations. |
| `QuanLyVanTai.BLL` | Tầng nghiệp vụ: các service (`Services`) và DTO (`DTOs`), xử lý Excel và xuất PDF vé. |
| `QuanLyVanTai.UI` | Giao diện WinForms: `Forms`, `UserControls`, `Components`, cấu hình `appsettings.json`. |
| `QuanLyVanTai.Tests` | Kiểm thử đơn vị (xUnit) cho BLL và DAL. |

### Cây thư mục

```
QuanLyVanTai/
├── Core/
│   ├── Configs/          # ThemeConfig
│   ├── Helpers/          # SecurityHelper, SessionManager, DataExportHelper, CultureHelper, ...
│   ├── Interfaces/       # IAuditable, ISoftDelete
│   ├── Logging/          # LoggerService (Serilog)
│   ├── Resources/        # AppResources (vi / en / ja)
│   └── Security/         # AuthorizationGuard, Enums (SystemRoles), UserSession
│
├── QuanLyVanTai.DAL/
│   ├── Migrations/       # 6 migration Code-First
│   ├── Models/           # 10 entity
│   ├── Repositories/     # BaseRepository + Route/Schedule/Station/Vehicle/Driver
│   ├── AppDbContext.cs    # DbContext + Fluent API + seed tài khoản mặc định
│   └── DatabaseSeeder.cs  # Dữ liệu mẫu runtime
│
├── QuanLyVanTai.BLL/
│   ├── DTOs/             # Auth, Route, Schedule, SeatMap, Ticket, Vehicle, Station, Booking, AiChatbot
│   ├── Exceptions/       # BusStationException (BusinessRuleViolationException...)
│   └── Services/
│       ├── AuthService / AccountService / PermissionService
│       ├── RouteManagementService / RouteExcelService
│       ├── StationManagementService / VehicleManagementService
│       ├── ScheduleService / CustomerBookingService / SeatMapService
│       ├── TicketService / TicketPdfExportService
│       ├── AuditLogService / AiChatbotService
│       └── DatabaseInitializer
│
├── QuanLyVanTai.UI/
│   ├── Components/       # SeatMapControl
│   ├── Forms/            # FrmAuth, FormMain, FormMain_KhachHang, FrmTicketDetail,
│   │                     # FrmTicketPrintPreview, FrmConfirmDelete, FrmErrorDialog
│   ├── Images/           # Icon hiển thị trên menu
│   ├── Models/           # TicketDisplayModel
│   ├── Properties/       # Resources
│   ├── UserControls/     # UcDashboard, UcManageRoute, UcManageVehicleAndStation,
│   │                     # UcManageStaff, UcManageService, UcManageTicket, UcManagePromotion,
│   │                     # UcPermission, UcAuditLog, UcCustomerBooking, UcCustomerTicketHistory,
│   │                     # UcAiChatbot
│   ├── Utils/            # ConfigReader, GlobalExceptionHandler
│   ├── Program.cs        # Điểm khởi động
│   └── appsettings.json  # Chuỗi kết nối + cấu hình AI Chatbot + Logging
│
├── QuanLyVanTai.Tests/
│   ├── BLL/              # Kiểm thử các service nghiệp vụ
│   ├── DAL/              # Kiểm thử repository
│   └── Helpers/          # TestDbContextFactory, SeedData
│
├── .github/workflows/    # CI: build + test trên GitHub Actions
├── LICENSE
└── README.md
```

---

## 🏛️ Thiết Kế Cơ Sở Dữ Liệu

Database `QuanLyVanTaiDb` gồm **10 bảng thực thể** (cộng bảng liên kết `RouteStations` do EF Core sinh tự động cho quan hệ N–N):

| # | Bảng | Mô tả |
| --- | --- | --- |
| 1 | `Accounts` | Tài khoản đăng nhập & nhân sự. Unique index trên `Username`, `Email`. Hỗ trợ băm mật khẩu, khoá tài khoản, câu hỏi bảo mật, soft-delete. |
| 2 | `Vehicles` | Phương tiện: biển số (unique), loại xe, tổng số ghế, hãng sản xuất, trạng thái (`Ready` / `InTransit` / `Maintenance`). |
| 3 | `Stations` | Trạm dừng / bến xe: mã trạm (unique), tên, địa chỉ, tỉnh/thành, trạng thái. |
| 4 | `Routes` | Tuyến xe: mã tuyến (unique), tên tuyến, cự ly (km), thời gian dự kiến (giờ), giá cước cơ bản, trạng thái (`Active` / `Suspended`). |
| 5 | `Tickets` | Vé xe: mã vé (unique), hành khách, số ghế, giá vé, giờ khởi hành, trạng thái thanh toán & trạng thái vé. |
| 6 | `Drivers` | Tài xế: mã tài xế, họ tên, GPLX (unique), hạng bằng, ngày hết hạn, trạng thái (`Active` / `OnLeave` / `Inactive`). |
| 7 | `Schedules` | Chuyến xe: mã chuyến (unique), tuyến, xe, giờ khởi hành / đến dự kiến / đến thực tế, trạng thái (`Scheduled` / `InProgress` / `Completed` / `Cancelled` / `Delayed`). |
| 8 | `DriverAssignments` | Phân công tài xế cho chuyến (junction `Schedule` – `Driver`): vai trò (`Primary` / `Secondary`), trạng thái phân công. |
| 9 | `AuditLogs` | Nhật ký hoạt động: người dùng, hành động, đối tượng, giá trị cũ/mới (JSON), IP, thời điểm. |
| 10 | `RolePermissions` | Ma trận phân quyền theo vai trò × menu (`CanView`, `CanAdd`, `CanEdit`, `CanDelete`, `CanExport`). |

**Quan hệ chính:**
- `Routes` ↔ `Stations`: N–N (bảng liên kết `RouteStations` do EF Core sinh tự động).
- `Routes` 1–N `Vehicles`, `Routes` 1–N `Tickets`, `Vehicles` 1–N `Tickets`.
- `Accounts` 1–N `Tickets` (nhân viên lập vé).
- `Schedules` 1–N `DriverAssignments` N–1 `Drivers` (N–N giữa chuyến và tài xế).

---

## 🔐 Phân Quyền (RBAC)

Hệ thống có 4 vai trò (`Core/Security/Enums.cs`):

| Vai trò | Màn hình chính | Quyền hạn |
| --- | --- | --- |
| Quản trị viên | `FormMain` | Toàn quyền, kể cả phân quyền & nhật ký |
| Quản lý | `FormMain` | Quản lý tuyến – trạm – phương tiện – nhân sự – giá vé – khuyến mãi |
| Nhân viên bán vé | `FormMain_KhachHang` | Bán vé, tra cứu vé, in vé |
| Khách hàng | `FormMain_KhachHang` | Đặt vé trực tuyến, xem lịch sử vé |

---

## ✨ Chức Năng Chính

- **Tổng quan (Dashboard)**: thống kê nhanh tình trạng vận hành.
- **Quản lý Tuyến xe**: thêm/sửa/xóa, nhập – xuất Excel (ClosedXML).
- **Quản lý Trạm dừng & Phương tiện**: quản lý bến xe và đội xe.
- **Điều độ chuyến xe (`ScheduleService`)**: kiểm tra chồng lấn thời gian nghiêm ngặt theo công thức `StartA < EndB AND EndA > StartB`, kèm thời gian đệm xoay vòng xe (30 phút) và nghỉ ngơi tài xế (15 phút).
- **Phân công tài xế**: lái chính / lái phụ, chỉ chọn tài xế đang `Active` và GPLX còn hạn.
- **Bán vé & Sơ đồ ghế (`SeatMapControl`)**: chọn ghế trực quan, in vé kèm mã QR.
- **Đặt vé khách hàng & Lịch sử vé**: dành cho vai trò Khách hàng.
- **Quản lý Nhân sự / Dịch vụ / Giá vé / Khuyến mãi**.
- **Phân quyền & Nhật ký hoạt động**: ghi vết thao tác Thêm/Sửa/Xóa/Xuất file.
- **Trợ lý AI (`AiChatbotService`)**: hỗ trợ hỏi đáp nghiệp vụ qua API Gemini/OpenAI (cấu hình trong `appsettings.json`).

---

## 🚀 Hướng Dẫn Cài Đặt & Chạy Dự Án

### 1. Yêu cầu hệ thống
- [.NET 10.0 SDK](https://dotnet.microsoft.com/)
- [Visual Studio 2022 / 2025](https://visualstudio.microsoft.com/) (workload **.NET Desktop Development**)
- **Microsoft SQL Server** (SQL Server Developer / Express / LocalDB)

### 2. Cấu hình chuỗi kết nối
Mở [appsettings.json](QuanLyVanTai.UI/appsettings.json) và chỉnh chuỗi kết nối cho phù hợp với máy của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=QuanLyVanTaiDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

- `Server=.` → dùng instance SQL Server mặc định (`MSSQLSERVER`).
- Nếu dùng SQL Server Express, đổi thành `Server=.\SQLEXPRESS`.
- Nếu dùng LocalDB, đổi thành `Server=(localdb)\MSSQLLocalDB`.
- `TrustServerCertificate=True` là bắt buộc với SQL Server local (mặc định driver bật `Encrypt=True`).

> Chuỗi kết nối được đọc tập trung từ `appsettings.json` cho cả tầng UI lẫn `AppDbContext`, đảm bảo Migration và ứng dụng luôn trỏ về **cùng một database**.

### 3. Chạy dự án (tự động sinh Database)
1. Mở solution `QuanLyVanTai.slnx` bằng Visual Studio.
2. Đặt `QuanLyVanTai.UI` làm **Startup Project**.
3. Nhấn **F5**.
4. Hệ thống gọi `db.Database.Migrate()` để tạo database `QuanLyVanTaiDb` cùng toàn bộ bảng, sau đó nạp dữ liệu mẫu qua `DatabaseSeeder`.

Hoặc chạy bằng dòng lệnh:

```powershell
dotnet run --project QuanLyVanTai.UI
```

### 4. Chạy kiểm thử

```powershell
dotnet test
```

---

## 🔑 Tài Khoản Mặc Định (Seed Data)

Ngoài dữ liệu mẫu về trạm, xe, tuyến, tài xế, chuyến xe và phân công, hệ thống tạo sẵn các tài khoản sau:

| Tài khoản | Mật khẩu | Vai trò |
| --- | --- | --- |
| `admin` | `admin123` | Quản trị viên |
| `quanly` | `quanly123` | Quản lý |
| `nhanvien` | `nhanvien123` | Nhân viên bán vé |
| `khachhang` | `khachhang123` | Khách hàng |

> Mật khẩu được băm bằng **PBKDF2 SHA-256** (100.000 vòng lặp, salt 16 byte, key 32 byte) qua `SecurityHelper`.

---

## 📄 Giấy Phép (License)

Dự án được phát hành theo giấy phép [MIT License](LICENSE).
