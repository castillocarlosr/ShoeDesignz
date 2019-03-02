using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models.Interfaces;
using ShoeDesignz.Models;

namespace ShoeDesignz.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _context;

        public CartController(ICart context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
       
    }
}