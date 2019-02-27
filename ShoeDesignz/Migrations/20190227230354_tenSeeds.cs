using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoeDesignz.Migrations
{
    public partial class tenSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shoes",
                columns: new[] { "ID", "Description", "DiscountPrice", "Gender", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 6, "These are awesome", 42.00m, 1, "https://via.placeholder.com/450", "ShoeSix", 234.56m, 1066666 },
                    { 7, "These are GREAT!  I love them more than pizza!!!!", 25.00m, 2, "https://via.placeholder.com/350", "ShoeSeven", 222.22m, 10777777 },
                    { 8, "These are ok.", 5.00m, 2, "https://via.placeholder.com/250", "Shoe8", 33.33m, 11088881 },
                    { 9, "Cool Coool Coool", 5.00m, 0, "https://via.placeholder.com/150", "ShoeNine", 44.44m, 11099999 },
                    { 10, "Total Rip OFF!", 50.00m, 1, "https://via.placeholder.com/450", "ShoeTen", 555.55m, 101010101 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Shoes",
                keyColumn: "ID",
                keyValue: 10);
        }
    }
}
