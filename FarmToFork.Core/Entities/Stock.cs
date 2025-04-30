using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Stock
{
    public int Id { get; set; }

    public int GrainCycleId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public decimal RemainingQuantity { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual GrainCycle GrainCycle { get; set; } = null!;
}
