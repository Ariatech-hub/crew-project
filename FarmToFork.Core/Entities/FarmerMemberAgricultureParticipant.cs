using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class FarmerMemberAgricultureParticipant
{
    public int Id { get; set; }

    public int ParticipantOptionId { get; set; }

    public int? SyncId { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? FarmerMemberId { get; set; }

    public int? ResearchId { get; set; }

    public DateTime? SyncDate { get; set; }

    public virtual FarmerMember? FarmerMember { get; set; }

    public virtual ParticipationOption ParticipantOption { get; set; } = null!;

    public virtual Research? Research { get; set; }
}
