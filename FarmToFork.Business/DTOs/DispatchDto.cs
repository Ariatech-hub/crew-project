namespace FarmToFork.Business.DTOs;



public class DispatchAvailableDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public int GrainCycleId { get; set; }
    public string GrainCycleName { get; set; } = null!;
    public string GrainCycleGrainName { get; set; } = null!;
    public decimal Quantity { get; set; }
    public decimal AvailableQuantity { get; set; }
    public int CustomerId { get; set; }
}

public class DispatchDto
{
    public int Id { get; set; }
    public int CustomerGrainCycleId { get; set; }
    public int CustomerId { get; set; }
    public int GrainCycleId { get; set; }
    public decimal Amount { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}

public class DispatchOverviewDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int GrainCycleId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedDate { get; set; }
    public string GrainCycleName { get; set; } = null!;
    public string GrainCycleGrainName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
    public string CustomerPhoneNumber { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerGrainCycleGrainCycleName { get; set; } = null!;
    public string CustomerGrainCycleGrainCycleGrainName { get; set; } = null!;
    public string CustomerGrainCycleCustomerName { get; set; } = null!;
    public string CustomerGrainCycleCustomerPhoneNumber { get; set; } = null!;
    public string CustomerGrainCycleCustomerEmail { get; set; } = null!;

}