namespace FarmToFork.Infrastructure.Repositories;
public class OccupationRepository : IOccupationRepository
{
    private readonly AppDbContext _appDbContext;

    public OccupationRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Occupation>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Occupations.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _appDbContext.Occupations.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<Occupation>> GetAsync(Expression<Func<Occupation, bool>> predicate)
    {
        return await _appDbContext.Occupations.Where(predicate).OrderBy(x=>x.OrderNumber).ToListAsync();
    }

    public async Task<Occupation> GetByIdAsync(int id)
    {
        return await _appDbContext.Occupations.SingleAsync(x => x.Id == id);
    }

    public async Task<Occupation> AddAsync(Occupation entity)
    {
        EntityEntry<Occupation> insertedOccupation = await _appDbContext.Occupations.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedOccupation.Entity;

    }

    public async Task UpdateAsync(Occupation entity)
    {
        Occupation occupationToUpdate = await _appDbContext.Occupations.SingleAsync(x => x.Id == entity.Id);
        
        occupationToUpdate.Id = entity.Id;
        occupationToUpdate.Name = entity.Name;
        occupationToUpdate.NepaliName = entity.NepaliName;
        occupationToUpdate.IsActive = entity.IsActive;
        occupationToUpdate.Code = entity.Code;
        occupationToUpdate.OrderNumber = entity.OrderNumber;
        occupationToUpdate.ModifiedBy = entity.ModifiedBy;
        occupationToUpdate.ModifiedDate = entity.ModifiedDate;

        await _appDbContext.SaveChangesAsync();

    }

    public async Task<Occupation> DeleteAsync(Occupation entity)
    {
        try
        {
            Occupation occupationToDelete = await _appDbContext.Occupations.SingleAsync(x => x.Id == entity.Id);
            

            EntityEntry<Occupation> deletedOccupation = _appDbContext.Occupations.Remove(occupationToDelete);
            await _appDbContext.SaveChangesAsync();
            return deletedOccupation.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not Microsoft.Data.SqlClient.SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Occupation Type is in use"),
                _ => new Exception(exception.Message)
            };

        }
    }
}

