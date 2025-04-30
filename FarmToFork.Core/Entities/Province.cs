using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Province
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NepaliName { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<FarmerMemberProperty> FarmerMemberProperties { get; set; } = new List<FarmerMemberProperty>();

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}
