using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities
{
    public partial class OrderAmount
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual Order Order { get; set; } = null!;
    }
}
