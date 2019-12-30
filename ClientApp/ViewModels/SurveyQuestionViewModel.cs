using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.ViewModels
{
    public class SurveyQuestionViewModel
    {
        public string QIndex { get; set; }
        public string QuestionText { get; set; }
        //ideally, QType would be an enum and not a string as dev continues
        public string QType { get; set; }
        public string Answers { get; set; }
        public List<string> AnswersArray { get; set; }
    }
}
