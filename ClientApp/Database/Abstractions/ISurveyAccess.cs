using ClientApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Database.Abstractions
{
    public interface ISurveyAccess
    {
        Task<SurveyQuestion> GetQuestionForIndex(string qIndex);
    }
}
