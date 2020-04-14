using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AskMate2.Controllers
{
    public class SinglePageController : Controller
    {
        public IActionResult SinglePage()
        {
            return View();
        }
    }
}