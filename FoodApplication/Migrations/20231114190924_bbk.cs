using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodApplication.Migrations
{
    public partial class bbk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartViewModelId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    CartViewModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.CartViewModelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_CartViewModelId",
                table: "products",
                column: "CartViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_cart_CartViewModelId",
                table: "products",
                column: "CartViewModelId",
                principalTable: "cart",
                principalColumn: "CartViewModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_cart_CartViewModelId",
                table: "products");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropIndex(
                name: "IX_products_CartViewModelId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CartViewModelId",
                table: "products");
        }
    }
}
