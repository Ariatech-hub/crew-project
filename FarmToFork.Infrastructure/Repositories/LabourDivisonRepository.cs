namespace FarmToFork.Infrastructure.Repositories;

public class LabourDivisonRepository : ILabourDivisionRepository
{
    private readonly AppDbContext _context;
    public LabourDivisonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LabourDivision> AddAsync(LabourDivision entity)
    {
        EntityEntry<LabourDivision> insertedLabourDivisionEntityEntry = await _context.LabourDivisions.AddAsync(entity);
        await _context.SaveChangesAsync();
        return insertedLabourDivisionEntityEntry.Entity;
    }

    public async Task<LabourDivision> DeleteAsync(LabourDivision entity)
    {
        try
        {
            LabourDivision labourDivisionToBeDeleted = await _context.LabourDivisions.SingleAsync(x => x.Id == entity.Id);

            EntityEntry<LabourDivision> deletedLabourDivision = _context.Remove(labourDivisionToBeDeleted);
            await _context.SaveChangesAsync();
            return deletedLabourDivision.Entity;
        }
        catch (DbUpdateException exception)
        {
            if (exception.InnerException is not SqlException sqlException) throw new Exception(exception.Message);
            throw sqlException.Number switch
            {
                547 => new Exception("Labour Division is in use"),
                _ => new Exception(exception.Message)
            };

        }
    }

    public async Task<IEnumerable<LabourDivision>> GetAllAsync(bool disableTracking = true)
    {

        return disableTracking
        ? await _context.LabourDivisions.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync()
        : await _context.LabourDivisions.OrderByDescending(x => x.Id).ToListAsync();
    }

    public async Task<IEnumerable<LabourDivision>> GetAsync(Expression<Func<LabourDivision, bool>> predicate)
    {
        return await _context
            .LabourDivisions
            .AsNoTracking()
            .Where(predicate)
            .OrderBy(x => x.OrderId)
            .ToListAsync();
    }

    public async Task<LabourDivision> GetByIdAsync(int id)
    {
        return await _context.LabourDivisions.SingleAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(LabourDivision entity)
    {
        LabourDivision labourDivisionToBeUpdated = await _context.LabourDivisions.SingleAsync(x => x.Id == entity.Id);

        labourDivisionToBeUpdated.Name = entity.Name;
        labourDivisionToBeUpdated.NepaliName = entity.NepaliName;
        labourDivisionToBeUpdated.Code = entity.Code;
        labourDivisionToBeUpdated.OrderId = entity.OrderId;
        labourDivisionToBeUpdated.IsActive = entity.IsActive;
        labourDivisionToBeUpdated.ModifiedBy = entity.ModifiedBy;
        labourDivisionToBeUpdated.ModifiedDate = entity.ModifiedDate;
        await _context.SaveChangesAsync();
    }
}

