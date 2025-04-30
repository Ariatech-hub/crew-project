
namespace FarmToFork.Infrastructure.Repositories;

public class GeneralRepository : IGeneralRepository
{
    private readonly AppDbContext _appDbContext;

    public GeneralRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Province>> GetAllProvince()
    {
        return await _appDbContext.Provinces.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<AccessLevel>> GetAllAccessLevels()
    {
        return await _appDbContext.AccessLevels.AsNoTracking().ToListAsync();
    }



    public async Task<IEnumerable<MaritalStatus>> GetAllActiveMaritalStatus()
    {
        return await _appDbContext.MaritalStatuses
            .Where(x => x.IsActive != null && x.IsActive.Value)
            .OrderBy(x => x.OrderId).ToListAsync();
    }

    public async Task<IEnumerable<LabourDivision>> GetAllActiveLaborDivisions()
    {
        return await _appDbContext.LabourDivisions.AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.OrderId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Month>> GetAllMonths()
    {
        return await _appDbContext
            .Months
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }




}

