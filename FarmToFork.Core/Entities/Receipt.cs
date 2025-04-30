using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities;

public partial class Receipt
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public int FarmerId { get; set; }

    public int ReceiptNumber { get; set; }

    public virtual Farmer Farmer { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}
