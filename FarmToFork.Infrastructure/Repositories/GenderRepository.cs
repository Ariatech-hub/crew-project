

namespace FarmToFork.Infrastructure.Repositories;

public class GenderRepository : IGenderRepository
{
    private readonly AppDbContext _appDbContext;

    public GenderRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<Gender>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Genders.AsNoTracking().ToListAsync()
            : await _appDbContext.Genders.ToListAsync();
    }

    public async Task<IEnumerable<Gender>> GetAsync(Expression<Func<Gender, bool>> predicate)
    {
        return await _appDbContext.Genders.Where(predicate).ToListAsync();
    }

    public async Task<Gender> GetByIdAsync(int id)
    {
        return await _appDbContext.Genders.SingleAsync(x => x.Id == id);
    }

    public Task<Gender> AddAsync(Gender entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Gender entity)
    {
        throw new NotImplementedException();
    }

    public Task<Gender> DeleteAsync(Gender entity)
    {
        throw new NotImplementedException();
    }
}

