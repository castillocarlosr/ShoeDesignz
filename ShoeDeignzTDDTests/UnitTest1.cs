using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Data;
using ShoeDesignz.Models;
using System;
using Xunit;

namespace ShoeDeignzTDDTests
{
    public class UnitTest1
    {
        //**********************************************************************************//
        /// <summary>
        /// These tests are for the Get methods on the ShoeDesignz Inventory DB.
        /// </summary>
        /// <param name="name"></param>
        [Theory]
        [InlineData("ShoeOne")]
        [InlineData("RunningHighHeels")]
        [InlineData("GlitterShoez")]
        public void GetName(string name)
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = name,
                Sku = 1234455,
                Price = 12.39m,
                DiscountPrice = 3.33m,
                Gender = Inventory.Genders.Female,
                Description = "This is for the test",
                Image = "/somePath"
            };
            Assert.Equal(name, inventory.Name);
            Assert.True(inventory.Name == name);
        }

        [Theory]
        [InlineData(12347890)]
        [InlineData(98765432)]
        [InlineData(10010110)]
        public void GetSku(int skuNumber)
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = "ShoeTest",
                Sku = skuNumber,
                Price = 12.39m,
                DiscountPrice = 3.33m,
                Gender = Inventory.Genders.Neutral,
                Description = "This is for the test 2",
                Image = "/somePath"
            };
            Assert.Equal(skuNumber, inventory.Sku);
            Assert.True(inventory.Sku == skuNumber);
        }

        [Theory]
        [InlineData(124.34)]
        [InlineData(8.87)]
        [InlineData(1.23)]
        public void GetPrice(decimal money)
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = "ShoeTest",
                Sku = 12347890,
                Price = money,
                DiscountPrice = 3.33m,
                Gender = Inventory.Genders.Male,
                Description = "This is for the test 2",
                Image = "/somePath"
            };
            Assert.Equal(money, inventory.Price);
            Assert.True(inventory.Price == money);
        }

        [Theory]
        [InlineData(124.34)]
        [InlineData(8.87)]
        [InlineData(1.23)]
        public void GetDiscountPrice(decimal moneyDiscount)
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = "ShoeTest",
                Sku = 12347890,
                Price = 145.98m,
                DiscountPrice = moneyDiscount,
                Gender = Inventory.Genders.Neutral,
                Description = "This is for the test 2",
                Image = "/somePath"
            };
            Assert.Equal(moneyDiscount, inventory.DiscountPrice);
            Assert.True(inventory.DiscountPrice == moneyDiscount);
        }
        /*  Could not get a proper enum test.  -Carlos
        [Theory]
        [InlineData(Inventory.Genders.Female)]
        [InlineData(Inventory.Genders.Male)]
        [InlineData(Inventory.Genders.Neutral)]
        public void Gender(Inventory.Genders test5)
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = "ShoeTest",
                Sku = 12347890,
                Price = 145.98m,
                DiscountPrice = 2.50m,
                Gender = Inventory.Genders.,
                Description = "This is for the test 2",
                Image = "/somePath"
            };
            Assert.Equal(true, (test5.ToString()));
        }
        */
        [Theory]
        [InlineData("Possibly the best shoe I have ever owned.  WOW!")]
        [InlineData("These shoes are going places!")]
        [InlineData("My cat vomitted on my new shoes.  Why?!?")]
        public void GetDescription(string describe)
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = "ShoeTest",
                Sku = 12347890,
                Price = 145.98m,
                DiscountPrice = 2.50m,
                Gender = Inventory.Genders.Neutral,
                Description = describe,
                Image = "/somePath"
            };
            Assert.Equal(describe, inventory.Description);
            Assert.True(inventory.Description == describe);
        }

        [Fact]
        public async void CartInDb()
        {
            DbContextOptions<ShoeDesignzDbContext> options = new DbContextOptionsBuilder<ShoeDesignzDbContext>()
                .UseInMemoryDatabase("GetCart")
                .Options;

            using (ShoeDesignzDbContext context = new ShoeDesignzDbContext(options))
            {
                //CRUD
                //CREATE 
                CartItems cartItems = new CartItems();
                cartItems.ID = 1;
                cartItems.CartID = 1;
                cartItems.InventoryID = 1;
                cartItems.Inventory.Name = "ShoeOne";
                context.CartItems.Add(cartItems);
                context.SaveChanges();

                //READ
                var cartName = await context.CartItems.FirstOrDefaultAsync(n => n.Inventory.Name == cartItems.Inventory.Name);
                Assert.Equal("ShoeOne", cartItems.Inventory.Name);

                //UPDATE
                cartName.Inventory.Name = "ShoeTwo UPDATE";
                context.Update(cartName);
                context.SaveChanges();
                var updateCart = await context.CartItems.FirstOrDefaultAsync(n => n.Inventory.Name == cartName.Inventory.Name);
                //Assert.Equal("ShoeTwo UPDATE", updateCart.Inventory.Name);

                //DELETE
                context.CartItems.Remove(updateCart);
                context.SaveChanges();
                var deleteCart = await context.CartItems.FirstOrDefaultAsync(n => n.Inventory.Name == updateCart.Inventory.Name);
                //Assert.True(deleteCart == null);
            }
        }
    }
}
