using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderAmounts = new HashSet<OrderAmount>();
            OrderDetails = new HashSet<OrderDetail>();
            
        }

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Description { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderAmount> OrderAmounts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
       
    }
}
