using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2;
using AskMate2.Domain;
using AskMate2.Models;

namespace AskMate2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    public class QuestionsController : Controller
    {
        CSV csv = new CSV();
        
        [HttpGet("list")] // <--- this is what you write after {PORT}
        public IActionResult ListQuestions()
        {
            foreach (Question question in csv.ReadFromQuestionsCSV("Questions.csv"))
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
            foreach (Question question in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("ListQuestions"); // has to  be a Questions.cshtml
        }

        public IActionResult DeleteQuestion([FromForm(Name = "DelId")] int delId)
        {
            foreach (Question question in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("DeleteQuestion");
        }
        [HttpPost]
        public IActionResult DeleteQuestion([FromForm(Name = "question")] string question)
        {
            csv.DeleteQuestion(question.Split(":").ToArray()[0]);
            return View("DeleteQuestion");
        }
        [HttpGet]
        public IActionResult ShowQuestion()
        {
            foreach (Question que in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                ViewData.Add(que.Id + ": " + que.Title, que.Text);
            }
            return View("ShowQuestion");
        }
        [HttpPost]
        public IActionResult ShowQuestion([FromForm(Name = "question")] string question)
        {
            string id = question.Split(":").ToArray()[0];
            foreach (Question que in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                if (que.Id == id)
                {
                    ViewData.Add(question, que.Text);
                }
            }
            return View("ShowQ");
        }
        [HttpGet]
        public IActionResult EditQuestion1()
        {
            List<Transit> transList = new List<Transit>();
            List<Question> questions = csv.ReadFromQuestionsCSV("Questions.csv");
            foreach (Question qst in questions)
            {
                Transit transit = new Transit();
                transit.Qid = qst.Id;
                transit.Qtitle = qst.Title;
                transit.Qtext = qst.Text;
                transList.Add(transit);
            }
            return View("EditQuestion1", transList);
        }
        public IActionResult EditQuestion2([FromForm(Name = "editTitle")] string que)
        {
            Transit transit = new Transit();
            List<Question> questions = csv.ReadFromQuestionsCSV("Questions.csv");
            foreach (Question qst in questions)
            {
                if (que.Split(":").ToArray()[0] == qst.Id)
                {
                    transit.Qid = qst.Id;
                    transit.Qtitle = qst.Title;
                    transit.Qtext = qst.Text;
                }
            }
            return View("EditQuestion2", transit);
        }
        [HttpPost]
        public IActionResult EditQuestion3([FromForm(Name = "editId")] string id,
                                          [FromForm(Name = "editTitle")] string title,
                                          [FromForm(Name = "editText")] string text)
        {
            csv.EditQuestion(id, title, text);
            return RedirectToAction("Index","Home");
        }
        
        






    }
}
