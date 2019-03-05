using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public async Task<IActionResult> Checkout(int id)
        {
            string stringEmail = User.Identity.Name;
            Cart cart = await _inventory.GetCart(stringEmail);
            Order order = await _order.CreateOrderForUser(stringEmail);
            order.OrderItems = new List<OrderItems>();
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
            return RedirectToAction("Index", "Order", order);
        }

            //string stringEmail = User.Identity.Name;
            //Cart cart = await _inventory.GetCart(stringEmail);
            //List<OrderItems> list = new List<OrderItems>();
            //Order order = new Order();
            //order.DateCreated = DateTime.UtcNow.Date;
            //order.ID = id;
            //order.OrderItems = list;
            //order.UserID = stringEmail;
            
            //// feplace order items with cart id

            //foreach (var item in cart.CartItems)
            //{
            //    OrderItems shoes = await _order.ConvertCartItem(item);

            //    list.Add(shoes);
            //}

            //string stringEmail = User.Identity.Name;
            //Cart cart = await _context.GetCart(stringEmail);

            //CartItems product = new CartItems();
            //product.InventoryID = id;
            //product.Quantity = 1;
            //product.CartID = cart.ID;
            //await _context.AddCartItem(product);

            //await _order.AddOrder(order);


            // empty the current cart
            // show page to thank for order
            // link to view orders that contain order details

        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            Cart cart = await _inventory.GetCart(email);
            return View(cart);
        }
       
    }
}