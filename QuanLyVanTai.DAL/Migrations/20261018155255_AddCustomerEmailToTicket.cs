using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyVanTai.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerEmailToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Tickets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Tickets");
        }
    }
}
