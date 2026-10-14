using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using QuanLyVanTai.BLL.DTOs;

namespace QuanLyVanTai.UI.Components
{
    /// <summary>
    /// Custom Control GDI+ vẽ sơ đồ ghế xe khách trực quan, chống giật hình (Double Buffered),
    /// tương tác click chọn ghế, hover hiệu ứng, hỗ trợ xe ghế ngồi, limousine và xe 2 tầng.
    /// </summary>
    public class SeatMapControl : Panel
    {
        private VehicleSeatMapDto? _seatMap;
        private readonly List<SeatVisualItem> _seats = new();
        private readonly Dictionary<SeatVisualItem, RectangleF> _seatBounds = new();
        private SeatVisualItem? _hoveredSeat;

        // Sự kiện thông báo khi danh sách ghế được chọn thay đổi
        public event EventHandler<List<SeatVisualItem>>? SelectionChanged;

        public SeatMapControl()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(248, 250, 252); // Nền xám dịu chuẩn ThemeConfig
        }

        /// <summary>
        /// Nạp sơ đồ ghế và tính toán lại tọa độ hiển thị
        /// </summary>
        public void LoadSeatMap(VehicleSeatMapDto seatMap)
        {
            _seatMap = seatMap;
            _seats.Clear();
            _seatBounds.Clear();
            _hoveredSeat = null;

            if (_seatMap?.Seats != null)
            {
                _seats.AddRange(_seatMap.Seats);
            }

            RecalculateLayout();
            this.Invalidate();
        }

        /// <summary>
        /// Lấy danh sách các ghế đang được chọn
        /// </summary>
        public List<SeatVisualItem> GetSelectedSeats()
        {
            return _seats.Where(s => s.State == SeatState.Selected).ToList();
        }

