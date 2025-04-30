using System;
using System.Collections.Generic;
using FarmToFork.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmToFork.Core;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessLevel> AccessLevels { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AuditLogin> AuditLogins { get; set; }

    public virtual DbSet<CardImage> CardImages { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerContactDetail> CustomerContactDetails { get; set; }

    public virtual DbSet<CustomerGrainCycle> CustomerGrainCycles { get; set; }

    public virtual DbSet<CustomerGrainCycleStatus> CustomerGrainCycleStatuses { get; set; }

    public virtual DbSet<Dispatch> Dispatches { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<EducationLevel> EducationLevels { get; set; }

    public virtual DbSet<Enumerator> Enumerators { get; set; }

    public virtual DbSet<Ethnicity> Ethnicities { get; set; }

    public virtual DbSet<ExcelDatum> ExcelData { get; set; }

    public virtual DbSet<Farmer> Farmers { get; set; }

    public virtual DbSet<FarmerMember> FarmerMembers { get; set; }

    public virtual DbSet<FarmerMemberAgricultureParticipant> FarmerMemberAgricultureParticipants { get; set; }

    public virtual DbSet<FarmerMemberProperty> FarmerMemberProperties { get; set; }

    public virtual DbSet<FarmerMemberTransaction> FarmerMemberTransactions { get; set; }

    public virtual DbSet<FarmerSubsector> FarmerSubsectors { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Grain> Grains { get; set; }

    public virtual DbSet<GrainCycle> GrainCycles { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<IncomeSource> IncomeSources { get; set; }

    public virtual DbSet<InternetType> InternetTypes { get; set; }

    public virtual DbSet<InternetUse> InternetUses { get; set; }

    public virtual DbSet<LabourDivision> LabourDivisions { get; set; }

    public virtual DbSet<LastSeasonProduction> LastSeasonProductions { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }

    public virtual DbSet<MarketStatus> MarketStatuses { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuAccessLevel> MenuAccessLevels { get; set; }

    public virtual DbSet<MenuAspNetUser> MenuAspNetUsers { get; set; }

    public virtual DbSet<Month> Months { get; set; }

    public virtual DbSet<Obstacle> Obstacles { get; set; }

    public virtual DbSet<Occupation> Occupations { get; set; }

    public virtual DbSet<Palika> Palikas { get; set; }

    public virtual DbSet<ParticipationOption> ParticipationOptions { get; set; }

    public virtual DbSet<Production> Productions { get; set; }

    public virtual DbSet<ProductionEvent> ProductionEvents { get; set; }

    public virtual DbSet<ProductionPlan> ProductionPlans { get; set; }

    public virtual DbSet<ProductionPlanGrainQuantity> ProductionPlanGrainQuantities { get; set; }

    public virtual DbSet<ProductionPlanInternetUse> ProductionPlanInternetUses { get; set; }

    public virtual DbSet<ProductionPlanMarketStatus> ProductionPlanMarketStatuses { get; set; }

    public virtual DbSet<ProductionPlanObstacle> ProductionPlanObstacles { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Relation> Relations { get; set; }

    public virtual DbSet<Research> Researches { get; set; }

    public virtual DbSet<ResearchInternetUse> ResearchInternetUses { get; set; }

    public virtual DbSet<ResearchObstacle> ResearchObstacles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Subsector> Subsectors { get; set; }

    public virtual DbSet<Table1> Table1s { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Year> Years { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MenuAccessRole");

            entity.ToTable("AccessLevel");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.RoleId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Id).HasMaxLength(450);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.LoginProvider).HasMaxLength(450);
            entity.Property(e => e.ProviderKey).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.RoleId).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.LoginProvider).HasMaxLength(450);
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<AuditLogin>(entity =>
        {
            entity.ToTable("AuditLogin");

            entity.Property(e => e.LogInTime).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<CardImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CardImag__3214EC07AC64111F");

            entity.ToTable("CardImage");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileName).HasMaxLength(512);
            entity.Property(e => e.ModifiedBy).HasMaxLength(512);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.ToTable("Community");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(512);
            entity.Property(e => e.ModifiedBy).HasMaxLength(512);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.PhoneNumber).HasMaxLength(16);
        });

        modelBuilder.Entity<CustomerContactDetail>(entity =>
        {
            entity.ToTable("CustomerContactDetail");

            entity.Property(e => e.Email).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.PhoneNumber).HasMaxLength(16);
        });

        modelBuilder.Entity<CustomerGrainCycle>(entity =>
        {
            entity.ToTable("CustomerGrainCycle");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerGrainCycles)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerGrainCycle_Customer");

            entity.HasOne(d => d.GrainCycle).WithMany(p => p.CustomerGrainCycles)
                .HasForeignKey(d => d.GrainCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerGrainCycle_GrainCycle");
        });

        modelBuilder.Entity<CustomerGrainCycleStatus>(entity =>
        {
            entity.ToTable("CustomerGrainCycleStatus");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Remarks).HasMaxLength(512);

            entity.HasOne(d => d.CustomerGrainCycle).WithMany(p => p.CustomerGrainCycleStatuses)
                .HasForeignKey(d => d.CustomerGrainCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerGrainCycleStatus_CustomerGrainCycle");

            entity.HasOne(d => d.Status).WithMany(p => p.CustomerGrainCycleStatuses)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomerGrainCycleStatus_Status");
        });

        modelBuilder.Entity<Dispatch>(entity =>
        {
            entity.ToTable("Dispatch");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ObtainedBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.CustomerGrainCycle).WithMany(p => p.Dispatches)
                .HasForeignKey(d => d.CustomerGrainCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dispatch_CustomerGrainCycle");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_District_Province");
        });

        modelBuilder.Entity<EducationLevel>(entity =>
        {
            entity.ToTable("EducationLevel");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Enumerator>(entity =>
        {
            entity.ToTable("Enumerator");

            entity.Property(e => e.DeviceId).HasMaxLength(450);
            entity.Property(e => e.Location).HasMaxLength(512);
            entity.Property(e => e.Username).HasMaxLength(512);
        });

        modelBuilder.Entity<Ethnicity>(entity =>
        {
            entity.ToTable("Ethnicity");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<ExcelDatum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.BOG)
                .HasMaxLength(53)
                .HasColumnName(";b:o g=");
            entity.Property(e => e.GrainCycle).HasColumnName("Grain Cycle");
            entity.Property(e => e.Rate).HasColumnName("rate");
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.ToTable("Farmer");

            entity.HasIndex(e => e.Identifier, "UC_Farmer").IsUnique();

            entity.Property(e => e.BankBalanceValuationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BusinessValuationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CattleValuationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CitizenshipNumber).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasMaxLength(32);
            entity.Property(e => e.DeviceId).HasMaxLength(450);
            entity.Property(e => e.FirstName).HasMaxLength(512);
            entity.Property(e => e.FullName).HasMaxLength(450);
            entity.Property(e => e.GrandFatherOrFatherInLawName).HasMaxLength(450);
            entity.Property(e => e.HouseValuationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HusbandOrFatherName).HasMaxLength(450);
            entity.Property(e => e.HusbandOrWifeName).HasMaxLength(450);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.JewelryValuationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LastName).HasMaxLength(512);
            entity.Property(e => e.Latitude).HasMaxLength(512);
            entity.Property(e => e.Longitude).HasMaxLength(512);
            entity.Property(e => e.MiddleName).HasMaxLength(512);
            entity.Property(e => e.MobileNumber).HasMaxLength(10);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NomineeName).HasMaxLength(450);
            entity.Property(e => e.OtherValuationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PhotoName)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Tole).HasMaxLength(512);

            entity.HasOne(d => d.Community).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK_Farmer_Community");

            entity.HasOne(d => d.District).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farmer_District");

            entity.HasOne(d => d.EducationLevel).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.EducationLevelId)
                .HasConstraintName("FK_Farmer_EducationLevel");

            entity.HasOne(d => d.Ethnicity).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.EthnicityId)
                .HasConstraintName("FK_Farmer_Ethnicity");

            entity.HasOne(d => d.Gender).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_Farmer_Gender");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("FK_Farmer_MaritalStatus");

            entity.HasOne(d => d.NomineeRelationship).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.NomineeRelationshipId)
                .HasConstraintName("FK_Farmer_Relation");

            entity.HasOne(d => d.Occupation).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.OccupationId)
                .HasConstraintName("FK_Farmer_Occupation");

            entity.HasOne(d => d.Palika).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.PalikaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farmer_Palika");

            entity.HasOne(d => d.Province).WithMany(p => p.Farmers)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Farmer_Province");
        });

        modelBuilder.Entity<FarmerMember>(entity =>
        {
            entity.ToTable("FarmerMember");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNumber).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.Remarks).HasMaxLength(1024);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");

            entity.HasOne(d => d.EducationLevel).WithMany(p => p.FarmerMembers)
                .HasForeignKey(d => d.EducationLevelId)
                .HasConstraintName("FK_FarmerMember_EducationLevel");

            entity.HasOne(d => d.Farmer).WithMany(p => p.FarmerMembers)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerMember_Farmer");

            entity.HasOne(d => d.Gender).WithMany(p => p.FarmerMembers)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK_FarmerMember_Gender");

            entity.HasOne(d => d.Location).WithMany(p => p.FarmerMembers)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK_FarmerMember_Location");

            entity.HasOne(d => d.Occupation).WithMany(p => p.FarmerMembers)
                .HasForeignKey(d => d.OccupationId)
                .HasConstraintName("FK_FarmerMember_Occupation");

            entity.HasOne(d => d.Relation).WithMany(p => p.FarmerMembers)
                .HasForeignKey(d => d.RelationId)
                .HasConstraintName("FK_FarmerMember_Relation");
        });

        modelBuilder.Entity<FarmerMemberAgricultureParticipant>(entity =>
        {
            entity.ToTable("FarmerMemberAgricultureParticipant");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SyncDate).HasColumnType("datetime");

            entity.HasOne(d => d.FarmerMember).WithMany(p => p.FarmerMemberAgricultureParticipants)
                .HasForeignKey(d => d.FarmerMemberId)
                .HasConstraintName("FK_FarmerMemberAgricultureParticipant_FarmerMember");

            entity.HasOne(d => d.ParticipantOption).WithMany(p => p.FarmerMemberAgricultureParticipants)
                .HasForeignKey(d => d.ParticipantOptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerMemberAgricultureParticipant_ParticipationOption");

            entity.HasOne(d => d.Research).WithMany(p => p.FarmerMemberAgricultureParticipants)
                .HasForeignKey(d => d.ResearchId)
                .HasConstraintName("FK_FarmerMemberAgricultureParticipant_Research");
        });

        modelBuilder.Entity<FarmerMemberProperty>(entity =>
        {
            entity.ToTable("FarmerMemberProperty");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.KittaNumber).HasMaxLength(64);
            entity.Property(e => e.LandArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LandValuation).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MemberIdentifier).HasMaxLength(450);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Tole).HasMaxLength(512);

            entity.HasOne(d => d.District).WithMany(p => p.FarmerMemberProperties)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerMemberProperty_District");

            entity.HasOne(d => d.FarmerMember).WithMany(p => p.FarmerMemberProperties)
                .HasForeignKey(d => d.FarmerMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerMemberProperty_FarmerMember");

            entity.HasOne(d => d.Palika).WithMany(p => p.FarmerMemberProperties)
                .HasForeignKey(d => d.PalikaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerMemberProperty_Palika");

            entity.HasOne(d => d.Province).WithMany(p => p.FarmerMemberProperties)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FarmerMemberProperty_Province");
        });

        modelBuilder.Entity<FarmerMemberTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FarmerMemberTransaction1");

            entity.ToTable("FarmerMemberTransaction");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ExpenseAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ExpenseSource).HasMaxLength(1024);
            entity.Property(e => e.IncomeAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IncomeSource).HasMaxLength(1024);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");

            entity.HasOne(d => d.FarmerMember).WithMany(p => p.FarmerMemberTransactions)
                .HasForeignKey(d => d.FarmerMemberId)
                .HasConstraintName("FK_FarmerMemberTransaction_FarmerMember");
        });

        modelBuilder.Entity<FarmerSubsector>(entity =>
        {
            entity.ToTable("FarmerSubsector");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Grain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_GrainType");

            entity.ToTable("Grain");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<GrainCycle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SubsectorCycle");

            entity.ToTable("GrainCycle");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(512);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.NepaliName).HasMaxLength(450);

            entity.HasOne(d => d.FromMonth).WithMany(p => p.GrainCycleFromMonths)
                .HasForeignKey(d => d.FromMonthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrainCycle_Month1");

            entity.HasOne(d => d.Grain).WithMany(p => p.GrainCycles)
                .HasForeignKey(d => d.GrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrainCycle_Grain");

            entity.HasOne(d => d.ToMonth).WithMany(p => p.GrainCycleToMonths)
                .HasForeignKey(d => d.ToMonthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrainCycle_Month");

            entity.HasOne(d => d.Year).WithMany(p => p.GrainCycles)
                .HasForeignKey(d => d.YearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GrainCycle_Year");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<IncomeSource>(entity =>
        {
            entity.ToTable("IncomeSource");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<InternetType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_TypeOfInterent");

            entity.ToTable("InternetType");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<InternetUse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_internetUse");

            entity.ToTable("InternetUse");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<LabourDivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_LabourDivision ");

            entity.ToTable("LabourDivision");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<LastSeasonProduction>(entity =>
        {
            entity.ToTable("LastSeasonProduction");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductionLandArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalProduction).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSalesAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Farmer).WithMany(p => p.LastSeasonProductions)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LastSeasonProduction_Farmer");

            entity.HasOne(d => d.Grain).WithMany(p => p.LastSeasonProductions)
                .HasForeignKey(d => d.GrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LastSeasonProduction_Grain");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<MaritalStatus>(entity =>
        {
            entity.ToTable("MaritalStatus");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(512);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(512);
        });

        modelBuilder.Entity<MarketStatus>(entity =>
        {
            entity.ToTable("MarketStatus");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.SaleRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSalesNumber).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Grain).WithMany(p => p.MarketStatuses)
                .HasForeignKey(d => d.GrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarketStatus_MarketStatus");

            entity.HasOne(d => d.Month).WithMany(p => p.MarketStatuses)
                .HasForeignKey(d => d.MonthId)
                .HasConstraintName("FK_MarketStatus_Month");

            entity.HasOne(d => d.Research).WithMany(p => p.MarketStatuses)
                .HasForeignKey(d => d.ResearchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MarketStatus_Research");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Icon)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(1024);
            entity.Property(e => e.Url)
                .HasMaxLength(1024)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuAccessLevel>(entity =>
        {
            entity.ToTable("MenuAccessLevel");

            entity.Property(e => e.AdminName)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MenuAspNetUser>(entity =>
        {
            entity.ToTable("MenuAspNetUser");

            entity.Property(e => e.AspNetUserId)
                .HasMaxLength(1024)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Month>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Monty");

            entity.ToTable("Month");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(512);
        });

        modelBuilder.Entity<Obstacle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Obstacle ");

            entity.ToTable("Obstacle");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.ToTable("Occupation");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Palika>(entity =>
        {
            entity.ToTable("Palika");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);

            entity.HasOne(d => d.District).WithMany(p => p.Palikas)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Palika_District");
        });

        modelBuilder.Entity<ParticipationOption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_productionEngagements");

            entity.ToTable("ParticipationOption");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Production>(entity =>
        {
            entity.ToTable("Production");

            entity.Property(e => e.LandArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Latitude)
                .HasMaxLength(512)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(512)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductionEvent>(entity =>
        {
            entity.ToTable("ProductionEvent");

            entity.Property(e => e.PhotoName).HasMaxLength(512);
        });

        modelBuilder.Entity<ProductionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductionPlan_1");

            entity.ToTable("ProductionPlan");

            entity.Property(e => e.ActualSalesThroughCooperative).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HomeUse).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LandArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Remarks).HasMaxLength(450);
            entity.Property(e => e.SaleBy).HasMaxLength(450);
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.SeedOnly).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.TotalProduction).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSalesThroughCooperative).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Farmer).WithMany(p => p.ProductionPlans)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionPlan_Farmer");

            entity.HasOne(d => d.GrainCycle).WithMany(p => p.ProductionPlans)
                .HasForeignKey(d => d.GrainCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionPlan_GrainCycle");

            entity.HasOne(d => d.Grain).WithMany(p => p.ProductionPlans)
                .HasForeignKey(d => d.GrainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionPlan_Grain");
        });

        modelBuilder.Entity<ProductionPlanGrainQuantity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ThisSeasonProductionPlan");

            entity.ToTable("ProductionPlanGrainQuantity");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.HomeUse).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ProductionLandArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeedOnly).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalProduction).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSales).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSalesThroughCooperative).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductionPlanInternetUse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductionPlanInternetType");

            entity.ToTable("ProductionPlanInternetUse");
        });

        modelBuilder.Entity<ProductionPlanMarketStatus>(entity =>
        {
            entity.ToTable("ProductionPlanMarketStatus");

            entity.Property(e => e.SaleRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalSalesNumber).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ProductionPlanObstacle>(entity =>
        {
            entity.ToTable("ProductionPlanObstacle");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(512);
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.ToTable("Receipt");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Receipts)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Receipt_Farmer");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.ToTable("ReceiptDetail");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.ProductionPlan).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ProductionPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptDetail_ProductionPlan");

            entity.HasOne(d => d.Receipt).WithMany(p => p.ReceiptDetails)
                .HasForeignKey(d => d.ReceiptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptDetail_Receipt");
        });

        modelBuilder.Entity<Relation>(entity =>
        {
            entity.ToTable("Relation");

            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(512);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        modelBuilder.Entity<Research>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Research_1");

            entity.ToTable("Research");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LandAreaForBeanProduction).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.TotalLandArea).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Farmer).WithMany(p => p.Researches)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Research_Farmer");

            entity.HasOne(d => d.HarvestingAndStoringAfterBeanProductionLabourDivision).WithMany(p => p.ResearchHarvestingAndStoringAfterBeanProductionLabourDivisions)
                .HasForeignKey(d => d.HarvestingAndStoringAfterBeanProductionLabourDivisionId)
                .HasConstraintName("FK_Research_LabourDivision3");

            entity.HasOne(d => d.LandOwnershipForBeanProductionLabourDivision).WithMany(p => p.ResearchLandOwnershipForBeanProductionLabourDivisions)
                .HasForeignKey(d => d.LandOwnershipForBeanProductionLabourDivisionId)
                .HasConstraintName("FK_Research_LabourDivision5");

            entity.HasOne(d => d.MakingLandReadyForBeanProductionLabourDivision).WithMany(p => p.ResearchMakingLandReadyForBeanProductionLabourDivisions)
                .HasForeignKey(d => d.MakingLandReadyForBeanProductionLabourDivisionId)
                .HasConstraintName("FK_Research_LabourDivision");

            entity.HasOne(d => d.PlantSeedForBeanProductionLabourDivision).WithMany(p => p.ResearchPlantSeedForBeanProductionLabourDivisions)
                .HasForeignKey(d => d.PlantSeedForBeanProductionLabourDivisionId)
                .HasConstraintName("FK_Research_LabourDivision1");

            entity.HasOne(d => d.SellingAndMarketingAfterBeanProductionLabourDivision).WithMany(p => p.ResearchSellingAndMarketingAfterBeanProductionLabourDivisions)
                .HasForeignKey(d => d.SellingAndMarketingAfterBeanProductionLabourDivisionId)
                .HasConstraintName("FK_Research_LabourDivision4");

            entity.HasOne(d => d.TakingCareForBeanProductionLabourDivision).WithMany(p => p.ResearchTakingCareForBeanProductionLabourDivisions)
                .HasForeignKey(d => d.TakingCareForBeanProductionLabourDivisionId)
                .HasConstraintName("FK_Research_LabourDivision6");

            entity.HasOne(d => d.TypeOfInternet).WithMany(p => p.Researches)
                .HasForeignKey(d => d.TypeOfInternetId)
                .HasConstraintName("FK_Research_InternetType");
        });

        modelBuilder.Entity<ResearchInternetUse>(entity =>
        {
            entity.ToTable("ResearchInternetUse");

            entity.HasOne(d => d.Research).WithMany(p => p.ResearchInternetUses)
                .HasForeignKey(d => d.ResearchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResearchInternetUse_Research");

            entity.HasOne(d => d.UseOfInternet).WithMany(p => p.ResearchInternetUses)
                .HasForeignKey(d => d.UseOfInternetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResearchInternetUse_InternetUse");
        });

        modelBuilder.Entity<ResearchObstacle>(entity =>
        {
            entity.ToTable("ResearchObstacle");

            entity.HasOne(d => d.Obstacle).WithMany(p => p.ResearchObstacles)
                .HasForeignKey(d => d.ObstacleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResearchObstacle_Obstacle");

            entity.HasOne(d => d.Research).WithMany(p => p.ResearchObstacles)
                .HasForeignKey(d => d.ResearchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ResearchObstacle_Research");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sale");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.ToTable("SaleDetail");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.GrainCycle).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.GrainCycleId)
                .HasConstraintName("FK_SaleDetail_GrainCycle");

            entity.HasOne(d => d.Sale).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK_SaleDetail_Sale");
        });

        modelBuilder.Entity<Stage>(entity =>
        {
            entity.ToTable("Stage");

            entity.Property(e => e.Name).HasMaxLength(512);

            entity.HasOne(d => d.Subsector).WithMany(p => p.Stages)
                .HasForeignKey(d => d.SubsectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stage_Subsector");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.Code).HasMaxLength(512);
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.ToTable("Stock");

            entity.Property(e => e.CreatedBy).HasMaxLength(450);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(450);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RemainingQuantity).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.GrainCycle).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.GrainCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stock_GrainCycle");
        });

        modelBuilder.Entity<Subsector>(entity =>
        {
            entity.ToTable("Subsector");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(512);
        });

        modelBuilder.Entity<Table1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Table_1");

            entity.Property(e => e.Asd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("asd");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.DeviceId, "UC_User").IsUnique();

            entity.Property(e => e.Location)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(450);
        });

        modelBuilder.Entity<Year>(entity =>
        {
            entity.ToTable("Year");

            entity.Property(e => e.CreatedBy).HasMaxLength(512);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(512);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(450);
            entity.Property(e => e.NepaliName).HasMaxLength(450);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
