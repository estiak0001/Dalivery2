using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class AddAssignedEmployeeOrCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_AssignCustomerToEmployee");

            migrationBuilder.CreateTable(
                name: "MobileRND_AssignedEmployeeOrCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeInformationID = table.Column<Guid>(nullable: false),
                    AssignedEmployeeID = table.Column<Guid>(nullable: false),
                    AssignedCustomerID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_AssignedEmployeeOrCustomer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileRND_AssignedEmployeeOrCustomer_MobileRND_EmployeeInformation_EmployeeInformationID",
                        column: x => x.EmployeeInformationID,
                        principalTable: "MobileRND_EmployeeInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileRND_AssignedEmployeeOrCustomer_EmployeeInformationID",
                table: "MobileRND_AssignedEmployeeOrCustomer",
                column: "EmployeeInformationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_AssignedEmployeeOrCustomer");

            migrationBuilder.CreateTable(
                name: "MobileRND_AssignCustomerToEmployee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeInformationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
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
    }
}
