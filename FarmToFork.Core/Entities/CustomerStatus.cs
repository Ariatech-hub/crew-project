using System;
using System.Collections.Generic;

namespace FarmToFork.Core.Entities
{
    public partial class CustomerStatus
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StatusId { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
    }
}
