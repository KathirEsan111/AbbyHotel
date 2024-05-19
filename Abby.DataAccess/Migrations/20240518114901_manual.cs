using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abby.DataAccess.Migrations
{
    public partial class manual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

               migrationBuilder.AddColumn<string>(
                name: "PickUpDate",
                table: "OrderHeader",
                type: "DateTime",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "PickUpDate",
               table: "OrderHeader");

        }
    }
}
