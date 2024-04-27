using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abby.DataAccess.Migrations
{
    public partial class updateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "OrderHeader",
                newName: "SessionId");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntendId",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntendId",
                table: "OrderHeader");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "OrderHeader",
                newName: "TransactionId");
        }
    }
}
