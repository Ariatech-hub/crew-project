using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Year
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public DateOnly StartDateAd { get; set; }

    public DateOnly EndDateAd { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<GrainCycle> GrainCycles { get; set; } = new List<GrainCycle>();
}
