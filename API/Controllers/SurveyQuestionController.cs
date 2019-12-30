using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database.Abstractions;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntervieweeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveyQuestionController : ControllerBase
    {
        private readonly ISurveyAccess _surveyAccess;
        public SurveyQuestionController(ISurveyAccess surveyAccess)
        {
            _surveyAccess = surveyAccess;
        }

        [HttpGet("GetByQIndex/{qIndex}")]
        public async Task<IActionResult> GetByQIndexAsync([FromRoute] string qIndex)
        {
            SurveyQuestion question = await _surveyAccess.GetQuestionByIndex(qIndex);
            
            return new JsonResult(question);
        }
    }
}
