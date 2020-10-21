using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductCompatibility.Migrations
{
    public partial class Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItem_Product_productID",
                table: "ShopCartItem");

            migrationBuilder.DropColumn(
                name: "price",
                table: "ShopCartItem");

            migrationBuilder.RenameColumn(
                name: "productID",
                table: "ShopCartItem",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ShopCartItem_productID",
                table: "ShopCartItem",
                newName: "IX_ShopCartItem_ProductID");

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SurName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    OrderTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    Price = table.Column<long>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderID",
                table: "OrderDetail",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductID",
                table: "OrderDetail",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItem_Product_ProductID",
                table: "ShopCartItem",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCartItem_Product_ProductID",
                table: "ShopCartItem");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ShopCartItem",
                newName: "productID");

            migrationBuilder.RenameIndex(
                name: "IX_ShopCartItem_ProductID",
                table: "ShopCartItem",
                newName: "IX_ShopCartItem_productID");

            migrationBuilder.AddColumn<int>(
                name: "price",
                table: "ShopCartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCartItem_Product_productID",
                table: "ShopCartItem",
                column: "productID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
