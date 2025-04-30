using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Farmer
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public int? GenderId { get; set; }

    public int? EthnicityId { get; set; }

    public int? MaritalStatusId { get; set; }

    public string? DateOfBirth { get; set; }

    public int ProvinceId { get; set; }

    public int DistrictId { get; set; }

    public int PalikaId { get; set; }

    public int Ward { get; set; }

    public string? Tole { get; set; }

    public string? MobileNumber { get; set; }

    public string? PhotoName { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string FullName { get; set; } = null!;

    public int? CommunityId { get; set; }

    public string? HusbandOrFatherName { get; set; }

    public int? OccupationId { get; set; }

    public int? EducationLevelId { get; set; }

    public int? Age { get; set; }

    public bool? OwnSmartPhone { get; set; }

    public string? GrandFatherOrFatherInLawName { get; set; }

    public decimal? HouseValuationAmount { get; set; }

    public decimal? BusinessValuationAmount { get; set; }

    public decimal? JewelryValuationAmount { get; set; }

    public string? NomineeName { get; set; }

    public int? NomineeRelationshipId { get; set; }

    public DateTime? SyncDate { get; set; }

    public decimal? CattleValuationAmount { get; set; }

    public decimal? BankBalanceValuationAmount { get; set; }

    public int? SyncId { get; set; }

    public decimal? OtherValuationAmount { get; set; }

    public string Identifier { get; set; } = null!;

    public string? DeviceId { get; set; }

    public bool IsActive { get; set; }

    public string? CitizenshipNumber { get; set; }

    public int? FarmerMembershipNumber { get; set; }

    public string? HusbandOrWifeName { get; set; }

    public int? SerialNumber { get; set; }

    public string? Code { get; set; }

    public bool IsNonMember { get; set; }

    public virtual Community? Community { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual EducationLevel? EducationLevel { get; set; }

    public virtual Ethnicity? Ethnicity { get; set; }

    public virtual ICollection<FarmerMember> FarmerMembers { get; set; } = new List<FarmerMember>();

    public virtual Gender? Gender { get; set; }

    public virtual ICollection<LastSeasonProduction> LastSeasonProductions { get; set; } = new List<LastSeasonProduction>();

    public virtual MaritalStatus? MaritalStatus { get; set; }

    public virtual Relation? NomineeRelationship { get; set; }

    public virtual Occupation? Occupation { get; set; }

    public virtual Palika Palika { get; set; } = null!;

    public virtual ICollection<ProductionPlan> ProductionPlans { get; set; } = new List<ProductionPlan>();

    public virtual Province Province { get; set; } = null!;

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    public virtual ICollection<Research> Researches { get; set; } = new List<Research>();
}
