using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _context;
        private readonly IInventory _inventory;
        private readonly IOrder _order;


        public CartController(ICart context, IInventory inventory, IOrder order)
        {
            _context = context;
            _inventory = inventory;
            _order = order;
        }            

        // Use this method here once you get a button to complete order on checkout page
        [HttpPost]
        public async Task<IActionResult> Checkout(int id)
        {
            string stringEmail = User.Identity.Name;
            Cart cart = await _inventory.GetCart(stringEmail);
            Order order = await _order.CreateOrderForUser(stringEmail);
            order.OrderItems = new List<OrderItems>();
            {

                foreach (CartItems item in cart.CartItems)
                {
                    OrderItems products = new OrderItems();
                    products.InventoryID = item.InventoryID;
                    products.Quantity = item.Quantity;
                    products.OrderID = order.ID;
                    products.CartID = cart.ID;
                    order.OrderItems.Add(products);
                }

                await _order.UpdateOrder(order);
                await _context.DeleteCartItems(stringEmail);
                return RedirectToAction("Index", "Order", order);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           
            await _context.DeleteItem(id);
            return RedirectToAction("Index", "Cart");
        }

      
        [HttpPost]
        public async Task<IActionResult> Update(int id)
        {
            var email = User.Identity.Name;
            await _context.UpdateCartItems(id, email);
            return RedirectToAction("Index", "Cart");

            
        }

        public async Task<IActionResult> Edit()
        {
            var email = User.Identity.Name;
            Cart cart =  await _inventory.GetCart(email);
            return View("Edit", cart);                     
        }

        public IActionResult GetCardInfo()
        {
            return RedirectToAction("Index", "CreditCard");
        }

        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            Cart cart = await _inventory.GetCart(email);
            return View(cart);
        }
   
    }
}