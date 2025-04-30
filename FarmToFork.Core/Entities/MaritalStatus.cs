using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class MaritalStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NepaliName { get; set; }

    public bool? IsActive { get; set; }

    public int? OrderId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}
