

namespace FarmToFork.Infrastructure.Repositories;

public class DistrictRepository : IDistrictRepository
{
    private readonly AppDbContext _appDbContext;


    public DistrictRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

    }
    public async Task<IEnumerable<District>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _appDbContext.Districts
                .Include(x => x.Province)
                .AsNoTracking().OrderBy(x => x.Name).ToListAsync()
            : await _appDbContext.Districts.ToListAsync();
    }

    public async Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>> predicate)
    {
        IEnumerable<District> districts = await _appDbContext.Districts.Where(predicate).AsNoTracking().ToListAsync();
        return districts;
    }

    public async Task<District> GetByIdAsync(int id)
    {
        return await _appDbContext.Districts.SingleAsync(x => x.Id == id);
    }

    public async Task<District> AddAsync(District entity)
    {
        EntityEntry<District> districtAddAsync = await _appDbContext.Districts.AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return districtAddAsync.Entity;
    }

    public async Task UpdateAsync(District entity)
    {
        District districtToUpdate = await _appDbContext.Districts.SingleAsync(x => x.Id == entity.Id);
        ValidationUtility.IsModelExist(districtToUpdate);

        districtToUpdate.Id = entity.Id;
        districtToUpdate.Name = entity.Name;
        districtToUpdate.NepaliName = entity.NepaliName;
        districtToUpdate.IsActive = entity.IsActive;
        districtToUpdate.ModifiedBy = entity.ModifiedBy;
        districtToUpdate.ModifiedDate = entity.ModifiedDate;

        await _appDbContext.SaveChangesAsync();


    }
    public async Task<District> DeleteAsync(District entity)
    {
        District districtToDelete = await _appDbContext.Districts.SingleAsync(x => x.Id == entity.Id);
        ValidationUtility.IsModelExist(districtToDelete);
        EntityEntry<District> deletedDistrict = _appDbContext.Districts.Remove(districtToDelete);
        return deletedDistrict.Entity;


    }

    public async Task UpdateActiveStatusOfDistrict(int districtId, string createdBy, DateTime createdDate)
    {
        await using var transaction = await _appDbContext.Database.BeginTransactionAsync();
        try
        {
            District district = await _appDbContext.Districts.SingleAsync(x => x.Id == districtId);
            district.IsActive = !district.IsActive;
            district.ModifiedBy = createdBy;
            district.ModifiedDate = createdDate;
            await _appDbContext.SaveChangesAsync();

            IEnumerable<Palika> palikasToUpdate = await _appDbContext.Palikas.Where(x => x.DistrictId == districtId).ToListAsync();
            foreach (var palika in palikasToUpdate)
            {
                palika.IsActive = district.IsActive;
            }

            await _appDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
    }
}

