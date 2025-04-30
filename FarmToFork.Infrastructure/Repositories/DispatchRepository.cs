using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace FarmToFork.Infrastructure.Repositories;

public class DispatchRepository : IDispatchRepository
{
    private readonly AppDbContext _appDbContext;

    public DispatchRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task Add(Dispatch dispatch)
    {
        await _appDbContext.Dispatches.AddAsync(dispatch);
        await _appDbContext.SaveChangesAsync();

        Stock stock = await _appDbContext.Stocks.FirstAsync(x => x.GrainCycleId == dispatch.CustomerGrainCycle.GrainCycleId);
        stock.RemainingQuantity -= dispatch.Quantity;
        stock.ModifiedBy = dispatch.CreatedBy;
        await _appDbContext.SaveChangesAsync();

    }

    public async Task<IEnumerable<Dispatch>> GetAll()
    {
        IEnumerable<Dispatch> dispatches = await _appDbContext
            .Dispatches.Select(dispatch => new Dispatch()
            {
                Id = dispatch.Id,
                CustomerGrainCycleId = dispatch.CustomerGrainCycleId,
                UnitPrice = dispatch.UnitPrice,
                Quantity = dispatch.Quantity,
                Total = dispatch.Total,
                CreatedDate = dispatch.CreatedDate,
                CustomerGrainCycle = new CustomerGrainCycle()
                {
                    GrainCycle = new GrainCycle()
                    {
                        Id = dispatch.CustomerGrainCycle.GrainCycle.Id,
                        Name = dispatch.CustomerGrainCycle.GrainCycle.Name,
                        Grain = new Grain()
                        {
                            Id = dispatch.CustomerGrainCycle.GrainCycle.Grain.Id,
                            Name = dispatch.CustomerGrainCycle.GrainCycle.Grain.Name
                        }
                    },
                    Customer = new Customer()
                    {
                        Id = dispatch.CustomerGrainCycle.Customer.Id,
                        Name = dispatch.CustomerGrainCycle.Customer.Name,
                        PhoneNumber = dispatch.CustomerGrainCycle.Customer.PhoneNumber,
                        Email = dispatch.CustomerGrainCycle.Customer.Email
                    }
                },
                
               
            })
            .OrderByDescending(x => x.Id)
            .ToListAsync();
        return dispatches;
    }
}