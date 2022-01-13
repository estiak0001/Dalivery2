using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class updateassignedInformationtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_AssignedEmployeeOrCustomer");

            migrationBuilder.CreateTable(
                name: "MobileRND_Assigned_ASM_TO_DNSM",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeDNSMID = table.Column<Guid>(nullable: false),
                    AssignedEmployeeASMID = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_Assigned_ASM_TO_DNSM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileRND_Assigned_ASM_TO_DNSM_MobileRND_EmployeeInformation_EmployeeDNSMID",
                        column: x => x.EmployeeDNSMID,
                        principalTable: "MobileRND_EmployeeInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobileRND_Assigned_Customer_TO_TSO",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeTSOID = table.Column<Guid>(nullable: false),
                    AssignedCustomerID = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_Assigned_Customer_TO_TSO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileRND_Assigned_Customer_TO_TSO_MobileRND_EmployeeInformation_EmployeeTSOID",
                        column: x => x.EmployeeTSOID,
                        principalTable: "MobileRND_EmployeeInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobileRND_Assigned_TSO_TO_ASM",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EmployeeASMID = table.Column<Guid>(nullable: false),
                    AssignedEmployeeTSOID = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_Assigned_TSO_TO_ASM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileRND_Assigned_TSO_TO_ASM_MobileRND_EmployeeInformation_EmployeeASMID",
                        column: x => x.EmployeeASMID,
                        principalTable: "MobileRND_EmployeeInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileRND_Assigned_ASM_TO_DNSM_EmployeeDNSMID",
                table: "MobileRND_Assigned_ASM_TO_DNSM",
                column: "EmployeeDNSMID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileRND_Assigned_Customer_TO_TSO_EmployeeTSOID",
                table: "MobileRND_Assigned_Customer_TO_TSO",
                column: "EmployeeTSOID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileRND_Assigned_TSO_TO_ASM_EmployeeASMID",
                table: "MobileRND_Assigned_TSO_TO_ASM",
                column: "EmployeeASMID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_Assigned_ASM_TO_DNSM");

            migrationBuilder.DropTable(
                name: "MobileRND_Assigned_Customer_TO_TSO");

            migrationBuilder.DropTable(
                name: "MobileRND_Assigned_TSO_TO_ASM");

            migrationBuilder.CreateTable(
                name: "MobileRND_AssignedEmployeeOrCustomer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedCustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssignedEmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeInformationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesChannelID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ZoneID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
    }
}
