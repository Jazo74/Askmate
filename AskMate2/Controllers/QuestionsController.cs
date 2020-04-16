using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AskMate2;
using AskMate2.Domain;
using AskMate2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AskMate2.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    public class QuestionsController : Controller
    {
        public static string focusQid = "";
        IDataService ds = new DBService();

        [Authorize]
        [HttpGet("list")] // <--- this is what you write after {PORT}
        public IActionResult ListQuestions()
        {
            string currentUser = "";
            currentUser = ds.GetUserId(HttpContext.User.FindFirstValue(ClaimTypes.Email));
            ViewData.Add("currentUser", currentUser);

            List<Transit> transitList = new List<Transit>();
            foreach (Question question in ds.GetQuestions())
            {
                Transit transit = new Transit();
                transit.Qid = question.Id.ToString();
                transit.QUserId = question.UserId;
                transit.Qtitle = question.Title;
                transit.Qtext = question.Text;
                transit.Qview = question.ViewNumber;
                transit.Qvote = question.VoteNumber;
                transit.QsubmissionTime = question.SubmissionTime;
                transit.Qimage = question.Image;
                transitList.Add(transit);
            }

            if (currentUser == "")
            {
                return View("AltListQuestionsOpen", transitList);
            } else
            {
                return View("AltListQuestions", transitList);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddQuestion()
        {
            return View("AddQuestion");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddQuestion([FromForm(Name = "title")] string title, [FromForm(Name = "text")] string text, [FromForm(Name = "currentUser")] string currentUser, [FromForm(Name = "image")] string image)
        {
            if (image == null || !image.StartsWith("https://"))
            {
                image = "https://";
            }
            ds.AddQuestion(ds.MakeQuestionWoId(title, currentUser, text, 0, 0, DateTime.Now, image));
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult DeleteQuestion([FromForm(Name = "DelId")] int delId)
        {
            foreach (Question question in ds.GetQuestions())
            {
                ViewData.Add(question.Id.ToString(), question.Title);
            }
            return View("DeleteQuestion");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteQuestion([FromForm(Name = "qId")] string qId)
        {
            ds.DeleteQuestion(qId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ShowQuestion()
        {
            List<Transit> transitList = new List<Transit>();
            foreach (Question que in ds.GetQuestions())
            {
                Transit transit = new Transit();
                transit.Qid = que.Id;
                transit.Qtitle = que.Title;
                transit.Qtext = que.Text;
                transitList.Add(transit);
            }
            return View("ShowQuestion", transitList);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ShowQ([FromForm(Name = "question")] string question)
        {
            string qid = question.Split(":").ToArray()[0];
            ds.ViewIncrement(qid);
            return View("ShowQ", ds.GetQuestionWithAnswers(qid));

        }

        [Authorize]
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("/Questions/ShowQe/{qid}")]
        public IActionResult ShowQe(string qid)
        {
            string currentUser = "";
            currentUser = ds.GetUserId(HttpContext.User.FindFirstValue(ClaimTypes.Email));
            ViewData.Add("currentUser", currentUser);
            focusQid = qid;
            ds.ViewIncrement(qid);
            if (currentUser == "")
            {
                return View("ShowQOpen", ds.GetQuestionWithAnswers(qid));
            }
            else
            {
                return View("ShowQ", ds.GetQuestionWithAnswers(qid));

            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditQuestion1()
        {
            List<Transit> transList = new List<Transit>();
            List<Question> questions = ds.GetQuestions();
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

        [Authorize]
        public IActionResult EditQuestion2([FromForm(Name = "editTitle")] string que)
        {
            Transit transit = new Transit();
            List<Question> questions = ds.GetQuestions();
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

        [Authorize]
        [HttpPost]
        public IActionResult EditQuestion3([FromForm(Name = "editId")] string id,
                                          [FromForm(Name = "editTitle")] string title,
                                          [FromForm(Name = "editText")] string text)
        {
            ds.UpdateQuestion(id, title, text);
            return RedirectToAction("Index","Home");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Vote()
        {
            List<Transit> transit_list = new List<Transit>();

            foreach (Question que in ds.GetQuestions())
            {
                Transit transit = new Transit();
                transit.Qid = que.Id;
                transit.Qtitle = que.Title;
                transit.Qtext = que.Text;
                transit.Qvote = que.VoteNumber;
                transit_list.Add(transit);
            }
            return View("Vote", transit_list);
        }
        //Change from Vote -> Vote2

        [Authorize]
        [HttpPost]
        public IActionResult Vote([FromForm(Name = "question")] string question)
        {
            string qId = question.Split(":").ToArray()[0];
            ds.Vote(qId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult VoteForQuestion([FromForm(Name = "qId")] string qId)
        {
            ds.Vote(qId);
            var uId = ds.GetUserFromQuestion(qId);
            ds.IncreaseReputation(uId, 5); //reputation increase by 5
            return RedirectToAction("ShowQe", "Questions", new { qid = focusQid });
        }

        [Authorize]
        [HttpPost]
        public IActionResult DownVoteForQuestion([FromForm(Name = "qId")] string qId)
        {
            ds.DownVote(qId);
            var uId = ds.GetUserFromQuestion(qId);
            ds.DecreaseReputation(uId, 2); //reputation increase by 5
            return RedirectToAction("ShowQe", "Questions", new { qid = focusQid });
        }

        


        [Authorize]
        [HttpPost]
        public IActionResult ShowQWithSelect([FromForm(Name = "wordToSearch")] string word,
                                          [FromForm(Name = "fromTimeToSearch")] DateTime fromTime,
                                          [FromForm(Name = "toTimeToSearch")] DateTime toTime,
                                          [FromForm(Name = "fromVoteSearch")] int fromVote)
        {
            List<Transit> transitList = new List<Transit>();
            List<Question> questionList = ds.GetQuestions(word, fromVote, fromTime, toTime);
            foreach (Question que in questionList)
            {
                Transit transit = new Transit();
                transit.Qid = que.Id;
                transit.Qtitle = que.Title;
                transit.Qtext = que.Text;
                transit.Qvote = que.VoteNumber;
                transit.QsubmissionTime = que.SubmissionTime;
                transitList.Add(transit);
            }
            return View("ALtListQuestions", transitList);
            return RedirectToAction("AltListQuestions",transitList);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ShowQLatestSelect([FromForm(Name = "latestX")] int latestX)
        {
            List<Transit> transitList = new List<Transit>();
            List<Question> questionList = ds.GetQuestions(latestX);
            foreach (Question que in questionList)
            {
                Transit transit = new Transit();
                transit.Qid = que.Id;
                transit.Qtitle = que.Title;
                transit.Qtext = que.Text;
                transit.Qvote = que.VoteNumber;
                transit.QsubmissionTime = que.SubmissionTime;
                transitList.Add(transit);
            }
            return View("ALtListQuestions", transitList);
            return RedirectToAction("AltListQuestions", transitList);
        }

        //[HttpPost]
        //public IActionResult AnswerVote([FromForm(Name = "answer")] string answer)
        //{
        //    string aId = answer.Split(":").ToArray()[0];
        //    ds.Vote(aId);
        //    return RedirectToAction("Index", "Home");
        //}

        [Authorize]
        [HttpGet]
        public IActionResult CommentToQuestion()
        {
            return View("CommentToQuestion");
        }

        [Authorize]
        [HttpPost]
        public IActionResult CommentToQuestion([FromForm(Name = "questionId")] string questionId, [FromForm(Name = "comment")] string comment, [FromForm(Name = "currentUser")] string currentUser)
        {
            ds.AddCommentQuestion(questionId, comment, currentUser);
            return RedirectToAction("ShowQe", "Questions", new { qid = focusQid });
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddImageToQuestion()
        {
            return View("AddImage");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddImageToQuestion([FromForm(Name = "image")] string image, [FromForm(Name ="qid")] string questionId)
        {
            ds.AddImageToQuestion(questionId, image);
            return RedirectToAction("ShowQe", "Questions", new { qid = focusQid });
        }

        [Authorize]
        [HttpGet]
        public IActionResult EditCommentQuestion()
        {
            return View("EditCommentQuestion");
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditCommentQuestion([FromForm(Name = "questionId")] string questionId, [FromForm(Name = "komment")] string komment)
        {
            ds.EditCommentQuestion(questionId, komment);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult DeleteCommentQuestion()
        {
            return View("DelQ");
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteCommentQuestion([FromForm(Name = "questionId")] string questionId)
        {
            ds.DeleteCommentQuestion(questionId);
            return RedirectToAction("Index", "Home");
        }
    }
}
