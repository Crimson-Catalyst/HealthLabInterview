using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientApp.Database.Abstractions;
using ClientApp.Database.Models;
using ClientApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ClientApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ISurveyAccess _surveyAccess;
        public SurveyController(ISurveyAccess surveyAccess)
        {
            _surveyAccess = surveyAccess;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string qIndex)
        {
            return RedirectToAction("Question", new { qIndex });
        }

        //question testing
        [HttpGet]
        public IActionResult ShortAnswer()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Question(string qIndex)
        {
            SurveyQuestion question = await _surveyAccess.GetQuestionForIndex(qIndex);

            SurveyQuestionViewModel toRet = new SurveyQuestionViewModel()
            {
                QIndex = question.QIndex,
                QuestionText = question.Question,
                QType = question.QType,
                Answers = question.Answers,
            };

            //parse the answers string into discrete questions
            toRet.AnswersArray = new List<string>();
            if (toRet.Answers != null)
            {
                string[] splitter = question.Answers.Split(',');

                foreach (string phrase in splitter)
                {
                    toRet.AnswersArray.Add(phrase);
                }
            }
            else
            {
                toRet.AnswersArray.Add("enter your answer...");
            }

            //return the given question
            return View(toRet);
        }
    }
}
