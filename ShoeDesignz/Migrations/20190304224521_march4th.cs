using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoeDesignz.Migrations.ShoeDesignzDb
{
    public partial class march4th : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shoes",
                columns: new[] { "ID", "Description", "DiscountPrice", "Gender", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 6, "These are awesome", 42.00m, 1, "https://via.placeholder.com/450", "Shoe6", 234.56m, 12347801 },
                    { 7, "These are GREAT!  I love them more than pizza!!!!", 25.00m, 2, "https://via.placeholder.com/350", "Shoe7", 222.22m, 9876543 },
                    { 8, "These are ok.", 5.00m, 2, "https://via.placeholder.com/250", "Shoe8", 33.33m, 144458901 },
                    { 9, "Cool Coool Coool", 5.00m, 0, "https://via.placeholder.com/150", "SHoe9", 44.44m, 123472221 },
                    { 10, "Total Rip OFF!", 50.00m, 1, "https://via.placeholder.com/450", "ShoeTen", 555.55m, 123471111 }
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
