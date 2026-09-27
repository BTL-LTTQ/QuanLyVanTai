using System.Drawing;

public static class ThemeConfig
{
    // Bảng màu (Color Palette)
    public static Color PrimaryColor = ColorTranslator.FromHtml("#2563EB"); // Xanh dương đậm
    public static Color SecondaryColor = ColorTranslator.FromHtml("#64748B"); // Xám nhạt
    public static Color BackgroundColor = ColorTranslator.FromHtml("#F8FAFC"); // Trắng xám
    public static Color TextMain = ColorTranslator.FromHtml("#0F172A"); // Đen/Xám đậm chữ chính
    public static Color DangerColor = ColorTranslator.FromHtml("#EF4444"); // Đỏ (Nút xóa, cảnh báo)

    // Font chữ
    public static Font MainFont = new Font("Segoe UI", 10F, FontStyle.Regular);
    public static Font TitleFont = new Font("Segoe UI", 14F, FontStyle.Bold);
}