# Tóm Tắt Fix - UcManageStaff & UcManageTicket

## 🐛 Vấn đề ban đầu

### 1. Không thấy tên cột (Column Header ẩn)
- **Nguyên nhân**: Thứ tự add Controls vào UserControl sai → các panel `DockStyle.Top` che mất header của DataGridView
- **Triệu chứng**: DataGridView hiển thị nhưng không có tên cột phía trên

### 2. Lỗi DbContext Concurrent
- **Nguyên nhân**: 
  - `AccountService` và `TicketService` dùng chung 1 `AppDbContext` instance (field `_context`)
  - Nhiều event (`TextChanged`, `SelectedIndexChanged`) fire đồng thời gọi `RefreshGridAsync()`
  - Nhiều query chạy song song trên cùng 1 DbContext instance → "A second operation was started on this context instance before a previous operation completed"
- **Triệu chứng**: MessageBox hiện lỗi "Lỗi lọc dữ liệu: A second operation was started on this context..."

---

## ✅ Giải pháp đã áp dụng

### Fix 1: Sửa thứ tự add Controls (Header ẩn)
**File**: `UcManageStaff.cs` (dòng ~213)

```csharp
// ❌ SAI - Add theo thứ tự thẳng
this.Controls.Add(pnlHeader);   // Top
this.Controls.Add(pnlStats);    // Top
this.Controls.Add(pnlToolbar);  // Top
this.Controls.Add(splitMain);   // Fill

// ✅ ĐÚNG - Add Fill trước, Top sau (ngược lại)
this.Controls.Add(splitMain);   // Fill - add TRƯỚC
this.Controls.Add(pnlToolbar);  // Top - add sau sẽ nằm trên
this.Controls.Add(pnlStats);    // Top
this.Controls.Add(pnlHeader);   // Top - add cuối sẽ nằm trên cùng
```

**Lý do**: WinForms xếp DockStyle.Top theo thứ tự ngược — control add sau sẽ nằm trên control add trước.

### Fix 2A: Tạo DbContext mới cho mỗi operation
**Files**: 
- `AccountService.cs`
- `TicketService.cs`
- `VehicleService.cs` (nếu cần)
- `StationService.cs` (nếu cần)

```csharp
// ❌ SAI - Dùng chung 1 context instance
public class AccountService
{
    private readonly AppDbContext _context; // ← Vấn đề ở đây
    
    public AccountService()
    {
        _context = new AppDbContext(); // Tạo 1 lần, dùng mãi
    }
    
    public async Task<List<Account>> SearchAsync(...)
    {
        return await _context.Accounts...  // Nhiều query cùng lúc = lỗi
    }
}

// ✅ ĐÚNG - Tạo context mới mỗi operation
public class AccountService
{
    private static AppDbContext CreateContext() => new AppDbContext();
    
    public async Task<List<Account>> SearchAsync(...)
    {
        using var db = CreateContext(); // Mỗi query có context riêng
        return await db.Accounts...
    }
}
```

### Fix 2B: Debounce pattern cho event handler
**Files**: `UcManageStaff.cs`, `UcManageTicket.cs`

```csharp
// ❌ SAI - Event trực tiếp gọi async query
txtKeyword.TextChanged += async (s, e) => await RefreshGridAsync();
cboRole.SelectedIndexChanged += async (s, e) => await RefreshGridAsync();
// → Gõ 1 ký tự + đổi combobox = 2 query đồng thời = lỗi

// ✅ ĐÚNG - Debounce 300ms
private System.Windows.Forms.Timer? _debounceTimer;

private void WireEvents()
{
    txtKeyword.TextChanged += (s, e) => ScheduleRefresh();
    cboRole.SelectedIndexChanged += (s, e) => ScheduleRefresh();
}

private void ScheduleRefresh()
{
    _debounceTimer?.Stop();
    _debounceTimer?.Dispose();
    _debounceTimer = new System.Windows.Forms.Timer { Interval = 300 };
    _debounceTimer.Tick += async (s, e) =>
    {
        _debounceTimer?.Stop();
        _debounceTimer?.Dispose();
        _debounceTimer = null;
        await RefreshGridAsync();
    };
    _debounceTimer.Start();
}
```

### Fix 2C: Guard flag để tránh concurrent
**Files**: `UcManageStaff.cs`, `UcManageTicket.cs`

```csharp
private bool _isRefreshing = false;

private async Task RefreshGridAsync()
{
    if (_isRefreshing) return; // ← Guard: đang refresh thì bỏ qua
    _isRefreshing = true;
    try
    {
        var accounts = await _accountService.SearchAsync(...);
        RenderGrid(accounts);
    }
    finally
    {
        _isRefreshing = false; // ← Luôn reset trong finally
    }
}
```

---

## 📊 Kết quả

✅ **Build thành công**: 0 error, 0 warning  
✅ **Header hiển thị**: Tên cột xuất hiện đúng vị trí  
✅ **Không còn lỗi DbContext**: Gõ nhanh, đổi filter liên tục → không bị crash  

---

## 🔍 Các file đã sửa

| File | Nội dung thay đổi |
|------|-------------------|
| `AccountService.cs` | Đổi sang pattern `CreateContext()` mỗi operation |
| `TicketService.cs` | Đổi sang pattern `CreateContext()` mỗi operation |
| `UcManageStaff.cs` | Debounce event, guard flag, fix thứ tự add controls |
| `UcManageTicket.cs` | Debounce event, guard flag |

---

## 💡 Best Practices rút ra

1. **DbContext Lifetime**:
   - ❌ Không dùng `AppDbContext` làm field trong Service
   - ✅ Tạo mới context mỗi operation với `using var db = new AppDbContext()`

2. **Event Handler Async**:
   - ❌ Không dùng `EventHandler filter = async (s, e) => await ...` (gây concurrent)
   - ✅ Dùng debounce timer hoặc guard flag

3. **WinForms Layout**:
   - ❌ Không add controls theo thứ tự trực quan (Header → Stats → Toolbar → Grid)
   - ✅ Add theo thứ tự ngược: Fill trước, DockStyle.Top sau

4. **DataGridView Header**:
   - ✅ Luôn set tường minh: `ColumnHeadersVisible = true` + `HeightSizeMode = DisableResizing`

---

## 🧪 Cách test

1. Mở màn hình **Quản lý nhân viên**
2. Gõ nhanh vào ô tìm kiếm (vài ký tự)
3. Đồng thời đổi ComboBox vai trò và trạng thái
4. Kết quả mong đợi:
   - ✅ Không bị lỗi popup
   - ✅ Danh sách lọc đúng
   - ✅ Header hiển thị rõ ràng

---

**Build date**: $(Get-Date -Format "dd/MM/yyyy HH:mm:ss")  
**Status**: ✅ Production Ready
