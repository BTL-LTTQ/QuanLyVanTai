---
name: winforms-visual-designer
description: Tiêu chuẩn toàn diện cho Đồ án Lập trình Windows Forms (C# / .NET 10+) và Lập trình trực quan (LTTQ) trong Visual Studio. Đảm bảo 100% Visual Studio Designer WYSIWYG, responsive layout không đè controls, chuẩn desktop WinForms (MenuStrip, ToolStrip, StatusStrip, MDI/UserControl), kiến trúc 3 tầng (UI/BLL/DAL), phân quyền 2 vai trò, EF Core + ADO.NET, async background task + ProgressBar, GDI+/LiveCharts2, ErrorProvider, xuất báo cáo Excel/PDF, Logging/Publish, và đáp ứng đầy đủ Rubric 10 điểm từ Chương 1 đến Chương 9.
---

# WinForms Visual Designer & Course Project Standard (.NET 10 / C# 13+)
## Chuẩn Lập Trình Trực Quan (LTTQ) & Yêu Cầu Đồ Án Môn Học Windows Forms

Skill này quy định toàn bộ chuẩn mực kiến trúc, kỹ thuật lập trình và thiết kế giao diện cho **Đồ án môn học Lập trình Windows Forms (.NET 10 / C# 13+)** trong **Visual Studio 2022/2025+**, đồng thời đáp ứng trọn vẹn **Rubric đánh giá 10 điểm** từ Chương 1 đến Chương 9 của đề cương học phần.

---

## 1. Nền Tảng Kỹ Thuật & Kiến Trúc Phân Tầng

### 1.1 Yêu Cầu Môi Trường Bắt Buộc
- **Framework**: .NET 10 trở lên, ngôn ngữ C# 13/14.
- **IDE**: Visual Studio 2022 (v17.12+) / Visual Studio 2025+.
- **Cơ sở dữ liệu**: SQL Server (LocalDB / Express / Developer).
- **Phạm vi nền tảng**: Ứng dụng desktop Windows Forms thuần túy. Không thay thế phần lõi bằng WPF, MAUI hay web ASP.NET.

### 1.2 Kiến Trúc Phân 3 Tầng (3-Tier Clean Architecture)
Tuyệt đối không viết logic nghiệp vụ hay câu lệnh truy vấn dữ liệu trực tiếp trong code-behind của Form:

```
Solution: QuanLyVanTai / QuanLyBanHang / ...
├── 1. Presentation Layer (UI)           ──> Windows Forms Project (.NET 10 - WinForms)
│    ├── Forms/                          ──> MainForm, LoginForm, Dialog Forms
│    ├── UserControls/                   ──> Các view chức năng dạng UserControl độc lập
│    ├── Helpers/ / ViewModels/          ──> Data binding, UI formatting, Error helper
│    └── Program.cs                      ──> Entry point, High-DPI setup, Global logging
├── 2. Business Logic Layer (BLL)        ──> Class Library (.NET 10)
│    ├── Services/                       ──> TicketService, ReportService, AuthService
│    ├── Interfaces/                     ──> ITicketService, IExportService
│    └── DTOs/ / Validators/             ──> Record/Class truyền dữ liệu, business rules
└── 3. Data Access Layer (DAL)           ──> Class Library (.NET 10)
     ├── Entities/                       ──> Domain entities (Code-First)
     ├── Context/                        ──> AppDbContext (EF Core)
     ├── Repositories/                   ──> Generic / Specific Repositories
     ├── Migrations/                     ──> EF Core Migrations
     └── AdoDemos/                       ──> Minh họa ADO.NET thuần (SqlCommand, SqlDataReader)
```

### 1.3 Ứng Dụng Tính Năng C# Hiện Đại & OOP (Chương 1–2)
- **Tối thiểu 3 class nghiệp vụ** áp dụng kế thừa, interface, abstract class (ví dụ: `BaseEntity`, `IRepository<T>`, `BaseExportService`).
- **C# Hiện đại**: Áp dụng `record` cho DTOs/Events, `nullable reference types` (`#nullable enable`), `pattern matching`, `switch expressions`.
- **LINQ**: Tận dụng triệt để LINQ to Objects và LINQ to Entities cho tìm kiếm, lọc, sắp xếp, gom nhóm dữ liệu.
- **NuGet Packages**: Sử dụng tối thiểu 2 gói ngoài chất lượng cao (ví dụ: `Microsoft.EntityFrameworkCore.SqlServer`, `ClosedXML` / `EPPlus`, `LiveChartsCore.SkiaSharpView.WinForms`, `Serilog.Sinks.File`).

---

## 2. Chuẩn Thiết Kế Trực Quan (Visual-First & WYSIWYG)

### 2.1 Nguyên Tắc Vàng: 100% Designer Compatibility
Trong môn học Lập trình trực quan (LTTQ), **Visual Studio Form Designer là tiêu chuẩn số 1 để đánh giá và chấm điểm**:

1. **Hiển thị trực quan tức thì**: Khi giảng viên hoặc reviewer double-click vào bất kỳ `Form` hoặc `UserControl` nào trong Solution Explorer, giao diện phải hiển thị hoàn chỉnh ngay trên canvas.
2. **Kéo-thả (Drag-and-Drop) hoàn toàn**: Bắt buộc hỗ trợ kéo control từ **Toolbox (`Ctrl + Alt + X`)** thả vào giao diện mà không bị lỗi layout.
3. **Chỉnh sửa qua Properties Window (`F4`)**: Mọi thuộc tính màu sắc, font, anchor, dock, margin, padding, border đều phải xem và sửa trực quan được trên bảng thuộc tính.
4. **CẤM dựng UI thủ công bằng code-behind**:
   - ❌ **Nghiêm cấm**: Viết hàng trăm dòng code C# trong file `.cs` (như `BuildUI()`, `CreateButtons()`, tự `new Button()`, tự tính tọa độ `Location = new Point(x, y)`) trong khi file `.Designer.cs` rỗng. Việc này làm canvas Designer bị trắng tinh, vi phạm nguyên lý LTTQ.
   - ✅ **Bắt buộc**: Toàn bộ khởi tạo và cấu hình controls phải nằm trong `InitializeComponent()` của file `.Designer.cs`. File code-behind `.cs` chỉ chứa xử lý sự kiện, data binding và gọi service.

### 2.2 Ổn Định Designer (Chống Crash Khi Mở Form)
Mọi Form và UserControl bắt buộc phải có **constructor không tham số (parameterless constructor)** và chặn thực thi mã runtime trong chế độ thiết kế:

```csharp
public UcManageTicket()
{
    InitializeComponent();

    // 🛡️ BẢO VỆ: Chặn Visual Studio Designer thực thi kết nối CSDL hoặc gọi Service
    if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime 
        || this.DesignMode)
    {
        return;
    }

    // Chỉ thực thi logic nghiệp vụ khi ứng dụng đang chạy thật:
    InitializeBusinessServices();
}
```

---

## 3. Chuẩn Bố Cục Desktop WinForms & Chống Đè Controls

### 3.1 Bố Cục Desktop Chuẩn Mực (Loại Bỏ Thuật Ngữ/Bố Cục Kiểu Web)
Theo yêu cầu đồ án, **không sử dụng mô hình bố cục hay thuật ngữ kiểu website** (không gọi "header/sidebar/footer" dạng web). Ứng dụng phải theo chuẩn desktop Windows:

```
┌────────────────────────────────────────────────────────────────────────┐
│ MenuStrip (Thực đơn chính: Hệ thống, Danh mục, Nghiệp vụ, Báo cáo, Trợ giúp) │
├────────────────────────────────────────────────────────────────────────┤
│ ToolStrip (Thanh công cụ: Icon thao tác nhanh: Thêm, Lưu, Xóa, Xuất Excel, In)│
├───────────────────┬────────────────────────────────────────────────────┤
│ Navigation Tree / │ Main Content Work Area                             │
│ Module Selector   │                                                    │
│ (Hoặc MDI Client) │ • MDI Children Forms HOẶC                          │
│                   │ • UserControls nhúng trong Panel (Dock = Fill)     │
│                   │ • Hoặc TabControl điều hướng đa tab                │
├───────────────────┴────────────────────────────────────────────────────┤
│ StatusStrip (Thanh trạng thái: Người đăng nhập, Quyền, Thời gian, ProgressBar)│
└────────────────────────────────────────────────────────────────────────┘
```

- **Top Navigation**: `MenuStrip` (menu thả xuống chuẩn desktop) và `ToolStrip` (icon thao tác nhanh kèm ToolTip).
- **Bottom Bar**: `StatusStrip` (chứa `ToolStripStatusLabel` hiển thị thông tin tài khoản, ngày giờ và `ToolStripProgressBar` cho tác vụ nền).
- **Điều hướng màn hình**: Dùng mô hình **MDI (Multiple Document Interface)** với `IsMdiContainer = true` HOẶC mô hình **View Injection (UserControl nhúng vào panel trung tâm `Dock = Fill`)** hoặc **TabControl**.

### 3.2 Quy Tắc Chống Đè Nút & Va Chạm Controls (Zero Overlaps)
Tuyệt đối không dùng tọa độ pixel tĩnh `Location = new Point(x, y)`. Bắt buộc sử dụng 4 layout containers chuyên dụng:

| Container Control | Mục Đích Sử Dụng | Cách Ngăn Chặn Va Chạm |
| :--- | :--- | :--- |
| **`TableLayoutPanel`** | Form nhập liệu, lưới thẻ thống kê (stat cards, KPI tiles). | Chia cột/hàng theo tỷ lệ phần trăm (`Percent`) hoặc cố định (`Absolute`). Mỗi ô chỉ chứa 1 control/panel, hoàn toàn cô lập, không thể va chạm. |
| **`FlowLayoutPanel`** | Thanh nút bấm tác vụ (Thêm, Sửa, Xóa, In, Đóng), nhóm nút lọc. | Tự động sắp xếp tuần tự. Bắt buộc đặt `WrapContents = true` để khi co nhỏ cửa sổ, các nút tự động xuống dòng thay vì bị che mất. |
| **`SplitContainer`** | Giao diện Master-Detail (Danh sách bên trái/trên, Chi tiết/Sửa bên phải/dưới). | Thanh trượt phân tách hai khung. Bắt buộc đặt `Panel1MinSize` và `Panel2MinSize` để người dùng không thể kéo mất một bên. |
| **`Panel` kết hợp `Dock`** | Khung xương chính (Menu `Top`, Thanh công cụ `Top`, Thanh trạng thái `Bottom`, Vùng làm việc `Fill`). | Neo các cạnh tự nhiên không cần tính toán tọa độ thủ công. |

### 3.3 Tối Thiểu 8 Loại Controls Được Dùng Có Mục Đích (Chương 3)
Hệ thống phải kết hợp ít nhất 8 loại control WinForms chuẩn:
1. `MenuStrip` / `ContextMenuStrip`
2. `ToolStrip`
3. `StatusStrip` & `ProgressBar`
4. `DataGridView`
5. `TableLayoutPanel` / `FlowLayoutPanel`
6. `SplitContainer`
7. `DateTimePicker`
8. `ComboBox` / `ListBox`
9. `NumericUpDown`
10. `ErrorProvider`
11. `TabControl`
12. `ToolTip`

---

## 4. Chuẩn Responsive Layout, DataGridView & High-DPI

### 4.1 Giới Hạn Kích Thước Tối Thiểu & Chống Tràn Giao Diện
- **Form `MinimumSize`**: Bắt buộc thiết lập trên mọi Form (ví dụ: `MainForm.MinimumSize = new Size(1024, 650);`, `Dialog.MinimumSize = new Size(540, 420);`) để tránh người dùng thu nhỏ form làm biến dạng giao diện.
- **Panel `AutoScroll`**: Panel chứa nội dung chính (`pnlContentHost`) phải đặt `AutoScroll = true`. Khi chạy trên màn hình độ phân giải thấp (1366x768 hoặc 1280x720), thanh cuộn xuất hiện tự nhiên, không bao giờ bị cắt mất nút.

### 4.2 Cấu Hình `DataGridView` Tự Co Giãn (Fluid Data Grid)
- Bắt buộc đặt `AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill`.
- Phân bổ độ rộng cột theo trọng số tỷ lệ `FillWeight` (ví dụ: Mã: 15%, Tên: 35%, Ngày: 25%, Trạng thái: 25%).
- Đặt `MinimumWidth` an toàn (ví dụ: `60px` - `120px`) cho từng cột để cột không bao giờ bị bẹp về 0px khi cửa sổ bị co lại.
- Dữ liệu binding phải là dữ liệu thực từ CSDL thông qua EF Core/ADO.NET, không dùng dữ liệu giả cố định (hard-code).

### 4.3 High-DPI Scaling (.NET 10)
Trong file `Program.cs`, thiết lập chuẩn DPI để chữ và icon không bị nhòe hoặc tràn viền trên màn hình 125%, 150%:
```csharp
ApplicationConfiguration.Initialize();
Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
```
Trong các Form và UserControl:
```csharp
this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
```

---

## 5. Kiến Trúc Đa Màn Hình: Tách UserControl (View Injection)

### 5.1 Loại Bỏ Anti-Pattern Xếp Chồng Panel ("Z-Order Hell")
❌ **Nghiêm cấm**: Tạo 5-10 panel full-size đè chồng lên nhau trong cùng một `FormMain`, rồi dùng `BringToFront()` / `Visible = true/false` trong code để đổi tab.
- **Tác hại**: Trong Visual Studio Designer, các panel nằm phía dưới bị che khuất, không thể click chỉnh sửa. Kéo thả control mới rất dễ rơi nhầm vào panel khác. Code `Designer.cs` phình to hàng nghìn dòng, dễ hỏng giao diện khi merge code.

### 5.2 Kiến Trúc Chuẩn: Module Hóa Bằng `UserControl` Độc Lập
Tách mỗi module nghiệp vụ thành một file `UserControl` độc lập (`UcQuanLyVe`, `UcQuanLyTuyen`, `UcThongKeBaoCao`...). Mỗi UserControl có canvas riêng, kéo thả trực quan độc lập.

**Cơ chế View Injection trong Form chính:**
```csharp
private UserControl? _currentView;

public void SwitchView(UserControl newView)
{
    if (newView == null) return;

    pnlContentHost.SuspendLayout();
    try
    {
        if (_currentView != null)
        {
            pnlContentHost.Controls.Remove(_currentView);
            _currentView.Dispose(); // Giải phóng tài nguyên
            _currentView = null;
        }

        newView.Dock = DockStyle.Fill;
        pnlContentHost.Controls.Add(newView);
        newView.BringToFront();
        _currentView = newView;
    }
    finally
    {
        pnlContentHost.ResumeLayout();
    }
}
```

---

## 6. Xử Lý Sự Kiện, Validation & Trải Nghiệm Người Dùng (Chương 4)

### 6.1 Validation Dữ Liệu Chặt Chẽ Bằng `ErrorProvider`
- Bắt buộc kiểm tra tính hợp lệ của mọi trường nhập liệu (không để trống, đúng định dạng email, số điện thoại, ngày tháng hợp lệ, giá trị số dương).
- Sử dụng sự kiện `Validating` của controls kết hợp `ErrorProvider` để hiển thị icon cảnh báo màu đỏ và thông báo lỗi rõ ràng bên cạnh ô nhập liệu:
```csharp
private void txtGiaVe_Validating(object sender, CancelEventArgs e)
{
    if (string.IsNullOrWhiteSpace(txtGiaVe.Text) || !decimal.TryParse(txtGiaVe.Text, out decimal gia) || gia <= 0)
    {
        e.Cancel = true;
        errorProviderMain.SetError(txtGiaVe, "Giá vé phải là số dương hợp lệ!");
    }
    else
    {
        errorProviderMain.SetError(txtGiaVe, string.Empty);
    }
}
```

### 6.2 Xử Lý Phím Tắt & Sự Kiện Bàn Phím/Chuột
- **Phím tắt toàn hệ thống**: Thiết lập `ShortcutKeys` trên `MenuStrip` (ví dụ: `Ctrl+N` Thêm mới, `Ctrl+S` Lưu, `Ctrl+P` In, `F5` Làm mới).
- **Phím Enter di chuyển con trỏ**: Xử lý phím Enter để tự động nhảy sang ô nhập liệu tiếp theo thay cho phím Tab.
- **Tìm kiếm Realtime có Debounce**: Khi người dùng gõ vào ô tìm kiếm, dùng `System.Windows.Forms.Timer` debounce (300-500ms) trước khi gọi truy vấn để tránh quá tải CSDL.
- **Thao tác chuột**: Xử lý sự kiện kéo-thả (Drag & Drop) hoặc click chuột phải mở `ContextMenuStrip`.

---

## 7. Lập Trình I/O, Async & Tác Vụ Nền (Chương 5)

### 7.1 Import / Export Dữ Liệu
- Cung cấp ít nhất 1 chức năng Import dữ liệu từ file (CSV / Excel / JSON) vào CSDL và Export dữ liệu từ CSDL ra file.
- Hỗ trợ chọn file an toàn thông qua `OpenFileDialog` và `SaveFileDialog` kèm bộ lọc định dạng (`Filter = "Excel Files|*.xlsx|CSV Files|*.csv"`).

### 7.2 Tác Vụ Nặng Chạy Nền Không Đóng Băng Giao Diện (Async & Progress)
- Mọi thao tác tốn thời gian (import nhiều dòng, xuất file lớn, thống kê phức tạp) **bắt buộc chạy bất đồng bộ** bằng `Task.Run` hoặc các hàm async I/O.
- Báo cáo tiến độ trực quan lên giao diện thông qua `IProgress<T>` / `Progress<T>` cập nhật vào `ToolStripProgressBar` trên `StatusStrip`:
```csharp
private async void btnImportData_Click(object sender, EventArgs e)
{
    btnImportData.Enabled = false;
    toolStripProgressBar.Visible = true;
    toolStripProgressBar.Value = 0;

    var progress = new Progress<int>(percent =>
    {
        toolStripProgressBar.Value = percent;
        statusLabel.Text = $"Đang xử lý: {percent}%...";
    });

    try
    {
        int count = await Task.Run(() => _importService.ImportExcelData(filePath, progress));
        MessageBox.Show($"Nhập thành công {count} bản ghi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Lỗi import: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        btnImportData.Enabled = true;
        toolStripProgressBar.Visible = false;
        statusLabel.Text = "Sẵn sàng";
    }
}
```

---

## 8. Đồ Họa GDI+ & Trực Quan Hóa Dữ Liệu (Chương 6)

- **Biểu đồ thống kê trên Dashboard**: Bắt buộc có tối thiểu 1 biểu đồ trực quan (biểu đồ cột doanh thu theo tháng, biểu đồ tròn tỷ lệ danh mục, hoặc đường xu hướng).
- **Phản ánh dữ liệu thật từ CSDL**: Số liệu vẽ biểu đồ phải được truy vấn từ Database (EF Core / ADO.NET), tự động cập nhật khi dữ liệu thay đổi; không dùng số liệu giả cố định.
- **Giải pháp triển khai**:
  - *Cách 1*: Tự vẽ bằng **GDI+** (sử dụng sự kiện `Paint` của `Panel` hoặc `PictureBox`, sử dụng `Graphics.DrawRectangle`, `FillPie`, `DrawString`). Bắt buộc bật thuộc tính `DoubleBuffered = true` để chống nhấp nháy (flicker).
  - *Cách 2*: Sử dụng thư viện trực quan hóa dữ liệu hiện đại như **LiveCharts2** (`LiveChartsCore.SkiaSharpView.WinForms`) hoặc **OxyPlot**.
- **Không tràn khung**: Biểu đồ phải tự co giãn kích thước theo container khi phóng to/thu nhỏ cửa sổ.

---

## 9. Cơ Sở Dữ Liệu: EF Core & ADO.NET (Chương 7)

### 9.1 Entity Framework Core (Phương Án Chính Cho Toàn Bộ CRUD)
- **Code-First & Migrations**: Định nghĩa các Entity class rõ ràng, cấu hình Fluent API trong `DbContext`, quản lý phiên bản CSDL qua Migrations (`Add-Migration`, `Update-Database`).
- **Quan hệ khóa ngoại**: Tối thiểu 2 bảng có quan hệ ràng buộc (1-N hoặc N-N), sử dụng `Include()` / `ThenInclude()` để Eager Loading dữ liệu liên quan.
- **Truy vấn bất đồng bộ**: Sử dụng các phương thức async của EF Core (`ToListAsync()`, `FirstOrDefaultAsync()`, `SaveChangesAsync()`).

### 9.2 Minh Họa ADO.NET Thuần (Đối Chiếu Kỹ Thuật)
- Để đáp ứng trọn vẹn yêu cầu Chương 7, xây dựng **ít nhất 1 chức năng** (ví dụ: tìm kiếm nâng cao nhiều tiêu chí, xuất thống kê nhanh, hoặc màn hình đăng nhập) sử dụng **ADO.NET thuần** (`SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlParameter` chống SQL Injection).
- Giúp sinh viên đối chiếu ưu/nhược điểm về hiệu năng và độ tiện lợi giữa ORM (EF Core) và truy vấn trực tiếp (ADO.NET).

---

## 10. Báo Cáo & Xuất Tài Liệu (Chương 8)

- **Xuất Báo Cáo Đa Định Dạng**: Cung cấp chức năng xuất báo cáo ra file **Excel (`.xlsx`)** (sử dụng `ClosedXML` hoặc `EPPlus`) và **PDF** theo bộ lọc dữ liệu (khoảng thời gian từ ngày - đến ngày, theo danh mục hoặc trạng thái).
- **In Ấn Trực Quan (Print Preview)**: Tích hợp `PrintDocument` và `PrintPreviewDialog` để xem trước hóa đơn / phiếu giao dịch trước khi in, HOẶC thiết kế template báo cáo chuyên nghiệp bằng công cụ như **FastReport**.

---

## 11. Đóng Gói, Triển Khai, Logging & Tích Hợp (Chương 9)

### 11.1 Ghi Log Lỗi Hệ Thống & Bắt Lỗi Toàn Cục
- Tích hợp thư viện ghi log chuyên nghiệp (ví dụ: `Serilog` ghi log ra file hàng ngày: `logs/app-.txt`).
- Thiết lập bắt lỗi toàn cục trong `Program.cs` để ứng dụng không bị văng đột ngột khi xảy ra ngoại lệ không mong muốn:
```csharp
Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
Application.ThreadException += (s, e) =>
{
    Log.Error(e.Exception, "Lỗi UI Thread Exception không được bắt");
    MessageBox.Show("Đã xảy ra lỗi không mong muốn. Vui lòng kiểm tra file log!", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
};
AppDomain.CurrentDomain.UnhandledException += (s, e) =>
{
    Log.Fatal(e.ExceptionObject as Exception, "Lỗi Nghiêm Trọng AppDomain Unhandled Exception");
};
```

### 11.2 Triển Khai & Đóng Gói Ứng Dụng
- Đóng gói ứng dụng thành bản **Publish độc lập (Self-Contained)**:
  ```bash
  dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
  ```
- Đảm bảo bản build chạy được ngay trên máy tính khác mà không yêu cầu máy đó phải cài sẵn .NET SDK hay Visual Studio.

### 11.3 Tích Hợp Dịch Vụ Ngoài (Điểm Cộng)
- Tích hợp gọi ít nhất 1 REST API ngoài bằng `HttpClient` async (ví dụ: API tỷ giá ngoại tệ, API tra cứu thời tiết, API quận/huyện/tỉnh thành Việt Nam, hoặc API QR thanh toán ngân hàng VietQR).

---

## 12. Phân Quyền Người Dùng & Quản Trị Hệ Thống

Ứng dụng bắt buộc có tối thiểu **2 vai trò người dùng**:

| Vai Trò | Giao Diện Điều Hướng | Quyền Hạn & Chức Năng Bắt Buộc |
| :--- | :--- | :--- |
| **Người Dùng / Nhân Viên** | Màn hình làm việc với menu thu gọn theo quyền hạn. | – Đăng nhập, đăng xuất, đổi mật khẩu cá nhân.<br>– Tìm kiếm, lọc dữ liệu (DataGridView + LINQ).<br>– Tạo mới giao dịch/nghiệp vụ chính (lập đơn, bán vé, tiếp nhận...).<br>– Xem lịch sử và trạng thái giao dịch của chính mình.<br>– Validate chặt chẽ thông tin nhập liệu. |
| **Quản Trị Viên / Quản Lý** | Toàn quyền menu, hiển thị thêm màn hình Dashboard tổng quan. | – Quản lý tài khoản người dùng, phân quyền (CRUD).<br>– Quản lý danh mục cốt lõi của hệ thống (sản phẩm, tuyến xe, nhân viên...).<br>– Duyệt / xử lý các giao dịch (đổi trạng thái, phê duyệt/từ chối).<br>– Xem Dashboard thống kê số liệu và biểu đồ trực quan.<br>– Xuất báo cáo tổng hợp (Excel/PDF) theo bộ lọc. |

---

## 13. Quy Chuẩn Đặt Tên & Quản Lý Controls

Đặt tên controls theo chuẩn tiền tố để hiển thị khoa học trong **Document Outline (`Ctrl + Alt + T`)**:

| Tiền Tố | Loại Control | Ví Dụ Đặt Tên Chuẩn |
| :--- | :--- | :--- |
| `btn` | Button / IconButton | `btnThemMoi`, `btnLuu`, `btnXuatExcel`, `btnDong` |
| `txt` | TextBox | `txtHoTen`, `txtSoDienThoai`, `txtTimKiem` |
| `cbo` | ComboBox | `cboLoaiVe`, `cboTrangThai`, `cboPhongBan` |
| `dgv` | DataGridView | `dgvDanhSachVe`, `dgvNhatKyGiaoDich` |
| `pnl` | Panel | `pnlWorkArea`, `pnlActionButtons` |
| `tlp` | TableLayoutPanel | `tlpThongKeCards`, `tlpNhapLieuGrid` |
| `flp` | FlowLayoutPanel | `flpThanhTacVu`, `flpBoLocNhanh` |
| `split`| SplitContainer | `splitDanhSachVaChiTiet` |
| `dtp` | DateTimePicker | `dtpNgayKhoiHanh`, `dtpTuNgay`, `dtpDenNgay` |
| `num` | NumericUpDown | `numSoLuong`, `numDonGia` |
| `chk` | CheckBox | `chkKichHoat`, `chkGhiNhoDangNhap` |
| `lbl` | Label | `lblTieuDeTrang`, `lblTongTien` |
| `err` | ErrorProvider | `errorProviderMain` |
| `prg` | ProgressBar | `progressBarTienTrinh` |
| `uc`  | UserControl | `ucTongQuanDashboard`, `ucQuanLyVe` |

---

## 14. Bảng Rubric Đánh Giá Đồ Án 10 Điểm & Checklist Nghiệm Thu

| STT | Tiêu Chí Đánh Giá (Rubric) | Yêu Cầu Để Đạt Điểm Tối Đa | Điểm | Checklist Nghiệm Thu |
| :---: | :--- | :--- | :---: | :--- |
| **1** | **Kiến trúc & OOP hiện đại** *(Ch.1–2)* | Solution 3 tầng (UI/BLL/DAL) rõ ràng; áp dụng kế thừa, interface, đóng gói; C# hiện đại (record, pattern matching, nullable); dùng LINQ hợp lý; >= 2 NuGet packages. | **1.0** | - [ ] Solution tách dự án UI, BLL, DAL<br>- [ ] Có >= 3 class nghiệp vụ OOP<br>- [ ] Code-behind không chứa SQL/Business logic |
| **2** | **Giao diện & Controls WinForms** *(Ch.3)* | Chuẩn desktop (MenuStrip, ToolStrip, StatusStrip); >= 8 controls; 100% Designer WYSIWYG; responsive co giãn không đè nút; DataGridView AutoSize Fill. | **1.0** | - [ ] Mở được trong VS Designer (không trắng form)<br>- [ ] Kéo thả Toolbox & sửa Properties F4 bình thường<br>- [ ] Không đè controls khi co giãn cửa sổ |
| **3** | **Xử lý sự kiện & Nhập liệu** *(Ch.4)* | Validate đầy đủ qua ErrorProvider; phím tắt ShortcutKeys, phím Enter chuyển ô, debounce tìm kiếm; data binding hai chiều mượt mà. | **1.0** | - [ ] Có ErrorProvider báo lỗi từng ô<br>- [ ] Menu có phím tắt Ctrl+N, Ctrl+S...<br>- [ ] Tìm kiếm không giật/đơ CSDL |
| **4** | **I/O, Async & Tác vụ nền** *(Ch.5)* | Import/Export CSV/Excel/JSON; tác vụ nặng chạy Task.Run / async, cập nhật ProgressBar trên StatusStrip, giao diện không bị đơ/freeze. | **1.0** | - [ ] Import/Export file chạy đúng dữ liệu<br>- [ ] Có ProgressBar chạy %, UI vẫn tương tác được |
| **5** | **Đồ họa GDI+ & Trực quan hóa** *(Ch.6)* | Biểu đồ Dashboard lấy dữ liệu thực từ CSDL; vẽ GDI+ (bật DoubleBuffered) hoặc LiveCharts2; không lỗi nhấp nháy, không tràn khung khi phóng to. | **0.5** | - [ ] Biểu đồ thể hiện dữ liệu thật từ DB<br>- [ ] Phóng to/thu nhỏ mượt mà, không flicker |
| **6** | **Cơ sở dữ liệu: EF Core & ADO.NET** *(Ch.7)* | CRUD đầy đủ qua EF Core (Code First + Migrations); >= 2 bảng khóa ngoại (Include/Join); có async DB; minh họa thêm ADO.NET thuần cho >= 1 chức năng. | **2.0** | - [ ] Có file Migrations trong DAL<br>- [ ] CRUD hoạt động trơn tru không lỗi<br>- [ ] Có ít nhất 1 hàm dùng SqlCommand/SqlDataReader |
| **7** | **Báo cáo & Xuất tài liệu** *(Ch.8)* | Xuất báo cáo PDF và Excel theo bộ lọc điều kiện (thời gian, giá trị); có PrintPreviewDialog hoặc FastReport chuyên nghiệp. | **1.5** | - [ ] Xuất Excel định dạng đẹp (ClosedXML/EPPlus)<br>- [ ] Xuất PDF đúng số liệu đã lọc<br>- [ ] Có xem trước khi in (Print Preview) |
| **8** | **Triển khai, Logging & Tích hợp** *(Ch.9)* | Publish chạy được trên máy khác (Self-Contained); ghi log lỗi Serilog; xử lý ngoại lệ toàn cục; tích hợp gọi 1 REST API ngoài. | **1.0** | - [ ] Có thư mục Publish chạy được không cần SDK<br>- [ ] Có thư mục ghi log file khi có lỗi<br>- [ ] Có gọi REST API (HttpClient) |
| **9** | **Trình bày, Báo cáo & Phân quyền** | Phân quyền 2 vai trò; báo cáo Word/PDF có ERD và kiến trúc; phân công công việc rõ ràng giữa các thành viên; demo trôi chảy. | **1.0** | - [ ] Phân quyền đăng nhập User vs Admin<br>- [ ] Báo cáo đầy đủ sơ đồ ERD & phân tầng |
| **--** | **TỔNG ĐIỂM** | **Đạt trọn vẹn toàn bộ các mục tiêu môn học** | **10.0** | |
