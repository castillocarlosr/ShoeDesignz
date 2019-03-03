using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoeDesignz.Migrations
{
    public partial class newDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "CartItems",
                newName: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_InventoryID",
                table: "CartItems",
                column: "InventoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Shoes_InventoryID",
                table: "CartItems",
                column: "InventoryID",
                principalTable: "Shoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Shoes_InventoryID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_InventoryID",
                table: "CartItems");

            migrationBuilder.RenameColumn(
                name: "InventoryID",
                table: "CartItems",
                newName: "ProductID");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "CartItems",
                nullable: false,
                defaultValue: 0);
        }
    }
}
