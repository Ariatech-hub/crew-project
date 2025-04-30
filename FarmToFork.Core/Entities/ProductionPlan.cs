using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ProductionPlan
{
    public int Id { get; set; }

    public int FarmerId { get; set; }

    public int GrainId { get; set; }

    public int GrainCycleId { get; set; }

    public decimal? LandArea { get; set; }

    public decimal? TotalProduction { get; set; }

    public decimal? HomeUse { get; set; }

    public decimal? TotalSales { get; set; }

    public decimal? TotalSalesThroughCooperative { get; set; }

    public decimal? SeedOnly { get; set; }

    public DateTime? SyncDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public decimal? ActualSalesThroughCooperative { get; set; }

    public DateTime? SaleDate { get; set; }

    public string? SaleBy { get; set; }

    public string? Remarks { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual Grain Grain { get; set; } = null!;

    public virtual GrainCycle GrainCycle { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
