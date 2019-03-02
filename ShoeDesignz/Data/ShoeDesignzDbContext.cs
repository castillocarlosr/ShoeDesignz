using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Models;
using System.Diagnostics;
using static ShoeDesignz.Models.Inventory;

namespace ShoeDesignz.Data
{
    public class ShoeDesignzDbContext : DbContext
    {
        public ShoeDesignzDbContext(DbContextOptions<ShoeDesignzDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory
                {
                    ID = 1,
                    Name = "Adidas",
                    Sku = 123478901,
                    Price = 234.56m,
                    DiscountPrice = 42.00m,
                    Gender = Genders.Female,
                    Description = "These are awesome",
                    Image = "https://via.placeholder.com/450"
                },
                new Inventory
                {
                    ID = 2,
                    Name = "Nike",
                    Sku = 9876543,
                    Price = 222.22m,
                    DiscountPrice = 25.00m,
                    Gender = Genders.Neutral,
                    Description = "These are GREAT!  I love them more than pizza!!!!",
                    Image = "https://via.placeholder.com/350"
                },
                new Inventory
                {
                    ID = 3,
                    Name = "Skech3rs",
                    Sku = 144458901,
                    Price = 33.33m,
                    DiscountPrice = 5.00m,
                    Gender = Genders.Neutral,
                    Description = "These are ok.",
                    Image = "https://via.placeholder.com/250"
                },
                new Inventory
                {
                    ID = 4,
                    Name = "Jordans",
                    Sku = 123472221,
                    Price = 44.44m,
                    DiscountPrice = 5.00m,
                    Gender = Genders.Male,
                    Description = "Cool Coool Coool",
                    Image = "https://via.placeholder.com/150"
                },
                new Inventory
                {
                    ID = 5,
                    Name = "Fake",
                    Sku = 123471111,
                    Price = 555.55m,
                    DiscountPrice = 50.00m,
                    Gender = Genders.Female,
                    Description = "Total Rip OFF!",
                    Image = "https://via.placeholder.com/450"
                },
                new Inventory
                {
                    ID = 6,
                    Name = "Shoe6",
                    Sku = 12347801,
                    Price = 234.56m,
                    DiscountPrice = 42.00m,
                    Gender = Genders.Female,
                    Description = "These are awesome",
                    Image = "https://via.placeholder.com/450"
                },
                new Inventory
                {
                    ID = 7,
                    Name = "Shoe7",
                    Sku = 9876543,
                    Price = 222.22m,
                    DiscountPrice = 25.00m,
                    Gender = Genders.Neutral,
                    Description = "These are GREAT!  I love them more than pizza!!!!",
                    Image = "https://via.placeholder.com/350"
                },
                new Inventory
                {
                    ID = 8,
                    Name = "Shoe8",
                    Sku = 144458901,
                    Price = 33.33m,
                    DiscountPrice = 5.00m,
                    Gender = Genders.Neutral,
                    Description = "These are ok.",
                    Image = "https://via.placeholder.com/250"
                },
                new Inventory
                {
                    ID = 9,
                    Name = "SHoe9",
                    Sku = 123472221,
                    Price = 44.44m,
                    DiscountPrice = 5.00m,
                    Gender = Genders.Male,
                    Description = "Cool Coool Coool",
                    Image = "https://via.placeholder.com/150"
                },
                new Inventory
                {
                    ID = 10,
                    Name = "ShoeTen",
                    Sku = 123471111,
                    Price = 555.55m,
                    DiscountPrice = 50.00m,
                    Gender = Genders.Female,
                    Description = "Total Rip OFF!",
                    Image = "https://via.placeholder.com/450"
                }
                );
        }


        public DbSet<Inventory> Shoes { get; set; }
    }
}
