using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.IO;

namespace JsonXml.Controllers
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
        private readonly IWebHostEnvironment _web;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWebHostEnvironment web)
        {
            _logger = logger;
            _web = web;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post(IFormFile book)
        {
            Service.Converter(_web, book);            

            //XmlDocument doc = new XmlDocument();
            //doc.Load(request.FileName);

            //XmlNode? node = doc.SelectSingleNode("/Book");

            //var newBook = new Book
            //{
            //    id = int.Parse(node["id"].InnerText),
            //    title = node["title"].InnerText,
            //    author = node["author"].InnerText
            //};

            return Ok(book);
        }
    }
}