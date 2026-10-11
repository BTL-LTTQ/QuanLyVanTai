using Core.Configs;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class UcDashboard : UserControl
    {
        // ─── Chart data ───────────────────────────────────────────────
        private readonly List<DayData> _chartData = new()
        {
            new DayData("19/09", 95,  80,  70),
            new DayData("20/09", 105, 90,  85),
            new DayData("21/09", 85,  75,  80),
            new DayData("22/09", 120, 110, 95),
            new DayData("23/09", 130, 120, 100),
            new DayData("24/09", 110, 95,  90),
            new DayData("25/09", 145, 130, 110),
            new DayData("26/09", 160, 145, 120),
            new DayData("27/09", 150, 135, 115),
            new DayData("28/09", 175, 160, 130),
        };

        // ─── Bus GPS data ─────────────────────────────────────────────
        private readonly List<BusInfo> _buses = new()
        {
            new BusInfo("51B-184.26", "T-02", "Bến Thành → Củ Chi",       62, 10.762, 106.660, BusState.Running),
            new BusInfo("50F-023.17", "T-05", "Sài Gòn → Bình Dương",     58, 10.852, 106.621, BusState.Running),
            new BusInfo("51B-330.68", "T-01", "Miền Đông → Vũng Tàu",     45, 10.774, 106.700, BusState.Running),
            new BusInfo("51B-D3",     "T-04", "Miền Tây → Cần Thơ",       72, 10.035, 105.769, BusState.Warning),
            new BusInfo("52A-108.45", "T-03", "Chợ Lớn → Long An",        55, 10.685, 106.434, BusState.Stopped),
        };

        public UcDashboard()
        {
            InitializeComponent();
            Build();
        }

        // ═══════════════════════════════════════════════════════════════
        //  LAYOUT
        // ═══════════════════════════════════════════════════════════════
        private void Build()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = ThemeConfig.MainFont;

            // ── outer TableLayout: rows = header | stats | body | footer
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 1,
                BackColor = Color.Transparent,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));   // header
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 115));  // stats cards
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));   // chart + map
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));   // footer
            this.Controls.Add(root);

            root.Controls.Add(BuildHeader(),    0, 0);
            root.Controls.Add(BuildStatCards(), 0, 1);
            root.Controls.Add(BuildBody(),      0, 2);
            root.Controls.Add(BuildFooter(),    0, 3);
        }

        // ── Header ────────────────────────────────────────────────────
        private Panel BuildHeader()
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(20, 0, 20, 0) };
            p.Controls.Add(new Label
            {
                Text = "Operations Dashboard",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(20, 12),
                AutoSize = true
            });
            p.Controls.Add(new Label
            {
                Text = $"{DateTime.Now:dddd, dd MMMM yyyy}  –  TP. Hồ Chí Minh",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(22, 44),
                AutoSize = true
            });
            return p;
        }

        // ── Stat Cards ────────────────────────────────────────────────
        private Panel BuildStatCards()
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(20, 10, 20, 10) };

            var cards = new (string title, string value, string sub, IconChar icon, Color color)[]
            {
                ("Today's Revenue",  "₫128.6M", "+12.4% vs yesterday",   IconChar.MoneyBillWave, Color.FromArgb(59,130,246)),
                ("Tickets Sold",     "1,248",    "88 online · 162 counter", IconChar.Ticket,        Color.FromArgb(16,185,129)),
                ("Buses Active",     "42 / 48",  "3 delayed · 3 service",  IconChar.Bus,           Color.FromArgb(99,102,241)),
                ("Passengers",       "3,876",    "Current occupancy 79%",  IconChar.Users,         Color.FromArgb(239,68,68)),
            };

            int x = 0;
            foreach (var (title, value, sub, icon, color) in cards)
            {
                var card = MakeStatCard(title, value, sub, icon, color);
                card.Location = new Point(x, 0);
                p.Controls.Add(card);
                x += 285;
            }
            return p;
        }

        private Panel MakeStatCard(string title, string value, string sub, IconChar icon, Color color)
        {
            var card = new Panel { Size = new Size(275, 95), BackColor = Color.White };

            // left accent bar
            card.Controls.Add(new Panel { Size = new Size(4, 95), Location = new Point(0, 0), BackColor = color });

            // icon box
            card.Controls.Add(new IconPictureBox
            {
                IconChar = icon, IconColor = color, IconSize = 28,
                Size = new Size(44, 44), Location = new Point(14, 26),
                BackColor = Color.FromArgb(color.R, 240, 253, 244)
            });

            card.Controls.Add(new Label { Text = title, Font = new Font("Segoe UI", 8.5F), ForeColor = Color.FromArgb(100,116,139), Location = new Point(68, 16), AutoSize = true });
            card.Controls.Add(new Label { Text = value, Font = new Font("Segoe UI", 17F, FontStyle.Bold), ForeColor = color, Location = new Point(66, 34), AutoSize = true });
            card.Controls.Add(new Label { Text = sub,   Font = new Font("Segoe UI", 7.5F), ForeColor = Color.FromArgb(148,163,184), Location = new Point(68, 72), AutoSize = true });

            card.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };
            return card;
        }

        // ── Body: Chart (left) + Map (right) ─────────────────────────
        private TableLayoutPanel BuildBody()
        {
            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 2,
                BackColor = Color.Transparent,
                Padding = new Padding(20, 10, 20, 10)
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));

            tbl.Controls.Add(BuildChartPanel(), 0, 0);
            tbl.Controls.Add(BuildMapPanel(),   1, 0);
            return tbl;
        }

        // ── Chart Panel ───────────────────────────────────────────────
        private Panel BuildChartPanel()
        {
            var wrap = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0, 0, 8, 0) };

            wrap.Controls.Add(new Label
            {
                Text = "Revenue & Ticket Performance — Last 10 Days",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(16, 14), AutoSize = true
            });
            wrap.Controls.Add(new Label
            {
                Text = "Unit: million VNĐ",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(16, 36), AutoSize = true
            });

            // legend
            int lx = 320;
            foreach (var (txt, col) in new (string, Color)[] { ("Revenue", Color.FromArgb(16,185,129)), ("Ticket sales", Color.FromArgb(59,130,246)), ("Operating cost", Color.FromArgb(239,68,68)) })
            {
                wrap.Controls.Add(new Panel { Size = new Size(10, 3), Location = new Point(lx, 22), BackColor = col });
                wrap.Controls.Add(new Label { Text = txt, Font = new Font("Segoe UI", 8F), ForeColor = Color.FromArgb(100,116,139), Location = new Point(lx + 14, 16), AutoSize = true });
                lx += 110;
            }

            // actual drawing area
            var canvas = new Panel { Dock = DockStyle.None, BackColor = Color.White, Left = 10, Top = 55, Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom };
            canvas.Paint += Canvas_Paint;
            wrap.Controls.Add(canvas);

            // resize canvas with parent
            wrap.Resize += (_, _) =>
            {
                canvas.Size = new Size(wrap.Width - 20, wrap.Height - 65);
            };

            return wrap;
        }

        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            if (sender is not Panel canvas || _chartData.Count == 0) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int W = canvas.Width, H = canvas.Height;
            const int PL = 48, PR = 20, PT = 10, PB = 30;
            int cw = W - PL - PR, ch = H - PT - PB;

            float maxVal = _chartData.Max(d => Math.Max(d.Revenue, Math.Max(d.Tickets, d.Cost)));
            float step = cw / (float)(_chartData.Count - 1);

            // grid
            using var gridPen = new Pen(Color.FromArgb(235, 237, 241), 1);
            for (int i = 0; i <= 4; i++)
            {
                int gy = PT + ch * i / 4;
                g.DrawLine(gridPen, PL, gy, W - PR, gy);
                string label = ((int)(maxVal * (4 - i) / 4)).ToString();
                using var lbr = new SolidBrush(Color.FromArgb(150, 163, 175));
                g.DrawString(label, new Font("Segoe UI", 7.5F), lbr, 2, gy - 8);
            }

            // x-axis labels
            for (int i = 0; i < _chartData.Count; i++)
            {
                float gx = PL + step * i;
                using var lbr = new SolidBrush(Color.FromArgb(150, 163, 175));
                g.DrawString(_chartData[i].Day, new Font("Segoe UI", 7.5F), lbr, gx - 14, H - PB + 4);
            }

            // lines
            DrawLine(g, _chartData.Select(d => d.Revenue).ToList(), Color.FromArgb(16, 185, 129), PL, PT, step, ch, maxVal);
            DrawLine(g, _chartData.Select(d => d.Tickets).ToList(), Color.FromArgb(59, 130, 246), PL, PT, step, ch, maxVal);
            DrawLine(g, _chartData.Select(d => d.Cost).ToList(),    Color.FromArgb(239, 68, 68),  PL, PT, step, ch, maxVal);
        }

        private static void DrawLine(Graphics g, List<float> vals, Color col, int PL, int PT, float step, int ch, float maxVal)
        {
            var pts = vals.Select((v, i) => new PointF(PL + step * i, PT + ch - ch * v / maxVal)).ToArray();
            using var pen = new Pen(col, 2.5f);
            g.DrawLines(pen, pts);
            using var br = new SolidBrush(col);
            foreach (var pt in pts) g.FillEllipse(br, pt.X - 3.5f, pt.Y - 3.5f, 7, 7);
        }

        // ── Map Panel ─────────────────────────────────────────────────
        private Panel BuildMapPanel()
        {
            var wrap = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(8, 0, 0, 0) };

            // title row
            wrap.Controls.Add(new Label
            {
                Text = "Real-time GPS — 42 buses online",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(16, 14), AutoSize = true
            });
            wrap.Controls.Add(new Label
            {
                Text = "● LIVE",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(16, 185, 129),
                Location = new Point(16, 36), AutoSize = true
            });

            // ── Map canvas (top half) ─────────────────────────────
            var mapCanvas = new Panel
            {
                Left = 10, Top = 55,
                BackColor = Color.FromArgb(235, 242, 248),
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            mapCanvas.Paint += (s, e) => PaintMap(e.Graphics, mapCanvas.Width, mapCanvas.Height);
            wrap.Controls.Add(mapCanvas);

            // ── Bus list (bottom half) ────────────────────────────
            var scroll = new Panel
            {
                Left = 10,
                BackColor = Color.Transparent,
                AutoScroll = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            int by = 0;
            foreach (var bus in _buses)
            {
                var card = MakeBusCard(bus);
                card.Top = by;
                scroll.Controls.Add(card);
                by += card.Height + 4;
            }
            wrap.Controls.Add(scroll);

            // size both on resize
            wrap.Resize += (_, _) =>
            {
                int mapH = (int)(wrap.Height * 0.45);
                mapCanvas.Size = new Size(wrap.Width - 20, mapH);
                scroll.Location = new Point(10, 55 + mapH + 8);
                scroll.Size = new Size(wrap.Width - 20, wrap.Height - 55 - mapH - 20);
                // fix bus card widths
                foreach (Control c in scroll.Controls) c.Width = scroll.Width - 4;
            };

            return wrap;
        }

        // ── GDI map drawing ──────────────────────────────────────────
        private void PaintMap(Graphics g, int W, int H)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // background
            using (var bg = new LinearGradientBrush(new Rectangle(0, 0, W, H),
                Color.FromArgb(220, 233, 245), Color.FromArgb(200, 218, 235), LinearGradientMode.Vertical))
                g.FillRectangle(bg, 0, 0, W, H);

            // grid
            using (var gp = new Pen(Color.FromArgb(170, 185, 200), 1) { DashStyle = DashStyle.Dot })
            {
                for (int i = 1; i < 5; i++) g.DrawLine(gp, W * i / 5, 0, W * i / 5, H);
                for (int i = 1; i < 4; i++) g.DrawLine(gp, 0, H * i / 4, W, H * i / 4);
            }

            // Vietnam outline (simplified, normalized 0-1)
            PointF[] shape =
            {
                new(0.47f, 0.12f), new(0.55f, 0.25f), new(0.60f, 0.42f),
                new(0.58f, 0.65f), new(0.52f, 0.78f), new(0.44f, 0.80f),
                new(0.36f, 0.72f), new(0.32f, 0.55f), new(0.34f, 0.32f),
                new(0.40f, 0.17f)
            };
            var shapeAbs = shape.Select(p => new PointF(p.X * W, p.Y * H)).ToArray();
            using (var sb = new SolidBrush(Color.FromArgb(90, 170, 200, 220))) g.FillPolygon(sb, shapeAbs);
            using (var op = new Pen(Color.FromArgb(130, 150, 170), 2)) g.DrawPolygon(op, shapeAbs);

            // Cities
            var cities = new (string name, float lat, float lon, bool big)[]
            {
                ("TP.HCM",       10.776f, 106.700f, true),
                ("Bình Dương",   11.000f, 106.620f, false),
                ("Long An",      10.690f, 106.240f, false),
                ("Vũng Tàu",     10.350f, 107.085f, false),
                ("Cần Thơ",      10.035f, 105.788f, false),
            };

            const float LAT0 = 9.8f, LAT1 = 11.2f, LON0 = 105.4f, LON1 = 107.3f;
            PointF Proj(float lat, float lon) => new PointF(
                (lon - LON0) / (LON1 - LON0) * W,
                (1f - (lat - LAT0) / (LAT1 - LAT0)) * H);

            // route lines
            using (var rp = new Pen(Color.FromArgb(120, 100, 140, 200), 2) { DashStyle = DashStyle.Dash })
            {
                var hcm = Proj(10.776f, 106.700f);
                foreach (var c in cities.Where(c => !c.big))
                    g.DrawLine(rp, hcm, Proj(c.lat, c.lon));
            }

            foreach (var (name, lat, lon, big) in cities)
            {
                var pt = Proj(lat, lon);
                int r = big ? 8 : 5;
                var col = big ? Color.FromArgb(59, 130, 246) : Color.FromArgb(100, 116, 139);
                using (var sb = new SolidBrush(col)) g.FillEllipse(sb, pt.X - r, pt.Y - r, r * 2, r * 2);
                using (var wp = new Pen(Color.White, 2)) g.DrawEllipse(wp, pt.X - r, pt.Y - r, r * 2, r * 2);
                using (var tf = new Font("Segoe UI", big ? 8f : 7.5f, big ? FontStyle.Bold : FontStyle.Regular))
                using (var tb = new SolidBrush(Color.FromArgb(30, 41, 59)))
                {
                    var sz = g.MeasureString(name, tf);
                    using (var bgb = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
                        g.FillRectangle(bgb, pt.X - sz.Width / 2 - 2, pt.Y + r + 2, sz.Width + 4, sz.Height);
                    g.DrawString(name, tf, tb, pt.X - sz.Width / 2, pt.Y + r + 2);
                }
            }

            // Bus markers
            foreach (var bus in _buses)
            {
                var pt = Proj((float)bus.Lat, (float)bus.Lon);
                Color bc = bus.State switch
                {
                    BusState.Running => Color.FromArgb(16, 185, 129),
                    BusState.Stopped => Color.FromArgb(245, 158, 11),
                    BusState.Warning => Color.FromArgb(239, 68, 68),
                    _ => Color.FromArgb(148, 163, 184)
                };

                // pulse ring
                using (var pp = new Pen(Color.FromArgb(80, bc), 2)) g.DrawEllipse(pp, pt.X - 14, pt.Y - 14, 28, 28);
                // fill
                using (var sb = new SolidBrush(bc)) g.FillEllipse(sb, pt.X - 10, pt.Y - 10, 20, 20);
                // border
                using (var wp = new Pen(Color.White, 2)) g.DrawEllipse(wp, pt.X - 10, pt.Y - 10, 20, 20);
                // label
                using (var tf = new Font("Segoe UI", 7f, FontStyle.Bold))
                using (var tb = new SolidBrush(Color.White))
                {
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(bus.Plate[^2..], tf, tb, pt.X, pt.Y, sf);
                }
                // plate below marker
                using (var tf = new Font("Segoe UI", 6.5f))
                {
                    var sz = g.MeasureString(bus.Plate, tf);
                    using (var bgb = new SolidBrush(Color.FromArgb(200, 30, 41, 59)))
                        g.FillRectangle(bgb, pt.X - sz.Width / 2 - 2, pt.Y + 12, sz.Width + 4, sz.Height);
                    using (var tb = new SolidBrush(Color.White))
                        g.DrawString(bus.Plate, tf, tb, pt.X - sz.Width / 2, pt.Y + 12);
                }
            }

            // coordinates label
            using (var tf = new Font("Segoe UI", 7f))
            using (var tb = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                g.DrawString($"{LON0}°E", tf, tb, 2, H - 14);
                g.DrawString($"{LON1}°E", tf, tb, W - 40, H - 14);
                g.DrawString($"{LAT1}°N", tf, tb, 2, 2);
                g.DrawString($"{LAT0}°N", tf, tb, 2, H - 24);
            }
        }

        // ── Bus card ─────────────────────────────────────────────────
        private static Panel MakeBusCard(BusInfo bus)
        {
            Color col = bus.State switch
            {
                BusState.Running => Color.FromArgb(16, 185, 129),
                BusState.Stopped => Color.FromArgb(245, 158, 11),
                BusState.Warning => Color.FromArgb(239, 68, 68),
                _ => Color.FromArgb(148, 163, 184)
            };

            var card = new Panel { Height = 62, BackColor = Color.White };
            card.Paint += (_, e) =>
            {
                using var p = new Pen(Color.FromArgb(226, 232, 240));
                e.Graphics.DrawRectangle(p, 0, 0, card.Width - 1, card.Height - 1);
            };

            // status dot
            card.Controls.Add(new Panel { Size = new Size(8, 8), Location = new Point(10, 12), BackColor = col });

            card.Controls.Add(new Label { Text = bus.Plate,  Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), ForeColor = Color.FromArgb(30,41,59), Location = new Point(26, 8),  AutoSize = true });
            card.Controls.Add(new Label { Text = $"{bus.Route} · {bus.Name}", Font = new Font("Segoe UI", 8f), ForeColor = Color.FromArgb(100,116,139), Location = new Point(26, 28), AutoSize = true });
            card.Controls.Add(new Label
            {
                Text = $"📍 {bus.Lat:F3}°, {bus.Lon:F3}°",
                Font = new Font("Segoe UI", 7.5f), ForeColor = Color.FromArgb(148,163,184),
                Location = new Point(26, 45), AutoSize = true
            });
            card.Controls.Add(new Label
            {
                Text = $"{bus.Speed} km/h",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold), ForeColor = col,
                BackColor = Color.FromArgb(245, 247, 250),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(210, 20), Size = new Size(70, 24)
            });

            return card;
        }

        // ── Footer ───────────────────────────────────────────────────
        private Panel BuildFooter()
        {
            var p = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            p.Controls.Add(new Label { Text = "Ready · Data synchronized", Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(100,116,139), Location = new Point(20, 10), AutoSize = true });
            p.Controls.Add(new Label
            {
                Text = $"42 vehicles online · 3 alerts",
                Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(100,116,139),
                Location = new Point(450, 10), AutoSize = true
            });
            var lblTime = new Label { Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Font = new Font("Segoe UI", 8.5f), ForeColor = Color.FromArgb(100,116,139), Location = new Point(900, 10), AutoSize = true };
            p.Controls.Add(lblTime);

            var timer = new System.Windows.Forms.Timer { Interval = 1000 };
            timer.Tick += (_, _) => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timer.Start();

            return p;
        }

        // ═══════════════════════════════════════════════════════════════
        //  DATA MODELS
        // ═══════════════════════════════════════════════════════════════
        private record DayData(string Day, float Revenue, float Tickets, float Cost);

        private record BusInfo(string Plate, string Route, string Name, int Speed, double Lat, double Lon, BusState State);

        private enum BusState { Running, Stopped, Warning, Offline }
    }
}
