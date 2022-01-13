using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addcouriertype5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "MobileRND_BookingEntry");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "MobileRND_BookingEntry");

            migrationBuilder.AddColumn<Guid>(
                name: "BrandID",
                table: "MobileRND_BookingEntry",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductID",
                table: "MobileRND_BookingEntry",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandID",
                table: "MobileRND_BookingEntry");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "MobileRND_BookingEntry");

            migrationBuilder.AddColumn<Guid>(
                name: "Brand",
                table: "MobileRND_BookingEntry",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Product",
                table: "MobileRND_BookingEntry",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
