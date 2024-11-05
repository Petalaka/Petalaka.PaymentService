using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Payment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addmoredetailofordertospecifythepaymentstatusadditemtypeinorderdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_PaymentGateways_PaymentGatewayId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PaidDate",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderExpiry",
                table: "Orders");
            
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OrderExpiry",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CancelDate",
                table: "Orders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PaymentDate",
                table: "Orders",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentGatewayId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentGatewayId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "ItemType",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentGatewayId",
                table: "Orders",
                column: "PaymentGatewayId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_PaymentGateways_PaymentGatewayId",
                table: "OrderDetails",
                column: "PaymentGatewayId",
                principalTable: "PaymentGateways",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentGateways_PaymentGatewayId",
                table: "Orders",
                column: "PaymentGatewayId",
                principalTable: "PaymentGateways",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_PaymentGateways_PaymentGatewayId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentGateways_PaymentGatewayId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentGatewayId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CancelDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentGatewayId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderExpiry",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentGatewayId",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaidDate",
                table: "OrderDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_PaymentGateways_PaymentGatewayId",
                table: "OrderDetails",
                column: "PaymentGatewayId",
                principalTable: "PaymentGateways",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
