using ShoeDesignz.Models;
using System;
using Xunit;

namespace ShoeDeignzTDDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Inventory inventory = new Inventory()
            {
                ID = 1,
                Name = "TestOne",
                Sku = 1234455,
                Price = 12.39m,
                DiscountPrice = 3.33m,
                Gender = Inventory.Genders.Female,
                Description = "This is for the rtest",
                Image = "/somePath"
            };

            Assert.True(inventory.Name == "TestOne");
        }
    }
}
