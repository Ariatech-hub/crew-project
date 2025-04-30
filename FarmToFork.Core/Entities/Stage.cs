using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Stage
{
    public int Id { get; set; }

    public int SubsectorId { get; set; }

    public string Name { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Subsector Subsector { get; set; } = null!;
}
