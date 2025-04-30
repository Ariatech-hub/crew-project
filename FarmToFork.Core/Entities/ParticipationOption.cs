using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ParticipationOption
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Code { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public int? OrderNumber { get; set; }

    public virtual ICollection<FarmerMemberAgricultureParticipant> FarmerMemberAgricultureParticipants { get; set; } = new List<FarmerMemberAgricultureParticipant>();
}
