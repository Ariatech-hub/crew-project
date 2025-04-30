using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Grain
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Code { get; set; }

    public int? OrderNumber { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<GrainCycle> GrainCycles { get; set; } = new List<GrainCycle>();

    public virtual ICollection<LastSeasonProduction> LastSeasonProductions { get; set; } = new List<LastSeasonProduction>();

    public virtual ICollection<MarketStatus> MarketStatuses { get; set; } = new List<MarketStatus>();

    public virtual ICollection<ProductionPlan> ProductionPlans { get; set; } = new List<ProductionPlan>();
}
