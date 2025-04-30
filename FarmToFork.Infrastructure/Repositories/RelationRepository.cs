namespace FarmToFork.Infrastructure.Repositories;

public class RelationRepository : IRelationRepository
{
    private readonly AppDbContext _appDbContext;

    public RelationRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Relation>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Relations.AsNoTracking().ToListAsync()
            : await _appDbContext.Relations.ToListAsync();
    }

    public async Task<IEnumerable<Relation>> GetAsync(Expression<Func<Relation, bool>> predicate)
    {
        return await _appDbContext.Relations.OrderByDescending(x => x.Id).Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<Relation> GetByIdAsync(int id)
    {
        return await _appDbContext.Relations.SingleAsync(x => x.Id == id);
    }

    public async Task<Relation> AddAsync(Relation entity)
    {
        EntityEntry<Relation> insertedRelation = await _appDbContext.Relations.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedRelation.Entity;
    }

    public async Task UpdateAsync(Relation entity)
    {
        Relation relationToUpdate = await _appDbContext.Relations.SingleAsync(x => x.Id == entity.Id);

        relationToUpdate.Id = entity.Id;
        relationToUpdate.Name = entity.Name;
        relationToUpdate.NepaliName = entity.NepaliName;
        relationToUpdate.IsActive = entity.IsActive;
        relationToUpdate.ModifiedBy = entity.ModifiedBy;
        relationToUpdate.ModifiedDate = entity.ModifiedDate;

        await _appDbContext.SaveChangesAsync();


    }

    public async Task<Relation> DeleteAsync(Relation entity)
    {
        try
        {
            Relation relationToDelete = await _appDbContext.Relations.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<Relation> deletedRelation = _appDbContext.Relations.Remove(relationToDelete);
            await _appDbContext.SaveChangesAsync();
            return deletedRelation.Entity;


        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Relation is in use"),
                _ => new Exception(exception.Message)
            };

        }


    }
}

