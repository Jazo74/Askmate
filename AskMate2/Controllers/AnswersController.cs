using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AskMate2.Domain;
using AskMate2.Models;
using AskMate2.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace AskMate2.Controllers
{
    
    public class AnswersController : Controller
    {
        IDataService ds = new DBService();
        [Authorize]
        [HttpGet]
        public IActionResult AddAnswer()
        {
            foreach (Question question in ds.GetQuestions())
            {
                ViewData.Add(question.Id.ToString(), question.Id.ToString() + ": " + question.Title);
            }
            return View("AddAnswer");
        }

        [Authorize]
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

        [Authorize]
        public IActionResult NewAnswer([FromForm(Name = "answer")] string answer, [FromForm(Name = "qID")] string qID, [FromForm(Name = "image")] string image, [FromForm(Name = "currentUser")] string currentUser)
        {
            if (image == null || !image.StartsWith("https://"))
            {
                image = "https://";
            }
            ds.AddAnswer(ds.MakeAnswerWoId(qID, currentUser, answer, image));
            //foreach (Question question in ds.GetQuestions())
            //{
            //    ViewData.Add(question.Id.ToString(), question.Title);
            //}
            return RedirectToAction("ShowQe", "Questions", new { qid = QuestionsController.focusQid }); ;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ShowAnswers()
        {
            foreach (Question question in ds.GetQuestions())
            {
                ViewData.Add(question.Id.ToString(), question.Id.ToString() + ": " + question.Title);
            }
            return View("ShowAnswers");
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public IActionResult DeleteAnswer()
        {
            foreach (Question question in ds.GetQuestions())
            {
                ViewData.Add(question.Id.ToString(), question.Id.ToString() + ": " + question.Title);
            }
            return View("DeleteAnswer");
        }

        [Authorize]
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

        [Authorize]
        public IActionResult DelAnswer([FromForm(Name = "Aid")] string Aid)
        {
            ds.DeleteAnswer(Aid);
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult AnswerVote()
        {
            return View("VoteAnswer");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AnswerVote([FromForm(Name = "aId")] string answerId)
        {
            ds.AnswerVote(answerId);
            var uId = ds.GetUserFromAnswer(answerId);
            ds.IncreaseReputation(uId, 10);
            return RedirectToAction("ShowQe", "Questions", new { qid = QuestionsController.focusQid });
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult AnswerDownVote()
        {
            return View("VoteAnswer");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AnswerDownVote([FromForm(Name = "aId")] string answerId)
        {
            ds.AnswerDownVote(answerId);
            var uId = ds.GetUserFromAnswer(answerId);
            ds.DecreaseReputation(uId, 2);
            return RedirectToAction("ShowQe", "Questions", new { qid = QuestionsController.focusQid });
        }


        [Authorize]
        [HttpPost]
        public IActionResult AddImageToAnswer([FromForm(Name = "image")] string image, [FromForm(Name = "aid")] string answerId)
        {
            if (image == null || !image.StartsWith("https://"))
            {
                image = "https://";
            }
            ds.AddImageToAnswer(answerId, (image));
            return RedirectToAction("ShowQe", "Questions", new { qid = QuestionsController.focusQid });
        }

        [Authorize]
        [HttpGet]
        public IActionResult CommentToAnswer()
        {
            return View("CommentToAnswer");
        }

        [Authorize]
        [HttpPost]
        public IActionResult CommentToAnswer([FromForm(Name = "answerId")] string answerId, [FromForm(Name = "comment")] string comment, [FromForm(Name = "currentUser")] string currentUser)
        {
            ds.AddCommentAnswer(answerId, comment, currentUser);
            return RedirectToAction("ShowQe", "Questions", new { qid = QuestionsController.focusQid }); //EDIT (according to specifications)
        }



        [Authorize]
        [HttpGet]
        public IActionResult EditCommentAnswer()
        {
            return View("EditCommentAnswer");
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditCommentAnswer([FromForm(Name = "answerId")] string answerId, [FromForm(Name = "komment")] string komment)
        {
            ds.EditCommentAnswer(answerId, komment);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditAnswer([FromForm(Name = "Aid")] string answerId)
        {
            Transit transit = new Transit();
            Answer answer = ds.GetAnswer(answerId);
            transit.Aid = answer.AId;
            transit.AUserId = answer.AUserId;
            transit.Qid = answer.QId;
            transit.AsubmissionTime = answer.SubmissionTime;
            transit.Atext = answer.Text;
            transit.Aimage = answer.Image;
            transit.Aaccepted = answer.Aaccepted;
            return View("EditAnswer", transit);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditAnswer2([FromForm(Name = "Aid")] string answerId, [FromForm(Name = "message")] string message, [FromForm(Name = "image")] string image)
        {
            ds.EditAnswer(answerId, message, image);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AcceptedAnswer([FromForm(Name = "aId")] string aId)
        {
            ds.AccepAnswer(aId);
            var uid = ds.GetUserFromAnswer(aId);
            ds.IncreaseReputation(uid, 15);
            return RedirectToAction("ShowQe", "Questions", new { qid = QuestionsController.focusQid });
        }
    }
}