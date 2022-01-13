using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileRND_EmployeeInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeID = table.Column<string>(maxLength: 150, nullable: false),
                    EmployeeName = table.Column<string>(maxLength: 250, nullable: false),
                    ContactNumber = table.Column<string>(maxLength: 150, nullable: false),
                    SalesChannelID = table.Column<Guid>(maxLength: 150, nullable: false),
                    ZoneID = table.Column<Guid>(maxLength: 150, nullable: false),
                    Brand = table.Column<string>(maxLength: 150, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_EmployeeInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileRND_AssignCustomerToEmployee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeInformationID = table.Column<Guid>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_AssignCustomerToEmployee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileRND_AssignCustomerToEmployee_MobileRND_EmployeeInformation_EmployeeInformationID",
                        column: x => x.EmployeeInformationID,
                        principalTable: "MobileRND_EmployeeInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileRND_AssignCustomerToEmployee_EmployeeInformationID",
                table: "MobileRND_AssignCustomerToEmployee",
                column: "EmployeeInformationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_AssignCustomerToEmployee");

            migrationBuilder.DropTable(
                name: "MobileRND_EmployeeInformation");
        }
    }
}
