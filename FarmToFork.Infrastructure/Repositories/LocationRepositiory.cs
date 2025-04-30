

namespace FarmToFork.Infrastructure.Repositories;

public class LocationRepositiory : ILocationRepository
{
    private readonly AppDbContext _appDbContext;

    public LocationRepositiory(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

    }
    public async Task<IEnumerable<Location>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Locations.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.Locations.OrderByDescending(x => x.Id).ToListAsync();

    }

    public async Task<IEnumerable<Location>> GetAsync(Expression<Func<Location, bool>> predicate)
    {
        return await _appDbContext
            .Locations
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(x => x.OrderId)
            .ToListAsync();
    }

    public async Task<Location> GetByIdAsync(int id)
    {
        return await _appDbContext.Locations.SingleAsync(x => x.Id == id);
    }

    public async Task<Location> AddAsync(Location entity)
    {
        EntityEntry<Location> insertedlocationEntityEntry = await _appDbContext.Locations.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedlocationEntityEntry.Entity;
    }

    public async Task UpdateAsync(Location entity)
    {
        Location locationToBeUpdated = await _appDbContext.Locations.SingleAsync(x => x.Id == entity.Id);

        locationToBeUpdated.Name = entity.Name;
        locationToBeUpdated.NepaliName = entity.NepaliName;
        locationToBeUpdated.Code = entity.Code;
        locationToBeUpdated.IsActive = entity.IsActive;
        locationToBeUpdated.OrderId = entity.OrderId;
        locationToBeUpdated.ModifiedBy = entity.ModifiedBy;
        locationToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Location> DeleteAsync(Location entity)
    {
        try
        {
            Location locationToBeDeleted = await _appDbContext.Locations.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<Location> deletedlocation = _appDbContext.Remove(locationToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedlocation.Entity;
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
}


