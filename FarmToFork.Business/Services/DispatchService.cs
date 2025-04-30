using FarmToFork.Core.Exception;

namespace FarmToFork.Business.Services;


public interface IDispatchService
{
    Task<IEnumerable<DispatchAvailableDto>> GetavailableDispatchIdTask(int grainCycleId, int customerId);

    Task ProcessDispatch(DispatchDto dispatchDto);

    Task<IEnumerable<DispatchOverviewDto>> GetAllDispatch();
}
public class DispatchService : IDispatchService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IGeneralUtility _generalUtility;
    private readonly IDispatchRepository _dispatchRepository;

    public DispatchService(ICustomerRepository customerRepository, IStockRepository stockRepository, IGeneralUtility generalUtility, IDispatchRepository dispatchRepository)
    {
        _customerRepository = customerRepository;
        _stockRepository = stockRepository;
        _generalUtility = generalUtility;
        _dispatchRepository = dispatchRepository;
    }
    public async Task<IEnumerable<DispatchAvailableDto>> GetavailableDispatchIdTask(int grainCycleId, int customerId)
    {

        IEnumerable<CustomerGrainCycle> data = await _customerRepository.GetCustomerGrainCycle(grainCycleId, customerId);
        IEnumerable<Stock> availableStock = await _stockRepository.GetAllStock(0);

        return data.Where(x=> x.IsDispatched == false).Select(item => new DispatchAvailableDto()
        {
            Id = item.Id,
            GrainCycleId = item.GrainCycleId,
            AvailableQuantity = availableStock.Where(x => x.GrainCycleId == item.GrainCycleId).Sum(x => x.Quantity),
            CustomerName = item.Customer.Name,
            CustomerId = item.CustomerId,
            GrainCycleName = item.GrainCycle.Name,
            GrainCycleGrainName = item.GrainCycle.Grain.Name
        }). OrderByDescending(x=>x.Id)
            .ToList();
    }

    public async Task ProcessDispatch(DispatchDto dispatchDto)
    {
        try
        {
            var number = await _stockRepository.NumberOfStockAvailable(dispatchDto.GrainCycleId);
            if (number < dispatchDto.Quantity)
            {
                throw new DataValidationException("No Item in stock");
            }
            await _customerRepository.UpdateCustomerGrainCycleDispatched(dispatchDto.CustomerGrainCycleId);

            Dispatch dispatch = ObjectMapper.Mapper.Map<Dispatch>(dispatchDto);
            dispatch.CreatedDate = _generalUtility.GetCurrentNepalTime();
            dispatch.CreatedBy = _generalUtility.GetLoggedInUsername();
            dispatch.Total = dispatch.Quantity * dispatch.UnitPrice;
            await _dispatchRepository.Add(dispatch);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<IEnumerable<DispatchOverviewDto>> GetAllDispatch()
    {
        IEnumerable<Dispatch> dispatches = await _dispatchRepository.GetAll();
        IEnumerable<DispatchOverviewDto> dispatchOverviewDtos = ObjectMapper.Mapper.Map<IEnumerable<DispatchOverviewDto>>(dispatches);
        return dispatchOverviewDtos;

    }
}