using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Subsector
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Code { get; set; }

    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();
}
