namespace FarmToFork.Infrastructure.Repositories;
public class CommunityRepository : ICommunityRepository
{
    private readonly AppDbContext _context;

    public CommunityRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Community>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _context.Communities.OrderByDescending(x => x.Id).ToListAsync()
            : await _context.Communities.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<Community>> GetAsync(Expression<Func<Community, bool>> predicate)
    {
        return await _context.Communities.Where(predicate).OrderBy(x => x.OrderNo).AsNoTracking().ToListAsync();
    }

    public async Task<Community> GetByIdAsync(int id)
    {
        return await _context.Communities.SingleAsync(x => x.Id == id);
    }

    public async Task<Community> AddAsync(Community entity)
    {
        try
        {
            await _context.Communities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                2624 => new Exception("Code already exists!"),
                _ => new Exception(exception.Message)
            };

        }


    }

    public async Task UpdateAsync(Community entity)
    {
        Community communityToUpdate = await _context.Communities.SingleAsync(x => x.Id == entity.Id);
        ValidationUtility.IsModelExist(communityToUpdate);
        try
        {
            communityToUpdate.Name = entity.Name;
            communityToUpdate.Code = entity.Code;
            communityToUpdate.OrderNo = entity.OrderNo;
            communityToUpdate.NepaliName = entity.NepaliName;
            communityToUpdate.IsActive = entity.IsActive;
            communityToUpdate.ModifiedBy = entity.ModifiedBy;
            communityToUpdate.ModifiedDate = entity.ModifiedDate;
            await _context.SaveChangesAsync();

        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is not SqlException sqlException) throw new Exception(ex.Message);
            throw sqlException.Number switch
            {
                2627 => new Exception("sdf"),
                _ => new Exception(ex.Message)
            };
        }

    }

    public async Task<Community> DeleteAsync(Community entity)
    {
        try
        {
            Community communityToDelete = await _context.Communities.SingleAsync(x => x.Id == entity.Id);
            _context.Remove(communityToDelete);
            await _context.SaveChangesAsync();
            return communityToDelete;
        }
        catch (Exception ex)
        {
           
            if (ex is SqlException sqlException)
            {
                throw sqlException.Number switch
                {
                    547 => new Exception("sdf"),
                    _ => new Exception(ex.Message)
                };
            }
            throw new Exception(ex.Message);
        }
    }
}
