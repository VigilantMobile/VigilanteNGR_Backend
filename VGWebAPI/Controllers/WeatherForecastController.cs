using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VGWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        
        public JsonResult Get()
        {
            var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();

            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
           .ToArray();

            var result = new
            {
                status = "success",
                data = data
            };

            return new JsonResult(result);

        }

        [HttpGet("GetText1")] 
        public JsonResult GetText1()
        {
            var result = new
            {
                status = "success",
                data = "Lorem Ipsum 1 - Sunny Today"
            };

            return new JsonResult(result);
        }

        [HttpGet("GetText2")]
        public JsonResult GetText2()
        {
            var result = new
            {
                status = "success",
                data = "Lorem Ipsum 2 - Windy Day"
            };

            return new JsonResult(result);
        }

        [HttpGet("GetText3")]
        public JsonResult GetText3()
        {
            var result = new
            {
                status = "success",
                data = "Lorem Ipsum 3 - Stormy Day"
            };

            return new JsonResult(result);
        }

        [HttpGet("GetText4")]
        public JsonResult GetText4()
        {
            var result = new
            {
                status = "success",
                data = "Lorem Ipsum 4 - Hot Today"
            };

            return new JsonResult(result);
        }

        [HttpGet("GetText5")]
        public JsonResult GetText5()
        {
            var result = new
            {
                status = "success",
                data = "Lorem Ipsum 5 - Icy and Chilly Today"
            };

            return new JsonResult(result);

        }
    }
}
