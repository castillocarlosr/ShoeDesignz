using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class ProductController : Controller
    {
        private readonly IInventory _context;

        public ProductController(IInventory context)
        {
            _context = context;
        }

        // Get Shoes 
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var product = await _context.GetInventories();
            return View("Products", product);
        }

        public async Task<IActionResult> Details(int id)
        {
            Inventory product = await _context.GetInventoryByID(id);
            return View("Details", product);
        }

        [HttpGet]          
        public async Task <IActionResult> Buy(int id)
        {
            string stringEmail = User.Identity.Name;
            Cart cart = await _context.GetCart(stringEmail);

            CartItems product = new CartItems();
            product.InventoryID = id;
            product.Quantity = 1;
            product.CartID = cart.ID;
            await _context.AddCartItem(product);
   
         return RedirectToAction("Index", "Cart", product);
  
        }

        //Set up payments here with http post

    }
}