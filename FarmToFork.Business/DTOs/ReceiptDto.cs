using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class ReceiptDto
    {
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public int ProductionPlanId { get; set; }
        public int ProductionPlanUnitPrice { get; set; } 
        public int ProductionPlanLandArea { get; set; } 
        public int ProductionPlanTotalProduction { get; set; } 
        public int ProductionPlanActualSalesThroughCooperative { get; set; }
        public string ProductionPlanGrainName { get; set; } = string.Empty;
        public string ProductionPlanGrainNepaliName { get; set; } = string.Empty;
        public string ProductionPlanGrainCycleName { get; set; } = string.Empty;
        public string ProductionPlanGrainCycleNepaliName { get; set; } = string.Empty;
    }

    // TODDO: Code Review
    public class BuyerReceiptDto
    {
        public int Id { get; set; }
        public string CustomerGrainCycleCustomerName { get; set; } = null!;
        public string CustomerGrainCycleCustomerEmail { get; set; } = null!;
        public string CustomerGrainCycleCustomerPhoneNumber { get; set; } = null!;
        public string CustomerGrainCycleGrainCycleName { get; set; } = null!;
        public string CustomerGrainCycleGrainCycleGrainName { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedDates => CreatedDate.ToString("yyyy-MM-dd");
        public string? Remarks { get; set; }
    }
}
