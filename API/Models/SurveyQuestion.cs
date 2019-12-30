using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace API.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int QuestionID { get; set; }
        public string QIndex { get; set; }
        public string Question { get; set; }
        //ideally, QType would be an enum and not a string as dev continues
        public string QType { get; set; }
        public string Answers { get; set; }
    }
}
