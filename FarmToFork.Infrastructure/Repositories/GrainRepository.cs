namespace FarmToFork.Infrastructure.Repositories;

public class GrainRepository : IGrainRepository
{
    private readonly AppDbContext _appDbContext;

    public GrainRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Grain>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Grains.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.Grains.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<Grain>> GetAsync(Expression<Func<Grain, bool>> predicate)
    {
        return await _appDbContext.Grains.Where(predicate).OrderBy(x => x.OrderNumber).AsNoTracking().ToListAsync();
    }

    public async Task<Grain> GetByIdAsync(int id)
    {
        return await _appDbContext.Grains.SingleAsync(x => x.Id == id);
    }

    public async Task<Grain> AddAsync(Grain entity)
    {
        EntityEntry<Grain> insertedGrain = await _appDbContext.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedGrain.Entity;
    }

    public async Task UpdateAsync(Grain entity)
    {
        Grain grainToUpdate = await _appDbContext.Grains.SingleAsync(x => x.Id == entity.Id);

        grainToUpdate.Name = entity.Name;
        grainToUpdate.NepaliName = entity.NepaliName;
        grainToUpdate.IsActive = entity.IsActive;
        grainToUpdate.Code = entity.Code;
        grainToUpdate.OrderNumber = entity.OrderNumber;
        grainToUpdate.ModifiedBy = entity.ModifiedBy;
        grainToUpdate.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();

    }

    public async Task<Grain> DeleteAsync(Grain entity)
    {
        try
        {
            Grain grainToDeleted = await _appDbContext.Grains.SingleAsync(x => x.Id == entity.Id);
            EntityEntry<Grain> deletedGrain = _appDbContext.Grains.Remove(grainToDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedGrain.Entity;
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
}

