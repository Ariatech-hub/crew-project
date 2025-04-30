namespace FarmToFork.Infrastructure.Repositories;

public class ProvinceRepository: IProvinceRepository
{
    private readonly AppDbContext _context;
    public ProvinceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Province>> GetAllAsync(bool disableTracking = true)
    {
        return disableTracking
            ? await _context
                .Provinces
                .AsNoTracking()
                .ToListAsync()
            : await _context.Provinces.ToListAsync();
    }

    public async Task<Province> GetByIdAsync(int id)
    {
        return await _context.Provinces.SingleAsync(x => x.Id == id);
    }
    public async Task UpdateProvinceActiveStatus(int provinceId)
    {
        Province provinceToUpdate = await _context.Provinces
            .Include(x => x.Districts)
            .Include(x => x.Districts).ThenInclude(x => x.Palikas)
            .SingleAsync(x => x.Id == provinceId);

        provinceToUpdate.Id = provinceId;

        provinceToUpdate.IsActive = !provinceToUpdate.IsActive;

        foreach (var district in provinceToUpdate.Districts)
        {

            district.IsActive = !district.IsActive;
            foreach (var palika in district.Palikas)
            {
                palika.IsActive = !palika.IsActive;
            }
        }
        await _context.SaveChangesAsync();
    }
}


