namespace FarmToFork.Business.DTOs;

public class StockDto
{
    public int Id { get; set; }
    public int GrainCycleId { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal Quantity { get; set; }
    public decimal RemainingQuantity { get; set; }
    public string GrainCycleName { get; set; } = null!;
    public string GrainCycleNepaliName { get; set; } = null!;
    public string GrainCycleGrainName { get; set; } = null!;
    public string GrainCycleGrainNepaliName { get; set; } = null!;
}