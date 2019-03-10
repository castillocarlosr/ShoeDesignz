using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Data;
using ShoeDesignz.Models;
using System;
using Xunit;

namespace ShoeDeignzTDDTests
{
    public class UnitTest1
    {
        //**********************************Inventory Tests****************************************//
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

        //********************************Order Tests***********************************************//
        /// <summary>
        /// Test for the orders
        /// </summary>
        /// <param name="userInfo"></param>
        [Theory]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public void OrderTestUserID(string userInfo)
        {
            Order order = new Order();
            order.UserID = userInfo;
            Assert.Equal(userInfo, order.UserID);
            Assert.True(order.UserID == userInfo);
        }

        //****************************************Cart Tests*********************************************//
        /// <summary>
        /// Tests for Cart order with user ID
        /// </summary>
        /// <param name="userInfo"></param>
        [Theory]
        [InlineData("UserOne")]
        [InlineData("UserTwo")]
        [InlineData("UserThree")]
        public void CartTestUserID(string userInfo)
        {
            Cart cart = new Cart();
            cart.UserID = userInfo;
            Assert.Equal(userInfo, cart.UserID);
            Assert.True(cart.UserID == userInfo);
        }


        //***********************************Cart in DB Tests**********************************************//
        /// <summary>
        /// Tests for Cart Items in DB
        /// </summary>
        [Fact]
        public async void CartInDb()
        {
            DbContextOptions<ShoeDesignzDbContext> options = new DbContextOptionsBuilder<ShoeDesignzDbContext>()
                .UseInMemoryDatabase("CartFromDB")
                .Options;

            using (ShoeDesignzDbContext context = new ShoeDesignzDbContext(options))
            {
                //CRUD
                //CREATE
                Cart cart = new Cart();
                cart.ID = 1;
                cart.UserID = "UserOne";
                context.Cart.Add(cart);
                await context.SaveChangesAsync();

                //READ
                var cartID = await context.Cart.FirstOrDefaultAsync(c => c.ID == cart.ID);
                var cartName = await context.Cart.FirstOrDefaultAsync(n => n.UserID == cart.UserID);
                Assert.Equal(1, cart.ID);
                Assert.Equal("UserOne", cart.UserID);

                //UPDATE
                //cartName.ID = 2;
                //cartName.UserID = "UserTwo";
                //context.Update(cartName);
                //context.SaveChanges();
                //var updateCart = await context.Cart.FirstOrDefaultAsync(c => c.ID == cartName.ID);
                //updateCart = await context.Cart.FirstOrDefaultAsync(c => c.UserID == cartName.UserID);
                //Assert.Equal(2, updateCart.ID);
                //Assert.Equal("UserTwo", updateCart.UserID);

                //DELETE
                //context.Remove(updateCart);
                //context.SaveChanges();
                //var deleteCart = await context.Cart.FirstOrDefaultAsync(n => n.Inventory.Name == updateCart.Inventory.Name);
                //Assert.True(deleteCart == null);
                //var updateCartCount = await context.Cart.CountAsync();
                //Assert.Equal(0, updateCartCount);
            }
        }

