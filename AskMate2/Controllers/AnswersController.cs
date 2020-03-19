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
        
        public IActionResult AddAnswer()
        {
            foreach (Question question in csv.ReadFromQuestionsCSV("Questions.csv"))
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
            foreach (Question que in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                if (que.Id == id)
                {
                    text = que.Text;
                }
            }
            ViewData.Add(question, text);
            return View("Answer"); 
        }
        public IActionResult NewAnswer([FromForm(Name = "answer")] string answer, [FromForm(Name = "qID")] string qID)
        {
            csv.AnswerWriteToCSV(qID, answer, "Answers.csv");
            foreach (Question question in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("AddAnswer");
        }
        [HttpGet]
        public IActionResult ShowAnswers()
        {
            foreach (Question question in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                ViewData.Add(question.Id.ToString(), question.Id.ToString() + ": " + question.Title);
            }
            return View("ShowAnswers");
        }
        [HttpPost]
        public IActionResult ShowAnswers([FromForm(Name = "question")] string question)
        {
            string text = "";
            int id = Int32.Parse(question.Split(":").ToArray()[0]);
            foreach (Answer que in csv.ReadFromAnswersCSV("Answers.csv"))
            {
                if (que.QId == id)
                {
                    text = que.Text;
                }
            }
            ViewData.Add(question, text);
            return View("ShowAnswers");
        }




    }
}