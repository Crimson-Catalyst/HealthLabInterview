using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database.Abstractions
{
    public interface ISurveyAccess
    {
        Task<SurveyQuestion> GetQuestionByIndex(string qIndex);
    }
}
