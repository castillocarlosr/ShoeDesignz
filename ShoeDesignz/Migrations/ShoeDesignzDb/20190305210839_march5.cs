using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoeDesignz.Migrations.ShoeDesignzDb
{
    public partial class march5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<string>(nullable: true),
                    Now = table.Column<DateTime>(nullable: false),
                    CreditCardNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Sku = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    DiscountPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CartID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    InventoryID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CartItems_Cart_CartID",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Shoes_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Shoes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InventoryID = table.Column<int>(nullable: false),
                    CartID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Cart_CartID",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Shoes_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Shoes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Shoes",
                columns: new[] { "ID", "Description", "DiscountPrice", "Gender", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, "These are awesome", 2.00m, 1, "https://via.placeholder.com/450", "Adidas", 11.11m, 123478901 },
                    { 2, "These are GREAT!  I love them more than pizza!!!!", 2.00m, 2, "https://via.placeholder.com/350", "Nike", 12.12m, 9876543 },
                    { 3, "These are ok.", 5.00m, 2, "https://via.placeholder.com/250", "Skech3rs", 33.33m, 144458901 },
                    { 4, "Cool Coool Coool", 5.00m, 0, "https://via.placeholder.com/150", "Jordans", 44.44m, 123472221 },
                    { 5, "Total Rip OFF!", 5.00m, 1, "https://via.placeholder.com/450", "Fake", 15.15m, 123471111 },
                    { 6, "These are awesome", 4.00m, 1, "https://via.placeholder.com/450", "Shoe6", 16.16m, 12347801 },
                    { 7, "These are GREAT!  I love them more than pizza!!!!", 2.00m, 2, "https://via.placeholder.com/350", "Shoe7", 17.17m, 9876543 },
                    { 8, "These are ok.", 5.00m, 2, "https://via.placeholder.com/250", "Shoe8", 18.18m, 144458901 },
                    { 9, "Cool Coool Coool", 5.00m, 0, "https://via.placeholder.com/150", "SHoe9", 19.19m, 123472221 },
                    { 10, "Total Rip OFF!", 7.00m, 1, "https://via.placeholder.com/450", "ShoeTen", 20.20m, 123471111 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_InventoryID",
                table: "CartItems",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CartID",
                table: "OrderItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_InventoryID",
                table: "OrderItems",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
