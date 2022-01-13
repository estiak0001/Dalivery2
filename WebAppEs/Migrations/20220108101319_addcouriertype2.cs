using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addcouriertype2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerID",
                table: "MobileRND_BookingDetailsEntry");

            migrationBuilder.AddColumn<string>(
                name: "CustomerNo",
                table: "MobileRND_BookingDetailsEntry",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerNo",
                table: "MobileRND_BookingDetailsEntry");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerID",
                table: "MobileRND_BookingDetailsEntry",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
