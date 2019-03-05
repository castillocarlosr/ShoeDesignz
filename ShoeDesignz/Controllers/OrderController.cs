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
        [HttpGet]
        [Route("order/details/{id}")]
        public async Task <IActionResult> Details(int id)
        {
            var email = User.Identity.Name;
            Order order = await _context.GetOrder(id);
            return View("Details",order);
        }

        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            List<Order> list = await _context.GetOrders(email);                  
            return View(list);
        }
        
    }
}