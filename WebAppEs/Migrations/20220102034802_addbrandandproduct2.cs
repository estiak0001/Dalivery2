using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addbrandandproduct2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileRND_BookingEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingDate = table.Column<DateTime>(maxLength: 150, nullable: false),
                    PaymentTypeId = table.Column<Guid>(maxLength: 150, nullable: false),
                    CourierId = table.Column<Guid>(maxLength: 150, nullable: false),
                    Brand = table.Column<string>(maxLength: 150, nullable: false),
                    Product = table.Column<string>(maxLength: 150, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_BookingEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileRND_Brand",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BrandName = table.Column<string>(maxLength: 150, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileRND_Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 150, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MobileRND_BookingDetailsEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingId = table.Column<Guid>(nullable: false),
                    CNNumber = table.Column<int>(maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(maxLength: 150, nullable: false),
                    Ammount = table.Column<decimal>(maxLength: 150, nullable: false),
                    Rate = table.Column<decimal>(maxLength: 150, nullable: false),
                    CustomerID = table.Column<Guid>(maxLength: 150, nullable: false),
                    Remarks = table.Column<Guid>(maxLength: 150, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_BookingDetailsEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileRND_BookingDetailsEntry_MobileRND_BookingEntry_BookingId",
                        column: x => x.BookingId,
                        principalTable: "MobileRND_BookingEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileRND_BookingDetailsEntry_BookingId",
                table: "MobileRND_BookingDetailsEntry",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_BookingDetailsEntry");

            migrationBuilder.DropTable(
                name: "MobileRND_Brand");

            migrationBuilder.DropTable(
                name: "MobileRND_Product");

            migrationBuilder.DropTable(
                name: "MobileRND_BookingEntry");
        }
    }
}
