using ClientApp.Database.Abstractions;
using ClientApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp.Database
{
    public class SurveyAccess : ISurveyAccess
    {
        private readonly IHttpClientFactory _clientFactory;

        public SurveyAccess(IHttpClientFactory factory)
        {
            _clientFactory = factory;
        }

        public async Task<SurveyQuestion> GetQuestionForIndex(string qIndex)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:5001/SurveyQuestion/GetByQIndex/{qIndex}");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            SurveyQuestion toRet;

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                toRet = await JsonSerializer.DeserializeAsync<SurveyQuestion>(stream);
            }
            else
            {
                throw new Exception("Request for survey question failed");
            }

            return toRet;
        }
    }
}
