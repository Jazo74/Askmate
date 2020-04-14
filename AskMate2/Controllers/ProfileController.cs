using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AskMate2.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AskMate2.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            //ILogger<ProfileController> logger MISSING form parameters
            _userService = userService;
        }


        public IActionResult Index()
        {
            var email = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            User user = _userService.GetUserByEmail(email);

            return View(new 
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            });
        }
    }
}