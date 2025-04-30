using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class District
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NepaliName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public string? Code { get; set; }

    public bool IsActive { get; set; }

    public int ProvinceId { get; set; }

    public virtual ICollection<FarmerMemberProperty> FarmerMemberProperties { get; set; } = new List<FarmerMemberProperty>();

    public virtual ICollection<Farmer> Farmers { get; set; } = new List<Farmer>();

    public virtual ICollection<Palika> Palikas { get; set; } = new List<Palika>();

    public virtual Province Province { get; set; } = null!;
}
