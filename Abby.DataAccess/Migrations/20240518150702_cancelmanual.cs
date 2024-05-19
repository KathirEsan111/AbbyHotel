using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abby.DataAccess.Migrations
{
    public partial class cancelmanual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
             name: "PickUpDate",
             table: "OrderHeader");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
                migrationBuilder.AddColumn<string>(
                name: "PickUpDate",
                table: "OrderHeader",
                type: "DateTime",
                nullable: false);
        }
    }
}
