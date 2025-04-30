using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Sale
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public decimal? Amount { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
