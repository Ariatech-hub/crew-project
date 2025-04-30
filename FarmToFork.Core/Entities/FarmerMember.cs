using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class FarmerMember
{
    public int Id { get; set; }

    public int FarmerId { get; set; }

    public string Name { get; set; } = null!;

    public int? RelationId { get; set; }

    public int? OccupationId { get; set; }

    public int? EducationLevelId { get; set; }

    public int? GenderId { get; set; }

    public int? Age { get; set; }

    public bool OwnSmartPhone { get; set; }

    public string? Remarks { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public DateTime SyncDate { get; set; }

    public int? SyncId { get; set; }

    public int? LocationId { get; set; }

    public virtual EducationLevel? EducationLevel { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual ICollection<FarmerMemberAgricultureParticipant> FarmerMemberAgricultureParticipants { get; set; } = new List<FarmerMemberAgricultureParticipant>();

    public virtual ICollection<FarmerMemberProperty> FarmerMemberProperties { get; set; } = new List<FarmerMemberProperty>();

    public virtual ICollection<FarmerMemberTransaction> FarmerMemberTransactions { get; set; } = new List<FarmerMemberTransaction>();

    public virtual Gender? Gender { get; set; }

    public virtual Location? Location { get; set; }

    public virtual Occupation? Occupation { get; set; }

    public virtual Relation? Relation { get; set; }
}
