using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class CustomerGrainCycleStatus
{
    public int Id { get; set; }

    public int CustomerGrainCycleId { get; set; }

    public int StatusId { get; set; }

    public string? Remarks { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual CustomerGrainCycle CustomerGrainCycle { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
