using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingList.Migrations
{
    public partial class BasketUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketShop");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Baskets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ShopId",
                table: "Baskets",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Shops_ShopId",
                table: "Baskets",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Shops_ShopId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_ShopId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Baskets");

            migrationBuilder.CreateTable(
                name: "BasketShop",
                columns: table => new
                {
                    BasketsId = table.Column<int>(type: "int", nullable: false),
                    ShopsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketShop", x => new { x.BasketsId, x.ShopsId });
                    table.ForeignKey(
                        name: "FK_BasketShop_Baskets_BasketsId",
                        column: x => x.BasketsId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketShop_Shops_ShopsId",
                        column: x => x.ShopsId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketShop_ShopsId",
                table: "BasketShop",
                column: "ShopsId");
        }
    }
}