        /// <summary>
        /// Hủy chọn toàn bộ các ghế đang chọn
        /// </summary>
        public void ClearSelection()
        {
            bool hasChanged = false;
            foreach (var seat in _seats)
            {
                if (seat.State == SeatState.Selected)
                {
                    seat.State = SeatState.Available;
                    hasChanged = true;
                }
            }

            if (hasChanged)
            {
                this.Invalidate();
                SelectionChanged?.Invoke(this, GetSelectedSeats());
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RecalculateLayout();
            this.Invalidate();
        }

        /// <summary>
        /// Tính toán tọa độ và kích thước cho từng ghế dựa theo số tầng và cấu trúc cột
        /// </summary>
        private void RecalculateLayout()
        {
            _seatBounds.Clear();
            if (_seatMap == null || _seats.Count == 0)
            {
                this.AutoScrollMinSize = Size.Empty;
                return;
            }

            bool isSleeper = (_seatMap.LayoutType == VehicleLayoutType.Sleeper24 ||
                              _seatMap.LayoutType == VehicleLayoutType.Sleeper34);

            // Kích thước chuẩn cho từng ô ghế
            float seatWidth = isSleeper ? 58f : 52f;
            float seatHeight = isSleeper ? 72f : 60f;
            float gapX = 14f;
            float gapY = 16f;

            int totalFloors = _seatMap.TotalFloors;
            int maxRow = _seats.Max(s => s.Row);
            int maxCol = _seats.Max(s => s.Column);

            float floorWidth = (maxCol * (seatWidth + gapX)) + 40f;
            float floorHeight = (maxRow * (seatHeight + gapY)) + 90f; // Bổ sung khoảng trống cho đầu xe & vô lăng

            float startX = 25f;
            float startY = 20f;

            for (int f = 1; f <= totalFloors; f++)
            {
                float floorOffsetX = startX + (f - 1) * (floorWidth + 40f);
                float floorOffsetY = startY;

                var floorSeats = _seats.Where(s => s.Floor == f).ToList();
                foreach (var seat in floorSeats)
                {
                    float x = floorOffsetX + 20f + (seat.Column - 1) * (seatWidth + gapX);
                    float y = floorOffsetY + 70f + (seat.Row - 1) * (seatHeight + gapY);

                    _seatBounds[seat] = new RectangleF(x, y, seatWidth, seatHeight);
                }
            }

            int requiredWidth = (int)(startX + totalFloors * (floorWidth + 40f) + 30f);
            int requiredHeight = (int)(startY + floorHeight + 50f);
            this.AutoScrollMinSize = new Size(requiredWidth, requiredHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Xử lý scroll offset
            g.TranslateTransform(this.AutoScrollPosition.X, this.AutoScrollPosition.Y);

            if (_seatMap == null || _seats.Count == 0)
            {
                using var fontEmpty = new Font("Segoe UI", 11f, FontStyle.Italic);
                using var brushEmpty = new SolidBrush(Color.FromArgb(148, 163, 184));
                g.DrawString("Chưa có chuyến xe được chọn hoặc sơ đồ trống.", fontEmpty, brushEmpty, 30, 40);
                return;
            }

            int totalFloors = _seatMap.TotalFloors;
            bool isSleeper = (_seatMap.LayoutType == VehicleLayoutType.Sleeper24 ||
                              _seatMap.LayoutType == VehicleLayoutType.Sleeper34);

            float seatWidth = isSleeper ? 58f : 52f;
            float seatHeight = isSleeper ? 72f : 60f;
            float gapX = 14f;
            float gapY = 16f;

            int maxRow = _seats.Max(s => s.Row);
            int maxCol = _seats.Max(s => s.Column);
            float floorWidth = (maxCol * (seatWidth + gapX)) + 40f;
            float floorHeight = (maxRow * (seatHeight + gapY)) + 90f;

            float startX = 25f;
            float startY = 20f;

            // 1. Vẽ khung thân xe cho từng tầng
            for (int f = 1; f <= totalFloors; f++)
            {
                float floorX = startX + (f - 1) * (floorWidth + 40f);
                float floorY = startY;
                RectangleF busBody = new RectangleF(floorX, floorY, floorWidth, floorHeight);

                // Khung viền thân xe bo góc mềm
                using (var path = CreateRoundedRectangle(busBody, 18))
                using (var bgBrush = new SolidBrush(Color.White))
                using (var borderPen = new Pen(Color.FromArgb(203, 213, 225), 2f))
                {
                    g.FillPath(bgBrush, path);
                    g.DrawPath(borderPen, path);
                }

                // Tiêu đề Tầng (VD: TẦNG DƯỚI (Tầng 1) / TẦNG TRÊN (Tầng 2))
                string floorTitle = (totalFloors > 1)
                    ? (f == 1 ? "TẦNG 1 (DƯỚI) - DÃY A" : "TẦNG 2 (TRÊN) - DÃY B")
                    : "KHOANG HÀNH KHÁCH";

                using (var titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
                using (var titleBrush = new SolidBrush(Color.FromArgb(30, 41, 59)))
                {
                    var titleSize = g.MeasureString(floorTitle, titleFont);
                    g.DrawString(floorTitle, titleFont, titleBrush, floorX + (floorWidth - titleSize.Width) / 2, floorY + 12);
                }

                // Vạch ngang ngăn cách tiêu đề tầng
                using (var linePen = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
                {
                    g.DrawLine(linePen, floorX + 15, floorY + 36, floorX + floorWidth - 15, floorY + 36);
                }

                // Biểu tượng Vô lăng tài xế ở góc trên bên trái tầng 1
                if (f == 1)
                {
                    DrawSteeringWheel(g, floorX + 22, floorY + 44);
                }

                // Cửa lên xuống
                DrawDoorIcon(g, floorX + floorWidth - 65, floorY + 44);
            }

            // 2. Vẽ từng ghế/giường
            foreach (var seat in _seats)
            {
                if (!_seatBounds.TryGetValue(seat, out var bounds)) continue;

                bool isHovered = (_hoveredSeat == seat);
                DrawSingleSeat(g, seat, bounds, isHovered, isSleeper);
            }
        }

        /// <summary>
        /// Vẽ biểu tượng vô lăng xe bằng GDI+
        /// </summary>
        private static void DrawSteeringWheel(Graphics g, float x, float y)
        {
            RectangleF wheelRect = new RectangleF(x, y, 22, 22);

            using var penWheel = new Pen(Color.FromArgb(71, 85, 105), 2f);
            using var brushCenter = new SolidBrush(Color.FromArgb(71, 85, 105));
            using var fontLabel = new Font("Segoe UI", 8f, FontStyle.Bold);
            using var textBrush = new SolidBrush(Color.FromArgb(100, 116, 139));

            // Vành vô lăng
            g.DrawEllipse(penWheel, wheelRect);

            // Tâm và 3 nan hoa
            float cx = wheelRect.X + wheelRect.Width / 2;
            float cy = wheelRect.Y + wheelRect.Height / 2;
            g.FillEllipse(brushCenter, cx - 3, cy - 3, 6, 6);
            g.DrawLine(penWheel, cx, cy, cx, wheelRect.Bottom);
            g.DrawLine(penWheel, cx, cy, wheelRect.Left + 2, cy - 2);
            g.DrawLine(penWheel, cx, cy, wheelRect.Right - 2, cy - 2);

            g.DrawString("Tài xế", fontLabel, textBrush, x + 26, y + 4);
        }

        /// <summary>
        /// Vẽ biểu tượng cửa lên xuống
        /// </summary>
        private static void DrawDoorIcon(Graphics g, float x, float y)
        {
            RectangleF doorRect = new RectangleF(x, y, 38, 18);
            using var penDoor = new Pen(Color.FromArgb(148, 163, 184), 1.5f);
            using var fontDoor = new Font("Segoe UI", 7.5f, FontStyle.Regular);
            using var textBrush = new SolidBrush(Color.FromArgb(100, 116, 139));

            g.DrawRectangle(penDoor, doorRect.X, doorRect.Y, doorRect.Width, doorRect.Height);
            g.DrawString("Cửa lên", fontDoor, textBrush, x + 3, y + 2);
        }

        /// <summary>
        /// Vẽ chi tiết một vị trí ghế/giường theo vector GDI+
        /// </summary>
        private static void DrawSingleSeat(Graphics g, SeatVisualItem seat, RectangleF bounds, bool isHovered, bool isSleeper)
        {
            // Bảng màu trạng thái chuẩn UI/UX
            Color fillColor;
            Color borderColor;
            Color textColor;
            float borderWidth = 1.5f;

            switch (seat.State)
            {
                case SeatState.Selected:
                    fillColor = Color.FromArgb(254, 243, 199);  // Cam nhạt
                    borderColor = Color.FromArgb(245, 158, 11); // Cam viền đậm
                    textColor = Color.FromArgb(180, 83, 9);     // Nâu cam
                    borderWidth = 2f;
                    break;

                case SeatState.Booked:
                    fillColor = Color.FromArgb(226, 232, 240);  // Xám tro
                    borderColor = Color.FromArgb(148, 163, 184);// Xám viền
                    textColor = Color.FromArgb(100, 116, 139);  // Chữ xám
                    break;

                case SeatState.Holding:
                    fillColor = Color.FromArgb(254, 226, 226);  // Đỏ nhạt
                    borderColor = Color.FromArgb(239, 68, 68);  // Đỏ viền
                    textColor = Color.FromArgb(185, 28, 28);
                    break;

                case SeatState.Available:
                default:
                    if (isHovered)
                    {
                        fillColor = Color.FromArgb(186, 230, 253);  // Xanh sáng khi hover
                        borderColor = Color.FromArgb(2, 132, 199);  // Xanh đậm viền
                        textColor = Color.FromArgb(3, 105, 161);
                        borderWidth = 2f;
                    }
                    else
                    {
                        fillColor = Color.FromArgb(224, 242, 254);  // Xanh băng nhẹ
                        borderColor = Color.FromArgb(2, 132, 199);  // Xanh dương 1.5px
                        textColor = Color.FromArgb(3, 105, 161);
                    }
                    break;
            }

            if (isSleeper)
            {
                // Vẽ Cabin VIP / Giường nằm (hình chữ nhật bo góc dài, có gối đầu và đệm nằm)
                using var pathBody = CreateRoundedRectangle(bounds, 8);
                using var brushBody = new SolidBrush(fillColor);
                using var penBorder = new Pen(borderColor, borderWidth);
                g.FillPath(brushBody, pathBody);
                g.DrawPath(penBorder, pathBody);

                // Gối đầu ở đỉnh
                RectangleF pillowRect = new RectangleF(bounds.X + 6, bounds.Y + 4, bounds.Width - 12, 14);
                using var pathPillow = CreateRoundedRectangle(pillowRect, 4);
                using var brushPillow = new SolidBrush(Color.FromArgb(255, 255, 255, 180));
                using var penPillow = new Pen(borderColor, 1f);
                g.FillPath(brushPillow, pathPillow);
                g.DrawPath(penPillow, pathPillow);

                // Mã ghế ở giữa
                using var fontCode = new Font("Segoe UI", 9f, FontStyle.Bold);
                using var brushText = new SolidBrush(textColor);
                var textSize = g.MeasureString(seat.SeatCode, fontCode);
                g.DrawString(seat.SeatCode, fontCode, brushText,
                    bounds.X + (bounds.Width - textSize.Width) / 2,
                    bounds.Y + (bounds.Height - textSize.Height) / 2 + 5);
            }
            else
            {
                // Vẽ Ghế ngồi tiêu chuẩn (Tựa lưng bo trên, Đệm ngồi bo dưới, 2 tay vịn hai bên)
                float armWidth = 6f;
                float backHeight = bounds.Height * 0.62f;
                float seatHeight = bounds.Height - backHeight - 2f;

                // 1. Hai tay vịn bên trái và bên phải
                RectangleF armLeft = new RectangleF(bounds.X, bounds.Y + 6, armWidth, bounds.Height - 12);
                RectangleF armRight = new RectangleF(bounds.Right - armWidth, bounds.Y + 6, armWidth, bounds.Height - 12);

                using (var armPathL = CreateRoundedRectangle(armLeft, 3))
                using (var armPathR = CreateRoundedRectangle(armRight, 3))
                using (var armBrush = new SolidBrush(Color.FromArgb(203, 213, 225)))
                using (var armPen = new Pen(borderColor, 1f))
                {
                    g.FillPath(armBrush, armPathL);
                    g.DrawPath(armPen, armPathL);
                    g.FillPath(armBrush, armPathR);
                    g.DrawPath(armPen, armPathR);
                }

                // 2. Tựa lưng (ở trên, bo góc đỉnh)
                RectangleF backRect = new RectangleF(bounds.X + armWidth + 2, bounds.Y, bounds.Width - (armWidth * 2) - 4, backHeight);
                using (var backPath = CreateRoundedRectangle(backRect, 6, topOnly: true))
                using (var backBrush = new SolidBrush(fillColor))
                using (var backPen = new Pen(borderColor, borderWidth))
                {
                    g.FillPath(backBrush, backPath);
                    g.DrawPath(backPen, backPath);
                }

                // 3. Đệm ngồi (ở dưới)
                RectangleF cushionRect = new RectangleF(bounds.X + armWidth + 2, bounds.Y + backHeight + 1, bounds.Width - (armWidth * 2) - 4, seatHeight);
                using (var cushionPath = CreateRoundedRectangle(cushionRect, 4))
                using (var cushionBrush = new SolidBrush(fillColor))
                using (var cushionPen = new Pen(borderColor, borderWidth))
                {
                    g.FillPath(cushionBrush, cushionPath);
                    g.DrawPath(cushionPen, cushionPath);
                }

                // 4. Nhãn số ghế ở giữa tựa lưng
                using var fontCode = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                using var brushText = new SolidBrush(textColor);
                var textSize = g.MeasureString(seat.SeatCode, fontCode);
                g.DrawString(seat.SeatCode, fontCode, brushText,
                    backRect.X + (backRect.Width - textSize.Width) / 2,
                    backRect.Y + (backRect.Height - textSize.Height) / 2);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Tính vị trí thực theo scroll
            Point realPoint = new Point(e.X - this.AutoScrollPosition.X, e.Y - this.AutoScrollPosition.Y);

            SeatVisualItem? matchSeat = null;
            foreach (var kvp in _seatBounds)
            {
                if (kvp.Value.Contains(realPoint))
                {
                    matchSeat = kvp.Key;
                    break;
                }
            }

            if (matchSeat != _hoveredSeat)
            {
                _hoveredSeat = matchSeat;
                this.Invalidate();
            }

            // Đổi con trỏ chuột
            if (_hoveredSeat != null)
            {
                if (_hoveredSeat.State == SeatState.Booked)
                    this.Cursor = Cursors.No;
                else
                    this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_hoveredSeat != null)
            {
                _hoveredSeat = null;
                this.Invalidate();
            }
            this.Cursor = Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (e.Button != MouseButtons.Left) return;

            Point realPoint = new Point(e.X - this.AutoScrollPosition.X, e.Y - this.AutoScrollPosition.Y);

            foreach (var kvp in _seatBounds)
            {
                if (kvp.Value.Contains(realPoint))
                {
                    var seat = kvp.Key;

                    // Không thể chọn ghế đã bán hoặc giữ chỗ
                    if (seat.State == SeatState.Booked || seat.State == SeatState.Holding)
                    {
                        System.Media.SystemSounds.Beep.Play();
                        return;
                    }

                    // Chuyển đổi trạng thái Available <-> Selected
                    if (seat.State == SeatState.Available)
                    {
                        seat.State = SeatState.Selected;
                    }
                    else if (seat.State == SeatState.Selected)
                    {
                        seat.State = SeatState.Available;
                    }

                    this.Invalidate();
                    SelectionChanged?.Invoke(this, GetSelectedSeats());
                    break;
                }
            }
        }

        private static GraphicsPath CreateRoundedRectangle(RectangleF rect, float radius, bool topOnly = false)
        {
            var path = new GraphicsPath();
            float diameter = radius * 2;

            if (topOnly)
            {
                path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
                path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
                path.AddLine(rect.Right, rect.Bottom, rect.X, rect.Bottom);
                path.CloseFigure();
                return path;
            }

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
