using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Production
{
    public int Id { get; set; }

    public int FarmerId { get; set; }

    public int SubsectorId { get; set; }

    public int SubsectorCycleId { get; set; }

    public decimal? LandArea { get; set; }

    public string? Longitude { get; set; }

    public string? Latitude { get; set; }
}
