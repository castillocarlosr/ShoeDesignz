using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _context;
        private readonly IInventory _inventory;

        public CartController(ICart context, IInventory inventory)
        {
            _context = context;
            _inventory = inventory;
        }

        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            Cart cart = await _inventory.GetCart(email);
            return View(cart);
        }
       
    }
}