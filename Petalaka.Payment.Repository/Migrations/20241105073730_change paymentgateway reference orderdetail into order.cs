using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Payment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class changepaymentgatewayreferenceorderdetailintoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_PaymentGateways_PaymentGatewayId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_PaymentGatewayId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PaymentGatewayId",
                table: "OrderDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentGatewayId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PaymentGatewayId",
                table: "OrderDetails",
                column: "PaymentGatewayId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_PaymentGateways_PaymentGatewayId",
                table: "OrderDetails",
                column: "PaymentGatewayId",
                principalTable: "PaymentGateways",
                principalColumn: "Id");
        }
    }
}
