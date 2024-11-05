using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Payment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddpremiumStartDateinorderdetailaddtionaldetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PremiumPlanStartDate",
                table: "OrderDetailAdditionalDetails",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PremiumPlanStartDate",
                table: "OrderDetailAdditionalDetails");
        }
    }
}
