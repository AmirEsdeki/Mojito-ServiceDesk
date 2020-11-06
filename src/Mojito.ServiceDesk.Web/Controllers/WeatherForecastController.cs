using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Web.Modules.AutoWrapper;
using System;
using System.Linq;
using System.Net;

namespace Mojito.ServiceDesk.Web.Controllers
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
        [ProducesResponseType(typeof(WeatherForecast[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public ApiResponse Get()
        {
            //try
            //{
            //    throw new InvalidTokenException();
            //    //throw new NullReferenceException();

            //}
            //catch (CustomException ex)
            //{
            //    throw new ApiException(ex, ex.StatusCode);
            //    throw;
            //}
            //catch (Exception ex)
            //{
            //    throw new ApiException(ex);
            //}

            //            throw new InvalidTokenException();
            //throw new EntityDoesNotExistException();
            var rng = new Random();
            var data = Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return new ApiResponse("New record has been created in the database.", data, (int)HttpStatusCode.Created);
        }
    }
}
