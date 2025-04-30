using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Palika
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public string? Code { get; set; }

    public bool IsActive { get; set; }

    public int DistrictId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual ICollection<FarmerMemberProperty> FarmerMemberProperties { get; set; } = new List<FarmerMemberProperty>();

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();
}
