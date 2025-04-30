using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class FarmerMemberProperty
{
    public int Id { get; set; }

    public int FarmerMemberId { get; set; }

    public int DistrictId { get; set; }

    public int PalikaId { get; set; }

    public int ProvinceId { get; set; }

    public int? Ward { get; set; }

    public string? KittaNumber { get; set; }

    public decimal? LandArea { get; set; }

    public decimal? LandValuation { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? SyncDate { get; set; }

    public string? MemberIdentifier { get; set; }

    public string? Tole { get; set; }

    public int? SyncId { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual FarmerMember FarmerMember { get; set; } = null!;

    public virtual Palika Palika { get; set; } = null!;

    public virtual Province Province { get; set; } = null!;
}
