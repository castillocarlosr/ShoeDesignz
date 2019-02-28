using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoeDesignz.Migrations
{
    public partial class thhree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Shoes",
                columns: new[] { "ID", "Description", "DiscountPrice", "Gender", "Image", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { 1, "These are awesome", 42.00m, 1, "https://via.placeholder.com/450", "Adidas", 234.56m, 123478901 },
                    { 2, "These are GREAT!  I love them more than pizza!!!!", 25.00m, 2, "https://via.placeholder.com/350", "Nike", 222.22m, 9876543 },
                    { 3, "These are ok.", 5.00m, 2, "https://via.placeholder.com/250", "Skech3rs", 33.33m, 144458901 },
                    { 4, "Cool Coool Coool", 5.00m, 0, "https://via.placeholder.com/150", "Jordans", 44.44m, 123472221 },
                    { 5, "Total Rip OFF!", 50.00m, 1, "https://via.placeholder.com/450", "Fake", 555.55m, 123471111 },
                    { 6, "These are awesome", 42.00m, 1, "https://via.placeholder.com/450", "Shoe6", 234.56m, 123478901 },
                    { 7, "These are GREAT!  I love them more than pizza!!!!", 25.00m, 2, "https://via.placeholder.com/350", "Shoe7", 222.22m, 9876543 },
                    { 8, "These are ok.", 5.00m, 2, "https://via.placeholder.com/250", "Shoe8", 33.33m, 144458901 },
                    { 9, "Cool Coool Coool", 5.00m, 0, "https://via.placeholder.com/150", "SHoe9", 44.44m, 123472221 },
                    { 10, "Total Rip OFF!", 50.00m, 1, "https://via.placeholder.com/450", "ShoeTen", 555.55m, 123471111 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shoes");
        }
    }
}
