namespace FarmToFork.Core.Repositories;

public interface IStockRepository
{
    Task Add(Stock stock);
    Task UpdateQuantity(Stock stock);
    Task<bool> IsStockExist(int grainCycleId);
    Task<IEnumerable<Stock>> GetAllStock(int grainCycleId);
    Task<decimal> NumberOfStockAvailable(int grainCycleId);
}