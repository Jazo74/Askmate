using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AskMate2.Domain;
using AskMate2.Models;
using AskMate2.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AskMate2.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService = new UserHandler();
        
        [HttpGet] //MISSING login page
        public IActionResult Login()
        {
            return View("Login");
        }
        
      
        [HttpPost]
        public async Task<ActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            List<User> allUsers = _userService.GetAllUsers();

            User user = _userService.Login(email, password);
            
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }


            var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            return View("Logout");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Logout([FromForm] string hidden)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public IActionResult Register([FromForm] string user_id, [FromForm] string email, [FromForm] string password)
        {


            _userService.RegisterUser(user_id, email, password);

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpGet]
        public IActionResult AllUsers()
        {
            List<UserTransit> allUser = _userService.GetAllUsersModel();
            
            return View("AllUsers", allUser);
        }




    }
}