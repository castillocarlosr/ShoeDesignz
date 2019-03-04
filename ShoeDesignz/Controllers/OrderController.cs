using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrder _context;
        private IConfiguration _configuration;

        public OrderController(IOrder context)
        {
            _context = context;
        }
        
        //public async Task <IActionResult> Details()
        //{

        //}
        public async Task <IActionResult> Index()
        {
            var email = User.Identity.Name;
            Order order = await _context.Getorder(email);
            return View(order);
        }



        /*
        [HttpPost]
        public IActionResult CompleteOrder(CreditCard card)
        {
            Payment payment = new Payment(_configuration);
            string cardSent = payment.Run();

            if(cardSent == "OK")
            {
                //Send to a page that says payment send sucessfull.  Click here to retrurn to store.
                return View();
            }
            else
            {
                //Send to page that says, "Oh no, something went wrong with your payment.
                //Try another credit card
                return View();
            }
        }*/
        
    }
}