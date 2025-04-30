using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class GrainCycle
{
    public int Id { get; set; }

    public int FromMonthId { get; set; }

    public int ToMonthId { get; set; }

    public int OrderNo { get; set; }

    public int YearId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public string Name { get; set; } = null!;

    public string NepaliName { get; set; } = null!;

    public bool IsActive { get; set; }

    public int GrainId { get; set; }

    public virtual ICollection<CustomerGrainCycle> CustomerGrainCycles { get; set; } = new List<CustomerGrainCycle>();

    public virtual Month FromMonth { get; set; } = null!;

    public virtual Grain Grain { get; set; } = null!;

    public virtual ICollection<ProductionPlan> ProductionPlans { get; set; } = new List<ProductionPlan>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual Month ToMonth { get; set; } = null!;

    public virtual Year Year { get; set; } = null!;
}
