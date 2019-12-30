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

        //begin button
        [HttpPost]
        public IActionResult Index(string qIndex)
        {
            return RedirectToAction("Question", new { qIndex });
        }
        
        //base question
        [HttpGet]
        public async Task<IActionResult> Question(string qIndex)
        {
            //keep the index within real parameters
            if(int.Parse(qIndex) < 1) {
                return RedirectToAction("Question", new { qIndex = "1" });
            }

            //retrieve question model
            SurveyQuestion question = await _surveyAccess.GetQuestionForIndex(qIndex);            
            SurveyQuestionViewModel toRet = new SurveyQuestionViewModel()
            {
                QIndex = question.QIndex,
                QuestionText = question.Question,
                QType = question.QType,
                Answers = question.Answers,
            };

            //if this question has the "complete" type, swap to a new action
            //temporary measures to end the survey without exceeding the database; would be changed in later dev
            if (toRet.QType == "Complete")
            {
                return RedirectToAction("EndSurvey");
            }

            //parse the answers string into discrete questions
            toRet.AnswersArray = new List<string>();
            if (toRet.Answers != null) {
                string[] splitter = question.Answers.Split(',');

                foreach (string phrase in splitter) {
                    toRet.AnswersArray.Add(phrase);
                }
            }
            else {
                toRet.Answers = "enter your answer...";
                toRet.AnswersArray.Add("enter your answer...");
            }
            
            //return the given question
            return View(toRet.QType, toRet);
        }

        [HttpPost]
        public IActionResult Question(string answer, string qIndex)
        {
            //as dev continues, this would record the answer
            return RedirectToAction("Question", new { qIndex });
        }

        [HttpGet]
        public IActionResult EndSurvey()
        {
            return View("Complete");
        }

        //allow patient to back up
        [HttpPost]
        public IActionResult EndSurvey(string qIndex)
        {
            return RedirectToAction("Question", new { qIndex });
        }

    }
}
