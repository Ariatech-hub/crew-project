namespace FarmToFork.Business.Services;

public interface IStockService
{
    Task<IEnumerable<StockDto>> GetAllStock(int grainCycleId);
}

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepository;

    public StockService(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    public async Task<IEnumerable<StockDto>> GetAllStock(int grainCycleId)
    {
        IEnumerable<Stock> stocks = await _stockRepository.GetAllStock(grainCycleId);
        IEnumerable<StockDto> mappedStocks = ObjectMapper.Mapper.Map<IEnumerable<StockDto>>(stocks);
        return mappedStocks;

    }
}