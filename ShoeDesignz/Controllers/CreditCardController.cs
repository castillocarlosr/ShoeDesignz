using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;

namespace ShoeDesignz.Controllers
{
    public class CreditCardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerifyOrder()
        {
            return RedirectToAction("Index", "Cart");
        }
    }
}