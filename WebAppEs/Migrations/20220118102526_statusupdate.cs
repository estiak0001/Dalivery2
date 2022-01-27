using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class statusupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredDateTime",
                table: "MobileRND_BookingDetailsEntry",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelivered",
                table: "MobileRND_BookingDetailsEntry",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredDateTime",
                table: "MobileRND_BookingDetailsEntry");

            migrationBuilder.DropColumn(
                name: "IsDelivered",
                table: "MobileRND_BookingDetailsEntry");
        }
    }
}
