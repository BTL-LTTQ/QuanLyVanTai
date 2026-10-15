using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuanLyVanTai.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleMenuPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "LichTrinh", "Điều độ Lịch trình" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "TramDung", "Quản lý Trạm dừng" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhuongTien", "Quản lý Phương tiện" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "NhanSu", "Quản lý Nhân sự" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "DichVu", "Quản lý Dịch vụ" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "GiaVe", "Quản lý Giá vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "KhuyenMai", "Quản lý Khuyến mãi" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhanQuyen", "Quản lý Phân quyền" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CanDelete", "MenuCode", "MenuName", "RoleName" },
                values: new object[] { true, "NhatKyHoatDong", "Nhật ký hoạt động", "Quản trị viên" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "TuyenXe", "Quản lý Tuyến xe" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "LichTrinh", "Điều độ Lịch trình" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "TramDung", "Quản lý Trạm dừng" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhuongTien", "Quản lý Phương tiện" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "NhanSu", "Quản lý Nhân sự" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "DichVu", "Quản lý Dịch vụ" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CanAdd", "CanEdit", "MenuCode", "MenuName" },
                values: new object[] { true, true, "GiaVe", "Quản lý Giá vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CanAdd", "CanEdit", "MenuCode", "MenuName" },
                values: new object[] { true, true, "KhuyenMai", "Quản lý Khuyến mãi" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CanExport", "MenuCode", "MenuName", "RoleName" },
                values: new object[] { true, "PhanQuyen", "Quản lý Phân quyền", "Quản lý" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CanExport", "CanView", "MenuCode", "MenuName", "RoleName" },
                values: new object[] { true, true, "NhatKyHoatDong", "Nhật ký hoạt động", "Quản lý" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CanView", "MenuCode", "MenuName" },
                values: new object[] { true, "TuyenXe", "Quản lý Tuyến xe" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "LichTrinh", "Điều độ Lịch trình" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "TramDung", "Quản lý Trạm dừng" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CanAdd", "CanView", "MenuCode", "MenuName" },
                values: new object[] { false, false, "PhuongTien", "Quản lý Phương tiện" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CanView", "MenuCode", "MenuName" },
                values: new object[] { false, "NhanSu", "Quản lý Nhân sự" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "DichVu", "Quản lý Dịch vụ" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CanAdd", "CanView", "MenuCode", "MenuName" },
                values: new object[] { true, true, "GiaVe", "Quản lý Giá vé" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "CanAdd", "CanDelete", "CanEdit", "CanExport", "CanView", "MenuCode", "MenuName", "RoleName", "UpdatedAt" },
                values: new object[,]
                {
                    { 28, false, false, false, false, true, "KhuyenMai", "Quản lý Khuyến mãi", "Nhân viên bán vé", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, false, false, false, false, false, "PhanQuyen", "Quản lý Phân quyền", "Nhân viên bán vé", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, false, false, false, false, false, "NhatKyHoatDong", "Nhật ký hoạt động", "Nhân viên bán vé", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "TramDung", "Quản lý Trạm dừng" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhuongTien", "Quản lý Phương tiện" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "NhanSu", "Quản lý Nhân sự" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "DichVu", "Quản lý Dịch vụ" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "GiaVe", "Quản lý Giá vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "KhuyenMai", "Quản lý Khuyến mãi" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhanQuyen", "Quản lý Phân quyền" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "NhatKyHoatDong", "Nhật ký hoạt động" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CanDelete", "MenuCode", "MenuName", "RoleName" },
                values: new object[] { false, "TuyenXe", "Quản lý Tuyến xe", "Quản lý" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "TramDung", "Quản lý Trạm dừng" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhuongTien", "Quản lý Phương tiện" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "NhanSu", "Quản lý Nhân sự" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "DichVu", "Quản lý Dịch vụ" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "GiaVe", "Quản lý Giá vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "KhuyenMai", "Quản lý Khuyến mãi" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CanAdd", "CanEdit", "MenuCode", "MenuName" },
                values: new object[] { false, false, "PhanQuyen", "Quản lý Phân quyền" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CanAdd", "CanEdit", "MenuCode", "MenuName" },
                values: new object[] { false, false, "NhatKyHoatDong", "Nhật ký hoạt động" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CanExport", "MenuCode", "MenuName", "RoleName" },
                values: new object[] { false, "TuyenXe", "Quản lý Tuyến xe", "Nhân viên bán vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CanExport", "CanView", "MenuCode", "MenuName", "RoleName" },
                values: new object[] { false, false, "TramDung", "Quản lý Trạm dừng", "Nhân viên bán vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CanView", "MenuCode", "MenuName" },
                values: new object[] { false, "PhuongTien", "Quản lý Phương tiện" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "NhanSu", "Quản lý Nhân sự" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "DichVu", "Quản lý Dịch vụ" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CanAdd", "CanView", "MenuCode", "MenuName" },
                values: new object[] { true, true, "GiaVe", "Quản lý Giá vé" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CanView", "MenuCode", "MenuName" },
                values: new object[] { true, "KhuyenMai", "Quản lý Khuyến mãi" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "MenuCode", "MenuName" },
                values: new object[] { "PhanQuyen", "Quản lý Phân quyền" });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CanAdd", "CanView", "MenuCode", "MenuName" },
                values: new object[] { false, false, "NhatKyHoatDong", "Nhật ký hoạt động" });
        }
    }
}
