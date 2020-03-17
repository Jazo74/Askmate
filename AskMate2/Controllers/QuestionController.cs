using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    public class QuestionController : Controller
    {
        [HttpGet("list")] // <--- this is what you write after {PORT}
        public IActionResult List()
        {
            return View("Question"); // has to  be a Question.cshtml
        }



    }
}
