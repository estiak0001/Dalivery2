using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class AddAssignedEmployeeOrCustomerTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SalesChannelID",
                table: "MobileRND_AssignedEmployeeOrCustomer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ZoneID",
                table: "MobileRND_AssignedEmployeeOrCustomer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesChannelID",
                table: "MobileRND_AssignedEmployeeOrCustomer");

            migrationBuilder.DropColumn(
                name: "ZoneID",
                table: "MobileRND_AssignedEmployeeOrCustomer");
        }
    }
}
