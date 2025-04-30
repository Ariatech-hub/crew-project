using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class CustomerGrainCycle
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int GrainCycleId { get; set; }

    public bool IsDispatched { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<CustomerGrainCycleStatus> CustomerGrainCycleStatuses { get; set; } = new List<CustomerGrainCycleStatus>();

    public virtual ICollection<Dispatch> Dispatches { get; set; } = new List<Dispatch>();

    public virtual GrainCycle GrainCycle { get; set; } = null!;
}
