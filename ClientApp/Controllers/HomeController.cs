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
    public class HomeController : Controller
    {
        private readonly IForecastAccess _forecastAccess;
        public HomeController(IForecastAccess forecastAccess)
        {
            _forecastAccess = forecastAccess;
        }

        [HttpGet]
        //returns view upon accessing index page
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //posts to server when index calls post
        public IActionResult Index(string zipcode)
        {
            // If we were saving anything to the database this is where we would call the manager to do so.
            return RedirectToAction("WeatherReport", new { zipcode });
        }

        /*[HttpGet]
        public IActionResult Testy()
        {
            TestyViewModel toRet = new TestyViewModel()
            {
                Dingdong = "whats up zorbosd",
            };
            return View(toRet);
        }*/

        [HttpGet]
        //returns view upon accessing Weather Report page; gets params from ? url
        public async Task<IActionResult> WeatherReport(string zipcode)
        {
            WeatherForecast forecast = await _forecastAccess.GetForecastForZipcode(zipcode);

            WeatherReportViewModel toRet = new WeatherReportViewModel()
            {
                Description = forecast.Summary,
                TemperatureC = forecast.TemperatureC.ToString(),
                TemperatureF = forecast.TemperatureF.ToString(),
                Zipcode = forecast.Zipcode
            };

            if (forecast.TemperatureC < 5)
            {
                toRet.TemperatureClass = "freezing";
            }
            else if (forecast.TemperatureC < 15)
            {
                toRet.TemperatureClass = "cold";
            }
            else
            {
                toRet.TemperatureClass = "warm";
            }

            return View(toRet);
        }
    }
}
