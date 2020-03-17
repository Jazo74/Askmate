using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    public class QuestionsController : Controller
    {
        [HttpGet("list")] // <--- this is what you write after {PORT}
        public IActionResult ListQuestions()
        {
            return View("ListQuestions"); // has to  be a Questions.cshtml
        }
        [HttpGet("AddQuestion")]
        public IActionResult AddQuestion()
        {
            return View("AddQuestion"); // has to  be a Questions.cshtml
        }
        [HttpPost]
        public IActionResult AddQuestion([FromForm(Name="title")] string title, [FromForm(Name = "text")] string text)
        {
            return View("AddQuestion"); // has to  be a Questions.cshtml
        }



    }
}
