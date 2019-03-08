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

            //Email edge
            StringBuilder sb = new StringBuilder();
            sb.Append("<h2>Your most recent order.</h2>");
            sb.Append($"<p>{order.Now}<p>");
            sb.Append(order.ID);
            sb.Append("<ul>");
            foreach (CartItems item in cart.CartItems)
            {
                OrderItems products = new OrderItems();
                products.InventoryID = item.InventoryID;
                //products.Inventory = item.Inventory;
                products.Quantity = item.Quantity;
                products.OrderID = order.ID;
                products.CartID = cart.ID;
                order.OrderItems.Add(products);
                //sb.AppendLine($"order item shit{order}");
                //sb.AppendLine($"order item date: {order.Now}");
                //sb.AppendLine($"order item Order items: {order.OrderItems}");
                //sb.AppendLine($"order item CC: {order.CreditCardNumber}");
                //sb.AppendLine($"order item Product Inventory: {products.Inventory}");
                //sb.AppendLine($"order item Product Cart Items: {cart.CartItems}");
                //sb.AppendLine($"LINE BREAK");
            }
            sb.Append("</ul>");


            await _order.UpdateOrder(order);

            sb.AppendLine($"Order Reciept:  {order}");
            sb.AppendLine("<p>Thank you!  We hope you continue to shop with us for your fabulous shoez needs!!</p>");
            sb.AppendLine("<p>_________________________________________________________________________________</p>");
            sb.AppendLine("<div><a href='https://shoedesignz.azurewebsites.net'>ShoeDesignz  e-Store</a></div>");         
            await _emailSender.SendEmailAsync(stringEmail, "Order Confirmation", sb.ToString());
            //Reciepts go here
            //return RedirectToAction("Index", "Order", order);
            return RedirectToAction("Index", "CreditCard");
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