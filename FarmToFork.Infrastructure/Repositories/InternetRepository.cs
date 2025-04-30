namespace FarmToFork.Infrastructure.Repositories;

public class InternetRepository : IInternetRepository
{
    private readonly AppDbContext _appDbContext;

    public InternetRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<InternetType>> GetAllAsync(bool disableTracking = true)
    {

        return disableTracking
            ? await _appDbContext.InternetTypes.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync()
            : await _appDbContext.InternetTypes.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<InternetType>> GetAsync(Expression<Func<InternetType, bool>> predicate)
    {
        return await _appDbContext
            .InternetTypes
            .Where(predicate)
            .OrderBy(x => x.OrderNumber)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<InternetType> GetByIdAsync(int id)
    {
        return await _appDbContext.InternetTypes.SingleAsync(x => x.Id == id);

    }

    public async Task<InternetType> AddAsync(InternetType entity)
    {
        EntityEntry<InternetType> insertedInternetTypesEntityEntry =
            await _appDbContext.InternetTypes.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedInternetTypesEntityEntry.Entity;
    }

    public async Task UpdateAsync(InternetType entity)
    {
        InternetType internetTypeToBeUpdated = await _appDbContext.InternetTypes.SingleAsync(x => x.Id == entity.Id);

        internetTypeToBeUpdated.Name = entity.Name;
        internetTypeToBeUpdated.NepaliName = entity.NepaliName;
        internetTypeToBeUpdated.Code = entity.Code;
        internetTypeToBeUpdated.OrderNumber = entity.OrderNumber;
        internetTypeToBeUpdated.IsActive = entity.IsActive;

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<InternetType> DeleteAsync(InternetType entity)
    {
        try
        {
            InternetType internetTypeToBeDeleted = await _appDbContext.InternetTypes.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<InternetType> deletedInternetTypesEntityEntry = _appDbContext.InternetTypes.Remove(internetTypeToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedInternetTypesEntityEntry.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Internet Type is in use"),
                _ => new Exception(exception.Message)
            };

        }


    }

    public async Task<IEnumerable<InternetUse>> GetAllActiveInternetUsage()
    {
        return await _appDbContext
            .InternetUses
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.OrderNumber)
            .ToListAsync();
    }

    public async Task<InternetUse> InsertInternetUse(InternetUse internetUse)
    {
        EntityEntry<InternetUse> insertedInternetUsEntityEntry = await _appDbContext.InternetUses.AddAsync(internetUse);
        await _appDbContext.SaveChangesAsync();
        return insertedInternetUsEntityEntry.Entity;

    }

    public async Task UpdateInternetUse(InternetUse internetUse)
    {
        InternetUse internetUseToBeUpdated = await GetById(internetUse.Id);

        internetUseToBeUpdated.Name = internetUse.Name;
        internetUseToBeUpdated.NepaliName = internetUse.NepaliName;
        internetUseToBeUpdated.Code = internetUse.Code;
        internetUseToBeUpdated.IsActive = internetUse.IsActive;
        internetUseToBeUpdated.OrderNumber = internetUse.OrderNumber;
        internetUseToBeUpdated.ModifiedBy = internetUse.ModifiedBy;
        internetUseToBeUpdated.ModifiedDate = internetUse.ModifiedDate;

        await _appDbContext.SaveChangesAsync();
    }

    public async Task<InternetUse> DeleteInternetUse(InternetUse internetUse)
    {
        try
        {
            InternetUse internetUseToBeDeleted = await GetById(internetUse.Id);
            EntityEntry<InternetUse> deletedInternetUsEntityEntry = _appDbContext.InternetUses.Remove(internetUseToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedInternetUsEntityEntry.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Internet Use is in use"),
                _ => new Exception(exception.Message)
            };

        }

    }

    public async Task<IEnumerable<InternetUse>> GetAllInternetUses()
    {
        return await _appDbContext.InternetUses.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
    }

    private async Task<InternetUse> GetById(int id)
    {
        return await _appDbContext.InternetUses.SingleAsync(x => x.Id == id);
    }
}

