using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileRND_CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerNo = table.Column<string>(maxLength: 150, nullable: false),
                    CustomerName = table.Column<string>(maxLength: 250, nullable: false),
                    SalesChannelID = table.Column<string>(maxLength: 150, nullable: false),
                    ZoneID = table.Column<string>(maxLength: 150, nullable: false),
                    Product = table.Column<string>(maxLength: 150, nullable: false),
                    Brand = table.Column<string>(maxLength: 150, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    DeliveryAddress = table.Column<string>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_CustomerInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_CustomerInfo");
        }
    }
}
