using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int? OrderId { get; set; }

    public virtual ICollection<CustomerGrainCycleStatus> CustomerGrainCycleStatuses { get; set; } = new List<CustomerGrainCycleStatus>();
}
