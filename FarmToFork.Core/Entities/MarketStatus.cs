using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class MarketStatus
{
    public int Id { get; set; }

    public int ResearchId { get; set; }

    public int GrainId { get; set; }

    public decimal? TotalSalesNumber { get; set; }

    public int? MonthId { get; set; }

    public decimal? SaleRate { get; set; }

    public bool? IsFromHome { get; set; }

    public int? SyncId { get; set; }

    public int? Week { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Grain Grain { get; set; } = null!;

    public virtual Month? Month { get; set; }

    public virtual Research Research { get; set; } = null!;
}
