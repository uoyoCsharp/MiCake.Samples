using System;
using System.Collections.Generic;
using System.Linq;
using EasyUowApplication.Aggregates;
using EasyUowApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyUowApplication.Controllers
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
        private readonly ItineraryRepository _itineraryRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ItineraryRepository itineraryRepository)
        {
            _logger = logger;
            _itineraryRepository = itineraryRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //使用仓储来处理聚合
            _itineraryRepository.Add(new Itinerary("1", "2", "3", "4", "5"));
            _itineraryRepository.Add(new Itinerary("12", "22", "23", "24", "52"));
            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
