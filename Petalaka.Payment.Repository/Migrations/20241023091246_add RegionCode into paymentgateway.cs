using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Payment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addRegionCodeintopaymentgateway : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "PaymentGateways",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "PaymentGateways");
        }
    }
}
