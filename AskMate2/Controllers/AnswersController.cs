using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskMate2.Domain;

namespace AskMate2.Controllers
{
    
    public class AnswersController : Controller
    {
        CSV csv = new CSV();
        //private readonly IDataService _dataService;
        //public AnswersController(IDataService dataService)
        //{
        //    _dataService = dataService; //fugg valamelyiktol
        //    //program nemtudja mitol 
        //}

        public IActionResult AddAnswer()
        {
            foreach (Question question in csv.ReadFromCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Id.ToString() + ": " + question.Title);
            }
            return View("AddAnswer");
        }
        [HttpPost]
        public IActionResult AddAnswer([FromForm(Name = "question")] string question)
        {
            string text = "";
            int id = Int32.Parse(question.Split(":").ToArray()[0]);
            foreach (Question que in csv.ReadFromCSV("Questions.csv"))
            {
                if (que.Id == id)
                {
                    text = que.Text;
                }
            }
            ViewData.Add(question, text);
            return View("Answer"); // has to  be a Questions.cshtml
        }
        public IActionResult NewAnswer([FromForm(Name = "answer")] string answer, [FromForm(Name = "qID")] string qID)
        {
            csv.AnswerWriteToCSV(qID, answer, "Answers.csv");
            foreach (Question question in csv.ReadFromCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("AddAnswer"); // has to  be a Questions.cshtml
        }

        //public IActionResult ListQuestions()
        //{
        //    return View(_dataService.GetQuestions());
        //}

        //public IActionResult AddAnswer([FromForm(Name = "title")] string title, [FromForm(Name = "text")] string text)
        //{
        //    _dataService.AddQuestion(title, text);
        //    return View("List", _dataService.GetQuestions());
        //}


    }
}