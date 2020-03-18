using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2;
using AskMate2.Domain;

namespace AskMate2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    public class QuestionsController : Controller
    {
        CSV csv = new CSV();
        
        [HttpGet("list")] // <--- this is what you write after {PORT}
        public IActionResult ListQuestions()
        {
            foreach (Question question in csv.ReadFromCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
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
            csv.QuestionWriteToCSV(title, text, "Questions.csv");
            foreach (Question question in csv.ReadFromCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("ListQuestions"); // has to  be a Questions.cshtml
        }



    }
}
