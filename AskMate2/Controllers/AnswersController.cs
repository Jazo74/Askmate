using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskMate2.Domain;
using AskMate2.Models;

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
            string id = question.Split(":").ToArray()[0];
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
            List<Transit> transitLst = new List<Transit>();
            string id = question.Split(":").ToArray()[0];
            string qtext = "";
            foreach (Question que in csv.ReadFromQuestionsCSV("Questions.csv"))
            {
                if (id == que.Id)
                {
                    qtext = que.Text;
                }
            }
            foreach (Answer answer in csv.ReadFromAnswersCSV("Answers.csv"))
            {
                if (answer.QId == id)
                {
                    Transit transit = new Transit();
                    text = answer.Text;
                    transit.Qid = id.ToString();
                    transit.Aid = answer.AId.ToString();
                    transit.Qtitle = question.Split(":").ToArray()[1];
                    transit.Qtext = qtext;
                    transit.Atext = answer.Text;
                    transitLst.Add(transit);
                }
            }
            
            return View("ShowA", transitLst);
        }

        public IActionResult DeleteAnswer([FromForm(Name = "answer")] string answer)
        {
            csv.DeleteAnswer(Int32.Parse(answer.Split(":").ToArray()[0]));
            return View("DeleteAnswer");
        }


    }
}