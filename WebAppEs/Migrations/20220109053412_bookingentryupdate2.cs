using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class bookingentryupdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "MobileRND_BookingDetailsEntry");

            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "MobileRND_BookingEntry",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprove",
                table: "MobileRND_BookingEntry");

            migrationBuilder.AddColumn<bool>(
                name: "IsApprove",
                table: "MobileRND_BookingDetailsEntry",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
