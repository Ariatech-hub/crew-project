using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int GrainCycleId { get; set; }
        public int GrainId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual Grain Grain { get; set; } = null!;
        public virtual GrainCycle GrainCycle { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
    }
}
