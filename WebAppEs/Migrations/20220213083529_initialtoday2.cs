using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class initialtoday2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "MobileRND_BookingEntry");

            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "MobileRND_BookingDetailsEntry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "MobileRND_BookingEntry",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "MobileRND_BookingDetailsEntry",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
