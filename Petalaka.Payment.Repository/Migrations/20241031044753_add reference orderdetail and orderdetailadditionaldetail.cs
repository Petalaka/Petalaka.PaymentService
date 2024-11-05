using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Petalaka.Payment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addreferenceorderdetailandorderdetailadditionaldetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailId",
                table: "OrderDetailAdditionalDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailAdditionalDetails_OrderDetailId",
                table: "OrderDetailAdditionalDetails",
                column: "OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetailAdditionalDetails_OrderDetails_OrderDetailId",
                table: "OrderDetailAdditionalDetails",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetailAdditionalDetails_OrderDetails_OrderDetailId",
                table: "OrderDetailAdditionalDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetailAdditionalDetails_OrderDetailId",
                table: "OrderDetailAdditionalDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "OrderDetailAdditionalDetails");
        }
    }
}
