using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities
{
    public partial class MonitorActivityBottleNeck
    {
        public int Id { get; set; }
        public int? MonitorActivityId { get; set; }
        public int? BottleneckId { get; set; }
    }
}
