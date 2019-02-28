using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser rvm)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {

                    UserName = rvm.Email,
                    Email = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Birthday = rvm.Birthday
                };


                var result = await _userManager.CreateAsync(user, rvm.Password);
                if (result.Succeeded)
                {
                    Claim fullNameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");

                    Claim birthdayClaim = new Claim(ClaimTypes.DateOfBirth, new DateTime(user.Birthday.Year, user.Birthday.Month, user.Birthday.Day).ToString("u"),
                    ClaimValueTypes.DateTime);

                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimTypes.Email);
                    List<Claim> claims = new List<Claim> { fullNameClaim, birthdayClaim, emailClaim };

                    await _userManager.AddClaimsAsync(user, claims);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //Email edge
                    await _emailSender.SendEmailAsync(rvm.Email, "Thank you for Loggin In!", "<p>Thanks for being here</p>");
                    var ourUser = await _userManager.FindByEmailAsync(rvm.Email);
                    string id = ourUser.Id;

                    return RedirectToAction("Products", "Product");
                }
            }

            return View(rvm);
        }
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    //Send the user an email
                    //Get the user email
                    //await _emailSender.SendEmailAsync(lvm.Email, "Thank you for Loggin In!", "<p>Thanks for being here</p>");

                    //How do i get a users ID.  follow below.........
                    //var ourUser = await _userManager.FindByEmailAsync(lvm.Email);
                    //string id = ourUser.Id;

                    return RedirectToAction("Products", "Product");
                }
            }

            ModelState.TryAddModelError(string.Empty, "Invalid Login Attempt");

            return View(lvm);          
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        //********************twitter setup below**************//
        [HttpPost]
        public IActionResult ExternalLogin(string provider)
        {
            var redirectURL = Url.Action(nameof(ExternalLoginCallBack), "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectURL);
            return Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallBack(string error = null)
        {
            if(error != null)
            {
                TempData["Error"] = "Error with the Provider";
                return RedirectToAction("Login");
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();

            if(info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (result.Succeeded)
            {
                return RedirectToAction("Products", "Product");
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            return View("ExternalLogin", new ExternalLoginViewModel { Email = email });
        }

        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel elvm)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if(info == null)
                {
                    TempData["Error"] = "Error loggin in!";
                }

                var user = new ApplicationUser
                {
                    UserName = elvm.Email,
                    Email = elvm.Email,
                    FirstName = elvm.FirstName,
                    LastName = elvm.LastName,
                    //Birthday = elvm.Birthday  If we can great.  If not, no big deal.
                };

                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    Claim fullNameFromClaim = new Claim("FullName", $"{user.FirstName}{user.LastName}");
                    await _userManager.AddClaimAsync(user, fullNameFromClaim);

                    result = await _userManager.AddLoginAsync(user, info);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Products", "Product");
                    }
                }
            }
            return View(elvm);
        }

    }
}

