using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Core.Configs
{
    public static class ThemeConfig
    {
        // Bảng màu (Color Palette)
        public static Color PrimaryColor = ColorTranslator.FromHtml("#2563EB"); // Xanh dương đậm
        public static Color SecondaryColor = ColorTranslator.FromHtml("#64748B"); // Xám nhạt
        public static Color BackgroundColor = ColorTranslator.FromHtml("#F8FAFC"); // Trắng xám
        public static Color PanelBackgroundColor = Color.White;
        public static Color TextMain = ColorTranslator.FromHtml("#0F172A"); // Đen/Xám đậm chữ chính
        public static Color DangerColor = ColorTranslator.FromHtml("#EF4444"); // Đỏ (Nút xóa, cảnh báo)
        public static Color SuccessColor = ColorTranslator.FromHtml("#10B981"); // Xanh lá
        public static Color WarningColor = ColorTranslator.FromHtml("#F59E0B"); // Cam

        // Font chữ
        public static Font MainFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static Font TitleFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static Font ButtonFont = new Font("Segoe UI", 9F, FontStyle.Bold);

        // Helper methods for styling UI controls
        public static void StyleButton(IconButton button, Color bgColor, Color iconColor, IconChar icon)
        {
            button.BackColor = bgColor;
            button.ForeColor = Color.White;
            button.IconChar = icon;
            button.IconColor = iconColor;
            button.IconSize = 18;
            button.Font = ButtonFont;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.TextImageRelation = TextImageRelation.ImageBeforeText;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Padding = new Padding(4, 0, 4, 0);
            button.Size = new Size(110, 36);
        }
        
        public static void StylePrimaryButton(IconButton button, IconChar icon)
        {
            StyleButton(button, PrimaryColor, Color.White, icon);
        }
        
        public static void StyleDangerButton(IconButton button, IconChar icon)
        {
            StyleButton(button, DangerColor, Color.White, icon);
        }
        
        public static void StyleSecondaryButton(IconButton button, IconChar icon)
        {
            StyleButton(button, SecondaryColor, Color.White, icon);
        }
        
        public static void StyleSuccessButton(IconButton button, IconChar icon)
        {
            StyleButton(button, SuccessColor, Color.White, icon);
        }
    }
}