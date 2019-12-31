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

        [HttpPost]
        public ActionResult<string> Add()
        {
            //使用仓储来处理聚合
            _itineraryRepository.Add(new Itinerary("奥特曼", "赛文奥特曼", "杰克奥特曼", "佐菲奥特曼", "泰罗奥特曼"));
            _itineraryRepository.Add(new Itinerary("盖亚奥特曼", "戴拿奥特曼", "阿古茹奥特曼", "迪迦奥特曼", ""));

            return "success";
        }

        [HttpGet]
        public ActionResult<long> Get()
        {
            var count = _itineraryRepository.GetCount();
            return count;
        }
    }
}
