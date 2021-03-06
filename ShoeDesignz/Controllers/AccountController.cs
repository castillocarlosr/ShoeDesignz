﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ShoeDesignz.Models;
using ShoeDesignz.Models.Interfaces;
using ShoeDesignz.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoeDesignz.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IEmailSender _emailSender;
        private readonly ICart _context;

        public AccountController(ICart context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        /// <summary>
        /// Will get the view page from the Register view page.
        /// </summary>
        /// <returns>A very awesome register page for new users.</returns>
        [HttpGet]
        public IActionResult Register() => View();

        /// <summary>
        /// Will send the new user registered information to a database to keep track of their shopping and be able to log back in.
        /// </summary>
        /// <param name="rvm"></param>
        /// <returns>An awesome thanks for registering page.</returns>
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
                    await _context.GetCartForUser(user.Email);
                    
                

                    Claim fullNameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");

                    Claim birthdayClaim = new Claim(ClaimTypes.DateOfBirth, new DateTime(user.Birthday.Year, user.Birthday.Month, user.Birthday.Day).ToString("u"),
                    ClaimValueTypes.DateTime);

                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimTypes.Email);
                    List<Claim> claims = new List<Claim> { fullNameClaim, birthdayClaim, emailClaim };

                    await _userManager.AddClaimsAsync(user, claims);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    //Email edge
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<h2>Congratulations on Registering</h2>");
                    sb.AppendLine("<p>Please accept this introductory coupon for 0% off on your first purchase.</p>");
                    sb.AppendLine("<p>We hope you continue to shop with us for your fabulous shoez needs!!</p>");
                    //sb.AppendLine("<a href='https://shoedesignz.azurewebsites.net'> Link to ShoeDesignz </a>");
                    await _emailSender.SendEmailAsync(rvm.Email, "Thank you for Registering with ShoeDesignz!", sb.ToString());
                    var ourUser = await _userManager.FindByEmailAsync(rvm.Email);
                    string id = ourUser.Id;

                    //Adding Amanda to Admin role
                    if(user.Email == "amanda@codefellows.com" || user.Email.Contains("@codefellows.com") || user.Email == "castillocarlosr2@gmail.com")
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }
                    //Will redirect to new page
                    return RedirectToAction("Products", "Product");
                }
            }

            return View(rvm);
        }
        /// <summary>
        /// Gets the view from the login view page
        /// </summary>
        /// <returns>An awesome login page</returns>
        [HttpGet]
        public IActionResult Login() => View();

        /// <summary>
        /// One:  This is where the login enters their email and password.
        /// Two:  3rd-party OAtuho is implemented to allow for Facebook and Microsoft login.
        /// </summary>
        /// <param name="lvm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(LoginUser lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);

                if (result.Succeeded)
                {
                    //Send the user an email
                    //Moved to registration because I will send an email after checkout.
                    //string id = ourUser.Id;

                    //Adding user to admin role  Make Admin razor page carlos
                    var user = await _userManager.FindByEmailAsync(lvm.Email);
                    if(await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
                    {
                        await _emailSender.SendEmailAsync(lvm.Email, "Admin Login", "<h2>Admin has logged in.</h2> <h6>Thanks for looking at out ShoeDesignz store.</h6>");
                        var ourUser = await _userManager.FindByEmailAsync(lvm.Email);
                        return RedirectToPage("/Admin/Index");

                    }
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

        /// <summary>
        /// This will redirect to a page that displays that the user does not have the necessary Policy .edu email requirement.
        /// </summary>
        /// <returns>A big WARNING that you are not allowed without an EDU email.</returns>
        public IActionResult AccessDenied()
        {
            return View();
        }
        //********************External Log In setup below**************//
        /// <summary>
        /// Will send the external login information up to the necessary provider.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns>Facebook or Microsoft for now.</returns>
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

