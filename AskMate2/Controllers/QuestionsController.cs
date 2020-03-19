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
        [HttpGet]
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

        public IActionResult DeleteQuestion([FromForm(Name = "DelId")] int delId)
        {
            foreach (Question question in csv.ReadFromCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("DeleteQuestion");
        }
        [HttpPost]
        public IActionResult DeleteQuestion([FromForm(Name = "question")] string question)
        {
            csv.DeleteQuestion(Int32.Parse(question.Split(":").ToArray()[0]));
            return View("DeleteQuestion");
        }
        [HttpGet]
        public IActionResult ShowQuestion()
        {
            foreach (Question que in csv.ReadFromCSV("Questions.csv"))
            {
                ViewData.Add(que.Id + ": " + que.Title, que.Text);
            }
            return View("ShowQuestion");
        }
        [HttpPost]
        public IActionResult ShowQuestion([FromForm(Name = "question")] string question)
        {
            int id = Int32.Parse(question.Split(":").ToArray()[0]);
            foreach (Question que in csv.ReadFromCSV("Questions.csv"))
            {
                if (que.Id == id)
                {
                    ViewData.Add(question, que.Text);
                }
            }
            return View("ShowQ");
        }

        public IActionResult EditQuestion([FromForm(Name = "editId")] int id, [FromForm(Name = "editTitle")] string title, [FromForm(Name = "editText")] string text)
        {
            csv.EditQuestion(id, title, text);
            return View("EditQuestion");
        }
    }
}
