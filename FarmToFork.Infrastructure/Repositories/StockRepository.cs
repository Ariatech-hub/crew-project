namespace FarmToFork.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly AppDbContext _appDbContext;

    public StockRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task Add(Stock stock)
    {
        await _appDbContext.Stocks.AddAsync(stock);
        await _appDbContext.SaveChangesAsync();

    }

    public async Task UpdateQuantity(Stock stock)
    {
        Stock stockToBeUpdated = await _appDbContext.Stocks.SingleAsync(x => x.GrainCycleId == stock.GrainCycleId);
        stockToBeUpdated.Quantity += stock.Quantity;
        stockToBeUpdated.RemainingQuantity += stock.Quantity;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<bool> IsStockExist(int grainCycleId)
    {
        return await _appDbContext.Stocks.AnyAsync(x => x.GrainCycleId == grainCycleId);
    }

    public async Task<IEnumerable<Stock>> GetAllStock(int grainCycleId)
    {
        IEnumerable<Stock> stocks = await _appDbContext
            .Stocks.Where(x => (grainCycleId == 0 || x.GrainCycleId == grainCycleId))
            .Select(x => new Stock()
            {
                Id = x.Id,
                GrainCycleId = x.GrainCycleId,
                Quantity = x.Quantity,
                RemainingQuantity = x.RemainingQuantity,
                GrainCycle = new GrainCycle()
                {
                    Id = x.GrainCycle.Id,
                    Name = x.GrainCycle.Name,
                    NepaliName = x.GrainCycle.NepaliName,
                   Grain = new Grain()
                   {
                       Id = x.GrainCycle.Grain.Id,
                       Name = x.GrainCycle.Grain.Name,
                       NepaliName = x.GrainCycle.Grain.NepaliName,
                   }
                }
            })
            .AsNoTracking().ToListAsync();

        return stocks;
    }

    public async Task<decimal> NumberOfStockAvailable(int grainCycleId)
    {
        var data = await _appDbContext.Stocks.SingleAsync(x => x.GrainCycleId == grainCycleId);
        return data.RemainingQuantity;
    }
}