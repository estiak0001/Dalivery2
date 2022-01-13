using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addCourierInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileRND_CourierInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CourierName = table.Column<string>(maxLength: 150, nullable: false),
                    CoverRate = table.Column<decimal>(maxLength: 150, nullable: false),
                    NonCoverRate = table.Column<decimal>(maxLength: 150, nullable: false),
                    RateFixedFromDate = table.Column<DateTime>(maxLength: 150, nullable: false),
                    RateFixedToDate = table.Column<DateTime>(maxLength: 150, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_CourierInformation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_CourierInformation");
        }
    }
}
