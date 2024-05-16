using ETLPipeline.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ETLPipeline.Services.Interface;
using System.Diagnostics;

namespace ETLPipeline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightTimerController : ControllerBase
    {
        private readonly IFlightsServices _flightService;

        public FlightTimerController(IFlightsServices flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        [Route("StartTimer")]
        public IActionResult StartTest()
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            var temp = _flightService.StartDataFetchingTimer();
            sw.Stop();
            return Ok(temp + sw.ElapsedMilliseconds);
        }

        [HttpPost]
        [Route("StopTimer")]
        public IActionResult StopTest()
        {
            return Ok(_flightService.StopDataFetchingTimer());
        }
    }
}
