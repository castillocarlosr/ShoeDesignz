using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoeDesignz.Models;

namespace ShoeDesignz.Controllers
{
    public class CompleteOrderController : Controller
    {/*
        private IConfiguration _configuration;

        [HttpPost]
        public IActionResult CompleteOrder(string card)
        {
            Payment payment = new Payment(_configuration);
            string cardSent = payment.Run();

            if(cardSent == "OK")
            {
                //Send to a page that says payment send sucessfull.  Click here to retrurn to store.
                var order = await _context.GetOrders();
                return View("Details", order);
            }
            else
            {
                //Send to page that says, "Oh no, something went wrong with your payment.
                //Try another credit card
                return View();
            }
        }

        public IActionResult Index()
        {
            return View();
        }*/
    }
}