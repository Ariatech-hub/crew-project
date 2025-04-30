namespace FarmToFork.Infrastructure.Repositories;

public class PalikaRepository : IPalikaRepository
{
    private readonly AppDbContext _appDbContext;

    public PalikaRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Palika>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext
                .Palikas
                .Include(x => x.District)
                .OrderBy(x => x.District.Name)
                .AsNoTracking()
                .ToListAsync()
            : await _appDbContext.Palikas.ToListAsync();
    }

    public async Task<IEnumerable<Palika>> GetAsync(Expression<Func<Palika, bool>> predicate)
    {
        return await _appDbContext.Palikas.Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task<Palika> GetByIdAsync(int id)
    {
        return await _appDbContext.Palikas.SingleAsync(x => x.Id == id);
    }

    public async Task<Palika> AddAsync(Palika entity)
    {
        EntityEntry<Palika> insertedPalika = await _appDbContext.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return insertedPalika.Entity;
    }

    public async Task UpdateAsync(Palika entity)
    {
        Palika palikaToUpdate = await _appDbContext.Palikas.SingleAsync(x => x.Id == entity.Id);

        palikaToUpdate.Id = entity.Id;
        palikaToUpdate.Name = entity.Name;
        palikaToUpdate.NepaliName = entity.NepaliName;
        palikaToUpdate.IsActive = entity.IsActive;
        palikaToUpdate.ModifiedBy = entity.ModifiedBy;
        palikaToUpdate.ModifiedDate = entity.ModifiedDate;
        await _appDbContext.SaveChangesAsync();


    }

    public async Task<Palika> DeleteAsync(Palika entity)
    {
        Palika palikaToDelete = await _appDbContext.Palikas.SingleAsync(x => x.Id == entity.Id);

        EntityEntry<Palika> deletedPalika = _appDbContext.Palikas.Remove(palikaToDelete);
        await _appDbContext.SaveChangesAsync();
        return deletedPalika.Entity;

    }

    public async Task UpdatePalikaActiveStatus(int palikaId, string createdBy, DateTime createdDate)
    {
        Palika palika = await _appDbContext.Palikas.SingleAsync(x => x.Id == palikaId);
        palika.IsActive = !palika.IsActive;
        palika.CreatedDate = createdDate;
        palika.CreatedBy = createdBy;
        await _appDbContext.SaveChangesAsync();
    }
}

