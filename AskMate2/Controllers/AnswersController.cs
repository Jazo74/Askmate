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
        private readonly IDataService _dataService;


        public AnswersController(IDataService dataService)
        {
            _dataService = dataService; //fugg valamelyiktol
            //program nemtudja mitol 
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