using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
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

        public async Task <IActionResult> Details()
        {
            
        }
        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            Order order = await _context.Getorder(email);
            return View(order);
        }
    }
}