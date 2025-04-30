using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ProductionPlanMarketStatus
{
    public int Id { get; set; }

    public int? ProductionPlanId { get; set; }

    public int? GrainTypeId { get; set; }

    public decimal? TotalSalesNumber { get; set; }

    public int? MonthId { get; set; }

    public decimal? SaleRate { get; set; }
}
