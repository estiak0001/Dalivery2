using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class updateEmployeeInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZoneID",
                table: "MobileRND_EmployeeInformation");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeType",
                table: "MobileRND_EmployeeInformation",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeType",
                table: "MobileRND_EmployeeInformation");

            migrationBuilder.AddColumn<Guid>(
                name: "ZoneID",
                table: "MobileRND_EmployeeInformation",
                type: "uniqueidentifier",
                maxLength: 150,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
