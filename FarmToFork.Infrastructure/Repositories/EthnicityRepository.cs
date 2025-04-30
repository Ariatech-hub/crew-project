

namespace FarmToFork.Infrastructure.Repositories;

public class EthnicityRepository : IEthnicityRepository
{
    private readonly AppDbContext _appDbContext;


    public EthnicityRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

    }

    public async Task<IEnumerable<Ethnicity>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Ethnicities.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.Ethnicities.ToListAsync();
    }

    public async Task<IEnumerable<Ethnicity>> GetAsync(Expression<Func<Ethnicity, bool>> predicate)
    {
        return await _appDbContext.Ethnicities.Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<Ethnicity> GetByIdAsync(int id)
    {
        return await _appDbContext.Ethnicities.SingleAsync(x => x.Id == id);
    }

    public async Task<Ethnicity> AddAsync(Ethnicity entity)
    {
        await _appDbContext.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Ethnicity entity)
    {
        Ethnicity ethnicityToUpdate = await _appDbContext.Ethnicities.SingleAsync(x => x.Id == entity.Id);

        ethnicityToUpdate.Name = entity.Name;
        ethnicityToUpdate.NepaliName = entity.NepaliName;
        ethnicityToUpdate.Code = entity.Code;
        ethnicityToUpdate.OrderNumber = entity.OrderNumber;
        ethnicityToUpdate.IsActive = entity.IsActive;
        ethnicityToUpdate.ModifiedBy = entity.ModifiedBy;
        ethnicityToUpdate.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();


    }

    public async Task<Ethnicity> DeleteAsync(Ethnicity entity)
    {
        try
        {
            Ethnicity ethnicityToDelete = await _appDbContext.Ethnicities.SingleAsync(x => x.Id == entity.Id);

            _appDbContext.Remove(ethnicityToDelete);
            await _appDbContext.SaveChangesAsync();
            return ethnicityToDelete;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Ethnicity is in use"),
                _ => new Exception(exception.Message)
            };

        }

    }
}

