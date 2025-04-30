using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ProductionPlanGrainQuantity
{
    public int Id { get; set; }

    public int? ProductionPlanId { get; set; }

    public int? GrainId { get; set; }

    public decimal? ProductionLandArea { get; set; }

    public decimal? TotalProduction { get; set; }

    public decimal? HomeUse { get; set; }

    public decimal? TotalSales { get; set; }

    public decimal? TotalSalesThroughCooperative { get; set; }

    public decimal? SeedOnly { get; set; }

    public int? SyncId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
