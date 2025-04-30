using FarmToFork.Core.Exception;
using System.Security.Cryptography.Xml;

namespace FarmToFork.Infrastructure.Repositories;

public class GrainCycleRepository : IGrainCycleRepository
{
    private readonly AppDbContext _appDbContext;

    public GrainCycleRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<IEnumerable<GrainCycle>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.GrainCycles.OrderByDescending(x => x.Id)
            .Include(x=> x.Grain)
            .Include(x=> x.FromMonth)
            .Include(x => x.ToMonth)
            .Include(x=>x.Year)
            .AsNoTracking()
            .ToListAsync()
            : await _appDbContext.GrainCycles.OrderByDescending(x => x.Id).ToListAsync();

    }

    public async Task<IEnumerable<GrainCycle>> GetAsync(Expression<Func<GrainCycle, bool>> predicate)
    {
        return await _appDbContext.GrainCycles.Where(predicate).OrderBy(x => x.OrderNo).AsNoTracking().ToListAsync();
    }

    public async Task<GrainCycle> GetByIdAsync(int id)
    {
        return await _appDbContext.GrainCycles.SingleAsync(x => x.Id == id);
    }

    public async Task<GrainCycle> AddAsync(GrainCycle entity)
    {
        IEnumerable<GrainCycle> grainCycles = await _appDbContext.GrainCycles
            .Where(x => x.GrainId == entity.GrainId && x.YearId == entity.YearId && x.FromMonthId == entity.FromMonthId && x.ToMonthId == entity.ToMonthId)
            .ToListAsync();
        if (grainCycles.Any())
        {
            throw new DataValidationException($"The grain cycle for grain {entity.Name} already exists");
        }
        IEnumerable<GrainCycle> grainCyclesToBeUpdated = await _appDbContext.GrainCycles
            .Where(x => x.GrainId == entity.GrainId && x.YearId == entity.YearId 
            && ((x.FromMonthId <= entity.FromMonthId && x.ToMonthId >= entity.ToMonthId) ||
            (x.FromMonthId >= entity.FromMonthId && x.ToMonthId <= entity.ToMonthId)))
            .ToListAsync();
        EntityEntry<GrainCycle> insertedGrainCycleEntityEntry = await _appDbContext.GrainCycles.AddAsync(entity);
        if (grainCyclesToBeUpdated.Any())
        {
            foreach (var grainCycle in grainCyclesToBeUpdated)
            {
                grainCycle.IsActive = false;
                grainCycle.ModifiedBy = entity.CreatedBy;
                grainCycle.ModifiedDate = entity.CreatedDate;
            }
        }

        await _appDbContext.SaveChangesAsync();
        return insertedGrainCycleEntityEntry.Entity;
    }

    public async Task UpdateAsync(GrainCycle entity)
    {

        GrainCycle grainCycleToBeUpdated = await _appDbContext.GrainCycles.SingleAsync(x => x.Id == entity.Id);
        grainCycleToBeUpdated.Name = entity.Name;
        grainCycleToBeUpdated.NepaliName = entity.NepaliName;
        grainCycleToBeUpdated.FromMonth = entity.FromMonth;
        grainCycleToBeUpdated.ToMonth = entity.ToMonth;
        grainCycleToBeUpdated.IsActive = entity.IsActive;
        grainCycleToBeUpdated.ModifiedBy = entity.ModifiedBy;
        grainCycleToBeUpdated.ModifiedDate = entity.ModifiedDate;



        await _appDbContext.SaveChangesAsync();
    }

    public async Task<GrainCycle> DeleteAsync(GrainCycle entity)
    {
        GrainCycle grainCycleToBeDeleted = await _appDbContext.GrainCycles.SingleAsync(x => x.Id == entity.Id);

        EntityEntry<GrainCycle> deletedGrainCycle = _appDbContext.GrainCycles.Remove(grainCycleToBeDeleted);
        await _appDbContext.SaveChangesAsync();
        return deletedGrainCycle.Entity;

    }
    
    public async Task<GrainCycle> GrainCycleActiveStatusChanged(GrainCycle grainCycle)
    {
        IEnumerable<GrainCycle> grainCycles = await _appDbContext.GrainCycles
        .Where(x => x.GrainId == grainCycle.GrainId && x.YearId == grainCycle.YearId
            && ((x.FromMonthId <= grainCycle.FromMonthId && x.ToMonthId >= grainCycle.ToMonthId) ||
            (x.FromMonthId >= grainCycle.FromMonthId && x.ToMonthId <= grainCycle.ToMonthId)) && x.Id != grainCycle.Id).ToListAsync();

        GrainCycle grainCycleToBeUpdated = await _appDbContext.GrainCycles.SingleAsync(x => x.Id == grainCycle.Id);

        grainCycleToBeUpdated.IsActive = !grainCycleToBeUpdated.IsActive;
        grainCycleToBeUpdated.ModifiedBy = grainCycle.ModifiedBy;
        grainCycleToBeUpdated.ModifiedDate = grainCycle.ModifiedDate;

        if (grainCycleToBeUpdated.IsActive)
        {
            foreach (var item in grainCycles)
            {
                item.IsActive = false;
                item.ModifiedDate = grainCycle.ModifiedDate;
                item.ModifiedBy = grainCycle.ModifiedBy;
            }
        }
        await _appDbContext.SaveChangesAsync();
        return grainCycle;
    }

    public async Task<IEnumerable<GrainCycle>> GetGrainCycleByGrainId(int id)
    {
        IEnumerable<GrainCycle> grainCycles = await _appDbContext.GrainCycles.Where(x => x.GrainId == id).ToListAsync();
        return grainCycles;
    }
}