        //***********************************Inventory in DB Tests**********************************************
        /// <summary>
        /// Tests for Cart Items in DB
        /// </summary>
        [Fact]
        public async void InventoryInDb()
        {
            DbContextOptions<ShoeDesignzDbContext> options = new DbContextOptionsBuilder<ShoeDesignzDbContext>()
                .UseInMemoryDatabase("InventoryFromDB")
                .Options;

            using (ShoeDesignzDbContext context = new ShoeDesignzDbContext(options))
            {
                //CRUD
                //CREATE
                Inventory inventory = new Inventory();
                inventory.ID = 4;
                inventory.Name = "TestShoeOne";
                inventory.Sku = 4732432;
                inventory.Price = 32.55m;
                inventory.DiscountPrice = 12.25m;
                inventory.Gender = Inventory.Genders.Neutral;
                inventory.Description = "Awesome Shoes to TEST";
                inventory.Image = "/somepath.jpg";
                context.Add(inventory);;
                await context.SaveChangesAsync();

                //READ
                var inventoryTest = await context.Shoes.FirstOrDefaultAsync(c => c.ID == inventory.ID);
                inventoryTest = await context.Shoes.FirstOrDefaultAsync(n => n.Name == inventory.Name);
                inventoryTest = await context.Shoes.FirstOrDefaultAsync(s => s.Sku == inventory.Sku);
                inventoryTest = await context.Shoes.FirstOrDefaultAsync(p => p.Price == inventory.Price);
                inventoryTest = await context.Shoes.FirstOrDefaultAsync(d => d.DiscountPrice == inventory.DiscountPrice);
                inventoryTest = await context.Shoes.FirstOrDefaultAsync(desc => desc.Description == inventory.Description);
                inventoryTest = await context.Shoes.FirstOrDefaultAsync(i => i.Image == inventory.Image);
                //var inventorySku = 
                Assert.Equal(4, inventoryTest.ID);
                Assert.Equal("TestShoeOne", inventoryTest.Name);
                Assert.Equal(4732432, inventoryTest.Sku);
                Assert.Equal(32.55m, inventoryTest.Price);
                Assert.Equal(12.25m, inventoryTest.DiscountPrice);
                Assert.Equal("Awesome Shoes to TEST", inventoryTest.Description);
                Assert.Equal("/somepath.jpg", inventoryTest.Image);

                //UPDATE
                /*
                inventoryTest.ID = 5;
                inventoryTest.Name = "ShoeUPDATE";
                inventoryTest.Sku = 100101011;
                inventoryTest.Price = 44.44m;
                inventoryTest.DiscountPrice = 12.00m;
                inventoryTest.Description = "Cool cool cool";
                inventoryTest.Image = "/newUpdatePath.png";
                context.Update(inventoryTest);
                context.SaveChanges();
                var updateInventory = await context.Shoes.FirstOrDefaultAsync(c => c.ID == inventoryTest.ID);
                updateInventory = await context.Shoes.FirstOrDefaultAsync(n => n.Name == inventory.Name);
                updateInventory = await context.Shoes.FirstOrDefaultAsync(s => s.Sku == inventory.Sku);
                updateInventory = await context.Shoes.FirstOrDefaultAsync(p => p.Price == inventory.Price);
                updateInventory = await context.Shoes.FirstOrDefaultAsync(d => d.DiscountPrice == inventory.DiscountPrice);
                updateInventory = await context.Shoes.FirstOrDefaultAsync(desc => desc.Description == inventory.Description);
                updateInventory = await context.Shoes.FirstOrDefaultAsync(i => i.Image == inventory.Image);
                Assert.Equal(5, inventoryTest.ID);
                Assert.Equal("ShoeUPDATE", inventoryTest.Name);
                Assert.Equal(100101011, inventoryTest.Sku);
                Assert.Equal(44.44m, inventoryTest.Price);
                Assert.Equal(12.00m, inventoryTest.DiscountPrice);
                Assert.Equal("Cool cool cool", inventoryTest.Description);
                Assert.Equal("/newUpdatePath.png", inventoryTest.Image);
                */
                //DELETE
                //context.Remove(updateCart);
                //context.SaveChanges();
                //var deleteCart = await context.Cart.FirstOrDefaultAsync(n => n.Inventory.Name == updateCart.Inventory.Name);
                //Assert.True(deleteCart == null);
                //var updateCartCount = await context.Cart.CountAsync();
                //Assert.Equal(0, updateCartCount);
            }
        }

        //************************************Order in DB Tests**********************************************
        /// <summary>
        /// Tests for Cart Items in DB
        /// </summary>
        [Fact]
        public async void OrderInDb()
        {
            DbContextOptions<ShoeDesignzDbContext> options = new DbContextOptionsBuilder<ShoeDesignzDbContext>()
                .UseInMemoryDatabase("CartFromDB")
                .Options;

            using (ShoeDesignzDbContext context = new ShoeDesignzDbContext(options))
            {
                //CRUD
                //CREATE
                Order order = new Order();
                order.ID = 7;
                order.UserID = "CATS";
                context.Add(order);
                await context.SaveChangesAsync();

                //READ
                var orderID = await context.Order.FirstOrDefaultAsync(c => c.ID == order.ID);
                var orderUserID = await context.Order.FirstOrDefaultAsync(n => n.UserID == order.UserID);
                Assert.Equal(7, order.ID);
                Assert.Equal("CATS", order.UserID);

                //UPDATE
                /*
                orderID.ID = 22;
                orderUserID.UserID = "MoreCATS";
                context.Update(orderID);
                context.SaveChanges();
                var updateOrderID = await context.Order.FirstOrDefaultAsync(c => c.ID == orderID.ID);
                Assert.Equal(22, updateOrderID.ID);
                */

            }
        }
    }
}
