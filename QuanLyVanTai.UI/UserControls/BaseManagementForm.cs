using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuanLyVanTai.UI.UserControls
{
    public partial class BaseManagementForm : UserControl
    {
        public BaseManagementForm()
        {
            InitializeComponent();
            this.Font = ThemeConfig.MainFont;
        }

        private void btnAdd_Click(object sender, EventArgs e) => OnAdd();
        private void btnEdit_Click(object sender, EventArgs e) => OnEdit();
        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();
        private void btnSave_Click(object sender, EventArgs e) => OnSave();
        private void btnCancel_Click(object sender, EventArgs e) => OnCancel();

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
