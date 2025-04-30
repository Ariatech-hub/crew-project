using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Month
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public virtual ICollection<GrainCycle> GrainCycleFromMonths { get; set; } = new List<GrainCycle>();

    public virtual ICollection<GrainCycle> GrainCycleToMonths { get; set; } = new List<GrainCycle>();

    public virtual ICollection<MarketStatus> MarketStatuses { get; set; } = new List<MarketStatus>();
}
