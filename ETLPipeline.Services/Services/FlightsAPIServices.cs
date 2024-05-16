using ETLPipeline.Repository.Models;
using Microsoft.Extensions.Logging;
namespace ETLPipeline.Services.Services
{
    public class FlightAPIServices : RestClientCaller.RestClientCaller
    {
        private readonly ILogger<FlightAPIServices> _logger;
        public readonly string _endpoint = "states/all?lamin=54.963435&lomin=8.275856&lamax=57.817087&lomax=10.870537";
        public FlightAPIServices(ILogger<FlightAPIServices> logger)
        {
            _logger = logger;
        }

        public async Task<List<Flights>> GetFlights()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

                

                var flightsModel = await CallEndpointAsync<FlightsModel>(_endpoint);

                if (flightsModel == null || flightsModel.states == null)
                {
                    _logger.LogWarning("Received null or incomplete data from the API.");
                    return [];
                }

                _logger.LogInformation($"Received {flightsModel.states.Count} states from the API.");

                var temp = flightsModel.states.Where(f => f != null).Select(f => new Flights
                {
                    Icao24 = f[0]?.ToString(),
                    Callsign = f[1]?.ToString(),
                    OriginCountry = f[2]?.ToString(),
                    TimePosition = Convert.ToInt32(f[3]?.ToString()),
                    LastContact = Convert.ToInt32(f[4]?.ToString()),
                    Longitude = Convert.ToDouble(f[5]?.ToString()),
                    Latitude = Convert.ToDouble(f[6]?.ToString()),
                    BaroAltitude = Convert.ToDouble(f[7]?.ToString()),
                    OnGround = Convert.ToBoolean(f[8]?.ToString()),
                    Velocity = Convert.ToDouble(f[9]?.ToString()),
                    TrueTrack = Convert.ToDouble(f[10]?.ToString()),
                    VerticalRate = Convert.ToDouble(f[11]?.ToString()),

                    Sensors = f[12]?.ToString()
                        .Split(',')
                        .Select(s => Convert.ToInt32(s))
                        .ToArray(),

                    GeoAltitude = Convert.ToDouble(f[13]?.ToString()),
                    Squawk = f[14]?.ToString(),
                    Spi = Convert.ToBoolean(f[15]?.ToString()),
                    PositionSource = (Repository.Models.PositionSource)Convert.ToInt32(f[16]?.ToString()),
                }).ToList();

                return temp;
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error occurred while retrieving flights from the API.");
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

    }
}