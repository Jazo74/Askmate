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
            ViewData.Add("1. kerdes cime", "elso kerdes szovegtozs");
            ViewData.Add("2. kerdes cime", "masodik kerdes szovegtozs");
            ViewData.Add("3. kerdes cime", "harmadik kerdes szovegtozs");
            ViewData.Add("4. kerdes cime", "negyedik kerdes szovegtozs");
            ViewData.Add("5. kerdes cime", "otodik kerdes szovegtozs");
            return View("ListQuestions"); // has to  be a Questions.cshtml
        }
        [HttpPost]
        public IActionResult Question([FromForm(Name = "question-title")] string title, [FromForm(Name = "question-id")] string id)
        {
            ViewData.Add(title, "id1");
            return View("Question"); // has to  be a Questions.cshtml
        }
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
