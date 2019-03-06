using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoeDesignz.Models;

namespace ShoeDesignz.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly IConfiguration _configuration;
        public CreditCardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerifyOrder()
        {
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult Run(Payment paymentInside)
        {
            Payment payment = new Payment(_configuration);
            string answer = payment.Run((long)paymentInside.CreditList, (int)paymentInside.ExpirationCard);

            if(answer == "OK")
            {
                return View("PaymentComplete");
                //return RedirectToAction("PaymentComplete", "CreditCard");
            }
            else
            {
                return RedirectToAction("PaymentError", "CreditCard");
            }
        }
    }
}