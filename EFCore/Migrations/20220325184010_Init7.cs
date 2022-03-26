using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Migrations
{
    public partial class Init7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_ProductId",
                table: "order");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_product_ProductId",
                table: "payment");

            migrationBuilder.DropIndex(
                name: "IX_payment_ProductId",
                table: "payment");

            migrationBuilder.DropIndex(
                name: "IX_order_ProductId",
                table: "order");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "payment");

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductId",
                table: "order",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_ProductId",
                table: "order",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_ProductId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_ProductId",
                table: "order");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_payment_ProductId",
                table: "payment",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductId",
                table: "order",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_ProductId",
                table: "order",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_product_ProductId",
                table: "payment",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
