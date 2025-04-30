using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class ReceiptDetail
{
    public int Id { get; set; }

    public int ReceiptId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Quantity { get; set; }

    public decimal Total { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public int ProductionPlanId { get; set; }

    public virtual ProductionPlan ProductionPlan { get; set; } = null!;

    public virtual Receipt Receipt { get; set; } = null!;
}
