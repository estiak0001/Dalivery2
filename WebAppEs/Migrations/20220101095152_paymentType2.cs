using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppEs.Migrations
{
    public partial class paymentType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentTypeName",
                table: "MobileRND_PaymentType");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "MobileRND_PaymentType",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "MobileRND_PaymentType");

            migrationBuilder.AddColumn<string>(
                name: "PaymentTypeName",
                table: "MobileRND_PaymentType",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
