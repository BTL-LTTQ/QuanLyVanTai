using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Core.Configs;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class BaseManagementForm : UserControl
    {
        public BaseManagementForm()
        {
            InitializeComponent();
            this.Font = ThemeConfig.MainFont;
            this.BackColor = ThemeConfig.PanelBackgroundColor;
            
            // Wire events
            this.btnAdd.Click += new EventHandler(btnAdd_Click);
            this.btnEdit.Click += new EventHandler(btnEdit_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            if (pnlToolbar != null)
            {
                pnlToolbar.BackColor = ThemeConfig.BackgroundColor;
            }

            if (dgvData != null)
            {
                dgvData.BackgroundColor = ThemeConfig.PanelBackgroundColor;
                dgvData.BorderStyle = BorderStyle.None;
            }

            ThemeConfig.StyleSuccessButton(btnAdd, FontAwesome.Sharp.IconChar.Plus);
            ThemeConfig.StylePrimaryButton(btnEdit, FontAwesome.Sharp.IconChar.Pen);
            ThemeConfig.StyleDangerButton(btnDelete, FontAwesome.Sharp.IconChar.Trash);
            ThemeConfig.StylePrimaryButton(btnSave, FontAwesome.Sharp.IconChar.FloppyDisk);
            ThemeConfig.StyleSecondaryButton(btnCancel, FontAwesome.Sharp.IconChar.Xmark);
            
            // Layout buttons in toolbar
            int padding = 10;
            btnAdd.Location = new Point(padding, 10);
            btnEdit.Location = new Point(btnAdd.Right + padding, 10);
            btnDelete.Location = new Point(btnEdit.Right + padding, 10);
            btnSave.Location = new Point(btnDelete.Right + padding, 10);
            btnCancel.Location = new Point(btnSave.Right + padding, 10);
            
            btnSave.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void btnAdd_Click(object? sender, EventArgs e) => OnAdd();
        private void btnEdit_Click(object? sender, EventArgs e) => OnEdit();
        private void btnDelete_Click(object? sender, EventArgs e) => OnDelete();
        private void btnSave_Click(object? sender, EventArgs e) => OnSave();
        private void btnCancel_Click(object? sender, EventArgs e) => OnCancel();

        // Cấu trúc các hàm Virtual để Form con ghi đè
        protected virtual void OnAdd()
        {
            // Logic chung (nếu có), ví dụ: clear các textbox, bật nút Lưu, tắt nút Thêm
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
        }

        protected virtual void OnEdit()
        {
            btnSave.Enabled = true;
        }

        protected virtual void OnDelete()
        {
            // Có thể hiển thị sẵn MessageBox hỏi "Bạn có chắc muốn xóa không?" ở form cha
            // Form con chỉ việc nhận kết quả và thực thi lệnh SQL.
        }

        protected virtual void OnSave()
        {
            // Để trống để Form con tự quyết định Lưu cái gì
        }

        protected virtual void OnCancel()
        {
            // Logic hủy chung
            btnSave.Enabled = false;
        }
    }
}
