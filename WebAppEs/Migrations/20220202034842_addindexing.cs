using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class addindexing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileRND_IndexingEntry",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    Grade = table.Column<string>(maxLength: 50, nullable: false),
                    Block = table.Column<string>(maxLength: 50, nullable: false),
                    RackNo = table.Column<string>(maxLength: 50, nullable: false),
                    StageNo = table.Column<string>(maxLength: 50, nullable: false),
                    ColumNo = table.Column<string>(maxLength: 50, nullable: false),
                    IndexingDate = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    LUser = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileRND_IndexingEntry", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileRND_IndexingEntry");
        }
    }
}
