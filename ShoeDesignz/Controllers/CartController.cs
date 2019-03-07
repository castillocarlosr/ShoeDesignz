using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class CartController : Controller
    {
        private readonly ICart _context;
        private readonly IInventory _inventory;
        private readonly IOrder _order;
        private readonly IEmailSender _emailSender;


        public CartController(ICart context, IInventory inventory, IOrder order, IEmailSender emailSender)
        {
            _context = context;
            _inventory = inventory;
            _order = order;
            _emailSender = emailSender;
        }            

        // Use this method here once you get a button to complete order on checkout page
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
            
            //Email edge
            StringBuilder sb = new StringBuilder();
            sb.Append("<h2>Your most recent order.</h2>");
            sb.Append(order);
            sb.AppendLine(order.CreditCardNumber);
            sb.Append(order.Now);
            sb.Append(order.OrderItems.ToString());
            sb.Append(order.OrderItems);
            sb.Append(order.ID);
            sb.Append(order);
            sb.AppendLine("<p>Thank you!  We hope you continue to shop with us for your fabulous shoez needs!!</p>");
            sb.AppendLine("<p> </p>");
            sb.AppendLine("<a href='https://shoedesignz.azurewebsites.net'> Link to ShoeDesignz </a>");         
            await _emailSender.SendEmailAsync(stringEmail, "Order Confirmation", sb.ToString());
            //Reciepts go here
            return RedirectToAction("Index", "Order", order);
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