using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Data;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;

namespace ShoeDesignz.Controllers
{
    public class ProductController : Controller
    {
        private readonly ShoeDesignzDbContext _context;

        public ProductController(ShoeDesignzDbContext context)
        {
            _context = context;
        }


        public List<Inventory> GetAllProducts(int productID)
        {
            return _context.Shoes.ToList();
        }


        // Get Shoes 

        public IActionResult Products(int shoeID)
        {

            var product = GetAllProducts(shoeID);
            return View("Products", product);
        }

    }
}