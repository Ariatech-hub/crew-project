namespace FarmToFork.Infrastructure.Repositories;

public class ObstacleRepository : IObstacleRepository
{
    private readonly AppDbContext _appDbContext;

    public ObstacleRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Obstacle>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Obstacles.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.Obstacles.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

    }

    public async Task<IEnumerable<Obstacle>> GetAsync(Expression<Func<Obstacle, bool>> predicate)
    {
        return await _appDbContext
            .Obstacles
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(x => x.OrderId)
            .ToListAsync();
    }

    public async Task<Obstacle> GetByIdAsync(int id)
    {
        return await _appDbContext.Obstacles.SingleAsync(x => x.Id == id);
    }

    public async Task<Obstacle> AddAsync(Obstacle entity)
    {
        EntityEntry<Obstacle> insertedObstacleEntityEntry = await _appDbContext.Obstacles.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedObstacleEntityEntry.Entity;
    }

    public async Task UpdateAsync(Obstacle entity)
    {
        Obstacle obstacleToBeUpdated = await _appDbContext.Obstacles.SingleAsync(x => x.Id == entity.Id);

        obstacleToBeUpdated.Name = entity.Name;
        obstacleToBeUpdated.NepaliName = entity.NepaliName;
        obstacleToBeUpdated.Code = entity.Code;
        obstacleToBeUpdated.IsActive = entity.IsActive;
        obstacleToBeUpdated.OrderId = entity.OrderId;
        obstacleToBeUpdated.IsSale = entity.IsSale;
        obstacleToBeUpdated.IsProduction = entity.IsProduction;
        obstacleToBeUpdated.ModifiedBy = entity.ModifiedBy;
        obstacleToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Obstacle> DeleteAsync(Obstacle entity)
    {
        try
        {
            Obstacle obstacleToBeDeleted = await _appDbContext.Obstacles.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<Obstacle> deletedObstacle = _appDbContext.Remove(obstacleToBeDeleted);
            await _appDbContext.SaveChangesAsync();
            return deletedObstacle.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not Microsoft.Data.SqlClient.SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Obstacle  is in use"),
                _ => new Exception(exception.Message)
            };

        }

    }
}

