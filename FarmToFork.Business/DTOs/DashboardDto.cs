using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class DashboardDto
    {
      
        public int FarmerCount { get; set; }
        public int CustomerCount { get; set; }
        public int GrainCount { get; set; }
        public int GrainCycleCount { get; set; }
    }
}
