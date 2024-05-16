using ETLPipeline.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ETLPipeline.Services.Services.FlightsServices;

namespace ETLPipeline.Services.Interface
{
    public interface IFlightsServices
    {
        Task<List<Flights>> AddFlightsToDb(IEnumerable<Flights> flights);
        string StartDataFetchingTimer();
        string StopDataFetchingTimer();
        void ProcessData(object state);
        Task<Timing> ProcessDataWithTimings(int intervalInSeconds, int durationInSeconds);

    }
}
