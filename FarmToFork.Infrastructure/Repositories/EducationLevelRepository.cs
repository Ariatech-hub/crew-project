
namespace FarmToFork.Infrastructure.Repositories;

public class EducationLevelRepository : IEducationLevelRepository
{
    private readonly AppDbContext _context;

    public EducationLevelRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<EducationLevel>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _context.EducationLevels.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
            : await _context.EducationLevels.ToListAsync();
    }

    public async Task<IEnumerable<EducationLevel>> GetAsync(Expression<Func<EducationLevel, bool>> predicate)
    {
        return await _context.EducationLevels.Where(predicate).ToListAsync();
    }

    public async Task<EducationLevel> GetByIdAsync(int id)
    {
        return await _context.EducationLevels.SingleAsync(x => x.Id == id);
    }

    public async Task<EducationLevel> AddAsync(EducationLevel entity)
    {
        await _context.EducationLevels.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(EducationLevel entity)
    {
        EducationLevel educationLevelToUpdate = await _context.EducationLevels.SingleAsync(x => x.Id == entity.Id);

        ValidationUtility.IsModelExist(entity);


        if (educationLevelToUpdate is null)
        {
            throw new Exception("Invalid Education Level");
        }

        educationLevelToUpdate.Name = entity.Name;
        educationLevelToUpdate.NepaliName = entity.NepaliName;
        educationLevelToUpdate.IsActive = entity.IsActive;
        educationLevelToUpdate.Code = entity.Code;
        educationLevelToUpdate.OrderNo = entity.OrderNo;
        educationLevelToUpdate.ModifiedBy = entity.ModifiedBy;
        educationLevelToUpdate.ModifiedDate = entity.ModifiedDate;
        await _context.SaveChangesAsync();

    }

    public async Task<EducationLevel> DeleteAsync(EducationLevel entity)
    {
        try
        {
            EducationLevel educationLevelToDelete = await _context.EducationLevels.SingleAsync(x => x.Id == entity.Id);
            if (educationLevelToDelete is null)
            {
                throw new Exception("Invalid Education Level");
            }

            EntityEntry<EducationLevel> deletedEducationLevel = _context.Remove(educationLevelToDelete);
            await _context.SaveChangesAsync();
            return deletedEducationLevel.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Education level is in use"),
                _ => new Exception(exception.Message)
            };

        }

    }



}

