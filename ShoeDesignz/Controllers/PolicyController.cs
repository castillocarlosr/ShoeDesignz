using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoeDesignz.Controllers
{
    [Authorize(Policy = "EduEmail")]
    public class PolicyController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Education()
        {
            return View();
        }

        //public IActionResult Login() => View();
        //public IActionResult Logout() => View();
    }
}