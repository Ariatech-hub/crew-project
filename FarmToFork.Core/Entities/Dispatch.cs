using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Dispatch
{
    public int Id { get; set; }

    public int CustomerGrainCycleId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Quantity { get; set; }

    public decimal Total { get; set; }

    public decimal ObtainedBalance { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual CustomerGrainCycle CustomerGrainCycle { get; set; } = null!;
}
