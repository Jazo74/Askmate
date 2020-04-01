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
        IDataService ds = new DBService();
        [HttpGet]
        public IActionResult AddAnswer()
        {
            foreach (Question question in ds.GetQuestions())
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
            foreach (Question que in ds.GetQuestions())
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
            
            ds.AddAnswer(ds.MakeAnswerWoId(qID, answer));
            //foreach (Question question in ds.GetQuestions())
            //{
            //    ViewData.Add(question.Id.ToString(), question.Title);
            //}
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult ShowAnswers()
        {
            foreach (Question question in ds.GetQuestions())
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
            foreach (Question que in ds.GetQuestions())
            {
                if (id == que.Id)
                {
                    Transit transit = new Transit();
                    transit.Qid = id.ToString();
                    transit.Qtitle = question.Split(":").ToArray()[1];
                    transit.Qtext = que.Text;
                    List<Answer> answers = ds.GetAnswers(que.Id);

                    foreach (Answer answer in answers)
                    {
                        text = answer.Text;
                        transit.Aid = answer.AId.ToString();
                        transit.Atext = answer.Text;
                        transitLst.Add(transit); 
                    }
                }
            }
            
            return View("ShowA", transitLst);
        }
        [HttpGet]
        public IActionResult DeleteAnswer()
        {
            foreach (Question question in ds.GetQuestions())
            {
                ViewData.Add(question.Id.ToString(), question.Id.ToString() + ": " + question.Title);
            }
            return View("DeleteAnswer");
        }
        [HttpPost]
        public IActionResult DeleteAnswer([FromForm(Name = "question")] string question)
        {
            string text = "";
            List<Transit> transitLst = new List<Transit>();
            string id = question.Split(":").ToArray()[0];
            string qtext = "";
            foreach (Question que in ds.GetQuestions())
            {
                if (id == que.Id)
                {
                    qtext = que.Text;
                }
            }
            foreach (Answer answer in ds.GetAnswers(id))
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
            return View("DeleteA", transitLst);

        }

        public IActionResult DelAnswer([FromForm(Name = "Aid")] string Aid)
        {
            ds.DeleteAnswer(Aid);
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public IActionResult AnswerVote()
        {
            return View("VoteAnswer");
        }

        [HttpPost]
        public IActionResult AnswerVote([FromForm(Name = "answerId")] string answerId)
        {
            ds.AnswerVote(answerId);
            return RedirectToAction("Index", "Home");
        }









        [HttpGet]
        public IActionResult CommentToAnswer()
        {
            return View("CommentToAnswer");
        }

        [HttpPost]
        public IActionResult CommentToAnswer(string answerId, string komment)
        {
            ds.AddCommentAnswer(answerId, komment);
            return RedirectToAction("Index", "Home"); //EDIT (according to specifications)
        }
    }
}