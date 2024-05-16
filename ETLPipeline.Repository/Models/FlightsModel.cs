using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLPipeline.Repository.Models
{
    public class FlightsModel
    {
        public int time { get; set; }
        public List<List<object>> states { get; set; }
    }
}
