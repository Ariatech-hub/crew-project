namespace FarmToFork.Infrastructure.Repositories;
public class MaritalStatusRepository : IMaritalStatusRepository
{
    private readonly AppDbContext _appDbContext;
    public MaritalStatusRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<MaritalStatus> AddAsync(MaritalStatus entity)
    {
        EntityEntry<MaritalStatus> insertedMaritalStatusEntry = await _appDbContext.MaritalStatuses.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedMaritalStatusEntry.Entity;
    }

    public async Task<MaritalStatus> DeleteAsync(MaritalStatus entity)
    {
        try
        {
            MaritalStatus maritalStatusToBeDeleted = await _appDbContext.MaritalStatuses.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<MaritalStatus> deletedMaritalStatus = _appDbContext.Remove(maritalStatusToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedMaritalStatus.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not Microsoft.Data.SqlClient.SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("location  is in use"),
                _ => new Exception(exception.Message)
            };

        }
    }

    public async Task<IEnumerable<MaritalStatus>> GetAllAsync(bool disableTracking = true)
    {
        var data = disableTracking
             ? await _appDbContext.MaritalStatuses.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
             : await _appDbContext.MaritalStatuses.OrderByDescending(x => x.Id).ToListAsync();
        return data;
    }

    public async Task<IEnumerable<MaritalStatus>> GetAsync(Expression<Func<MaritalStatus, bool>> predicate)
    {
        return await _appDbContext
             .MaritalStatuses
             .AsNoTracking()
             .Where(predicate)
             .OrderBy(x => x.OrderId)
             .ToListAsync();
    }

    public async Task<MaritalStatus> GetByIdAsync(int id)
    {
        return await _appDbContext.MaritalStatuses.SingleAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(MaritalStatus entity)
    {
        MaritalStatus maritalStatusToBeUpdated = await _appDbContext.MaritalStatuses.SingleAsync(x => x.Id == entity.Id);

        maritalStatusToBeUpdated.Name = entity.Name;
        maritalStatusToBeUpdated.NepaliName = entity.NepaliName;
        maritalStatusToBeUpdated.IsActive = entity.IsActive;
        maritalStatusToBeUpdated.OrderId = entity.OrderId;
        maritalStatusToBeUpdated.ModifiedBy = entity.ModifiedBy;
        maritalStatusToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();
    }
}

