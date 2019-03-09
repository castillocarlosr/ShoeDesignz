using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Data;
using ShoeDesignz.Models;

namespace ShoeDesignz.Controllers
{
    [Authorize(Policy = "EduEmail")]
    public class PolicyController : Controller
    {
        private readonly ShoeDesignzDbContext _context;

        public PolicyController(ShoeDesignzDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Inventory> GetAllProducts(int productID)
        {
            return _context.Shoes.ToList();
        }

        public IActionResult EduEmail(int shoeID)
        {
            var product = GetAllProducts(shoeID);
            return View(product);
        }
    }
}