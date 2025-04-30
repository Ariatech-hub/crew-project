using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class FarmerMemberTransaction
{
    public int Id { get; set; }

    public int? FarmerMemberId { get; set; }

    public string? IncomeSource { get; set; }

    public decimal? IncomeAmount { get; set; }

    public string? ExpenseSource { get; set; }

    public decimal? ExpenseAmount { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? SyncDate { get; set; }

    public int? SyncId { get; set; }

    public virtual FarmerMember? FarmerMember { get; set; }
}
