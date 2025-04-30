using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Gender
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public string? Code { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<FarmerMember> FarmerMembers { get; set; } = new List<FarmerMember>();

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}
