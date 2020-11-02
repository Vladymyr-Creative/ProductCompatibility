using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductCompatibility.Migrations
{
    public partial class ProductCompatibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Compatibility_CompatibilityID",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_CompatibilityID",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CompatibilityID",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductsCompatibility",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompatibilityID = table.Column<int>(nullable: false),
                    Product1ID = table.Column<int>(nullable: false),
                    Product2ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCompatibility", x => x.ID);
                    table.UniqueConstraint("AK_ProductsCompatibility_Product1ID_Product2ID", x => new { x.Product1ID, x.Product2ID });
                    table.ForeignKey(
                        name: "FK_ProductsCompatibility_Compatibility_CompatibilityID",
                        column: x => x.CompatibilityID,
                        principalTable: "Compatibility",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductsCompatibility_Product_Product1ID",
                        column: x => x.Product1ID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ProductsCompatibility_Product_Product2ID",
                        column: x => x.Product2ID,
                        principalTable: "Product",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCompatibility_CompatibilityID",
                table: "ProductsCompatibility",
                column: "CompatibilityID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCompatibility_Product2ID",
                table: "ProductsCompatibility",
                column: "Product2ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsCompatibility");

            migrationBuilder.AddColumn<int>(
                name: "CompatibilityID",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_CompatibilityID",
                table: "Product",
                column: "CompatibilityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Compatibility_CompatibilityID",
                table: "Product",
                column: "CompatibilityID",
                principalTable: "Compatibility",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
