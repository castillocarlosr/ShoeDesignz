using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _context;

        public OrderController(IOrder context)
        {
            _context = context;
        }

        public async Task <IActionResult> Details(int id)
        {          
            return View("Details", "Order");
        }
        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            List<Order> list = await _context.GetOrders(email);                  
            return View(list);
        }
        
    }
}