using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Database.Models
{
    public class SurveyQuestion
    {
        public string QIndex { get; set; }
        public string Question { get; set; }
        //ideally, QType would be an enum and not a string as dev continues
        public string QType { get; set; }
        public string Answers { get; set; }
        public List<string> AnswersArray { get; set; }
    }
}
