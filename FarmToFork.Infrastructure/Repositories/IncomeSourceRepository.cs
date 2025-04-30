

namespace FarmToFork.Infrastructure.Repositories;

public class IncomeSourceRepository : IIncomeSourceRepository
{
    private readonly AppDbContext _context;
    public IncomeSourceRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<IncomeSource>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
        ? await _context.IncomeSources.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
        : await _context.IncomeSources.OrderByDescending(x => x.Id).ToListAsync();

    }

    public async Task<IEnumerable<IncomeSource>> GetAsync(Expression<Func<IncomeSource, bool>> predicate)
    {
        return await _context
            .IncomeSources
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(x => x.CreatedDate)
            .ToListAsync();
    }

    public async Task<IncomeSource> GetByIdAsync(int id)
    {
        return await _context.IncomeSources.SingleAsync(x => x.Id == id);
    }

    public async Task<IncomeSource> AddAsync(IncomeSource entity)
    {
        EntityEntry<IncomeSource> insertedIncomeSourceEntityEntry = await _context.IncomeSources.AddAsync(entity);
        await _context.SaveChangesAsync();
        return insertedIncomeSourceEntityEntry.Entity;
    }

    public async Task UpdateAsync(IncomeSource entity)
    {
        IncomeSource incomeSourceToBeUpdated = await _context.IncomeSources.SingleAsync(x => x.Id == entity.Id);

        incomeSourceToBeUpdated.Name = entity.Name;
        incomeSourceToBeUpdated.NepaliName = entity.NepaliName;
        incomeSourceToBeUpdated.Code = entity.Code;
        incomeSourceToBeUpdated.OrderNumber = entity.OrderNumber;
        incomeSourceToBeUpdated.IsActive = entity.IsActive;
        incomeSourceToBeUpdated.ModifiedBy = entity.ModifiedBy;
        incomeSourceToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _context.SaveChangesAsync();
    }

    public async Task<IncomeSource> DeleteAsync(IncomeSource entity)
    {
        try
        {
            IncomeSource incomeSourceToBeDeleted = await _context.IncomeSources.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<IncomeSource> deletedIncomeSource = _context.Remove(incomeSourceToBeDeleted);
            await _context.SaveChangesAsync();
            return deletedIncomeSource.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Income Source is in use"),
                _ => new Exception(exception.Message)
            };

        }

    }
}

