using ETLPipeline.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ETLPipeline.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightAPIServices _flightAPIServices;
        public FlightsController(FlightAPIServices flightAPIServices)
        {
            _flightAPIServices = flightAPIServices; 
        }

        [HttpGet(Name = "GetFlights")]
        public async Task<IActionResult> Get()
        {
            var flights = await _flightAPIServices.GetFlights();
            if (flights == null)
            {
                return NotFound(flights);
            }
            return Ok(flights);
        }
    }
}
