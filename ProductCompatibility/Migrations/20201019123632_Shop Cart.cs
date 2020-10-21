using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductCompatibility.Migrations
{
    public partial class ShopCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShopCartItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productID = table.Column<int>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    ShopCartID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCartItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShopCartItem_Product_productID",
                        column: x => x.productID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopCartItem_productID",
                table: "ShopCartItem",
                column: "productID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopCartItem");
        }
    }
}
