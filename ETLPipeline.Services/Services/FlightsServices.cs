using ETLPipeline.Repository.DBContext;
using ETLPipeline.Repository.Models;
using ETLPipeline.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPipeline.Services.Services
{
    public class FlightsServices : IFlightsServices
    {
        private readonly ETLPipelineContext _context;
        private readonly FlightAPIServices _flightAPIServices;
        private readonly ILogger<FlightsServices> _logger;
        private Timer? _timer { get; set; }
        private bool _timerRunning { get; set; } = false;
        public FlightsServices(ETLPipelineContext context, FlightAPIServices flightAPIServices, ILogger<FlightsServices> logger)
        {
            _context = context;
            _flightAPIServices = flightAPIServices;
            _logger = logger;
        }

        public async Task<List<Flights>> AddFlightsToDb(IEnumerable<Flights> flights)
        {
            List<Flights> insertdataflights = [];
            foreach (var item in flights)
            {
                var flightExists = await _context.Flights.OrderByDescending(f => f.CollectedDate).FirstOrDefaultAsync(f => f.Callsign == item.Callsign && f.Icao24 == item.Icao24); ;

                if (flightExists != null)
                {
                    var flightHascode = item.TimePosition + item.Longitude + item.Velocity + item.GeoAltitude + item.VerticalRate;
                    var flightExistsHascode = flightExists.TimePosition + flightExists.Longitude + flightExists.Velocity + flightExists.GeoAltitude + flightExists.VerticalRate;

                    if (flightHascode != flightExistsHascode)
                    {
                        insertdataflights.Add(item);
                    }
                }
                else
                {
                    insertdataflights.Add(item);
                }
            }
            if (insertdataflights.Count > 0)
            {
                await _context.Flights.AddRangeAsync(insertdataflights);
                await _context.SaveChangesAsync();
            }   
            return insertdataflights;
        }
        public string StartDataFetchingTimer()
        {
            // Start a timer to fetch data every 5 minutes
            if (!_timerRunning)
            {
                _timer = new Timer(ProcessData, null, 0, 300000);
                _timerRunning = true;
                return "Timer started";
            }
            else
            {
                return "Timer is already running";
            }
        }

        public string StopDataFetchingTimer()
        {
            if (_timerRunning)
            {
                // Stop the data fetching timer
                _timer?.Dispose();
                _timerRunning = false;
                return "Timer stopped";
            }
            else
            {
                return "Timer is not running";
            }
        }

        public async void ProcessData(object state)
        {
            try
            {

                var flights = await _flightAPIServices.GetFlights();
                await AddFlightsToDb(flights);
                _logger.LogInformation("Data was processed");
            }
            catch (Exception ex)
            {
                // Log and handle the exception
                _logger.LogError(ex, "Error processing data");
            }
        }

        public async Task<Timing> ProcessDataWithTimings(int intervalInSeconds, int durationInSeconds)
        {
            Stopwatch stopwatch = new();

            List<TimeSpan> apiTimes = [];
            List<TimeSpan> insertTimes = [];

            for (int i = 0; i < durationInSeconds / intervalInSeconds; i++)
            {
                // Measure API call time
                stopwatch.Restart();
                var flights = await _flightAPIServices.GetFlights();
                stopwatch.Stop();
                apiTimes.Add(stopwatch.Elapsed);

                // Measure insert time
                stopwatch.Restart();
                await AddFlightsToDb(flights);
                stopwatch.Stop();
                insertTimes.Add(stopwatch.Elapsed);

                // Wait for the next interval
                await Task.Delay(intervalInSeconds * 1000);
            }

            TimeSpan totalApiTime = apiTimes.Aggregate(TimeSpan.Zero, (acc, val) => acc + val);
            TimeSpan totalInsertTime = insertTimes.Aggregate(TimeSpan.Zero, (acc, val) => acc + val);

            return new Timing
            {
                ApiTimes = apiTimes,
                InsertTimes = insertTimes,
                TotalApiTime = totalApiTime,
                TotalInsertTime = totalInsertTime
            };
        }
        public class Timing
        {
            public List<TimeSpan> ApiTimes { get; set; }
            public List<TimeSpan> InsertTimes { get; set; }
            public TimeSpan TotalApiTime { get; set; }
            public TimeSpan TotalInsertTime { get; set; }
        }
    }
}
