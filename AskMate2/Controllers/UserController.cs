using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2;
using AskMate2.Domain;
using AskMate2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AskMate2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    public class UserController : Controller 
    { 
        public static string focusQid = "";
        IDataService ds = new DBService();

        [Authorize]
        [HttpGet]
        public IActionResult user()
        {
            string currentUser = ds.GetUserId(HttpContext.User.FindFirstValue(ClaimTypes.Email));
            ViewData.Add("currentUser", currentUser);
            QAC qac = ds.GetQACByUserId(currentUser);
            return View("user", qac);
        }
    }
}