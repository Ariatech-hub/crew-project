using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class SaleDetail
{
    public int Id { get; set; }

    public int? SaleId { get; set; }

    public int? GrainCycleId { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Amount { get; set; }

    public virtual GrainCycle? GrainCycle { get; set; }

    public virtual Sale? Sale { get; set; }
}
