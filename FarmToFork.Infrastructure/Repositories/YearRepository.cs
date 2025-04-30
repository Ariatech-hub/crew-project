namespace FarmToFork.Infrastructure.Repositories;

public class YearRepository : IYearRepository
{
    private readonly AppDbContext _appDbContext;

    public YearRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Year>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Years.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.Years.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<Year>> GetAsync(Expression<Func<Year, bool>> predicate)
    {
        return await _appDbContext
            .Years
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Year> GetByIdAsync(int id)
    {
        return await _appDbContext.Years.SingleAsync(x => x.Id == id);
    }

    public async Task<Year> AddAsync(Year entity)
    {
        EntityEntry<Year> insertedYearEntityEntry = await _appDbContext.Years.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedYearEntityEntry.Entity;
    }

    public async Task UpdateAsync(Year entity)
    {
        Year yearToBeUpdated = await _appDbContext.Years.SingleAsync(x => x.Id == entity.Id);

        yearToBeUpdated.Name = entity.Name;
        yearToBeUpdated.NepaliName = entity.NepaliName;
        yearToBeUpdated.IsActive = entity.IsActive;
        yearToBeUpdated.StartDateAd = entity.StartDateAd;
        yearToBeUpdated.EndDateAd = entity.EndDateAd;
      //  yearToBeUpdated.OrderNo = entity.OrderNo;
        yearToBeUpdated.ModifiedBy = entity.ModifiedBy;
        yearToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Year> DeleteAsync(Year entity)
    {
        try
        {
            Year yearToBeDeleted = await _appDbContext.Years.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<Year> deletedYear = _appDbContext.Remove(yearToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedYear.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not Microsoft.Data.SqlClient.SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Year  is in use"),
                _ => new Exception(exception.Message)
            };

        }
    }
}