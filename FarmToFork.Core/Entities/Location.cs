using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Location
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public string? Code { get; set; }

    public int? OrderId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<FarmerMember> FarmerMembers { get; set; } = new List<FarmerMember>();
}
