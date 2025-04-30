using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class LastSeasonProduction
{
    public int Id { get; set; }

    public int FarmerId { get; set; }

    public int GrainId { get; set; }

    public decimal? ProductionLandArea { get; set; }

    public decimal? TotalProduction { get; set; }

    public decimal? TotalSales { get; set; }

    public decimal? TotalSalesAmount { get; set; }

    public int? SyncId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual Grain Grain { get; set; } = null!;
}
