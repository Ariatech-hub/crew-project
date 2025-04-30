namespace FarmToFork.Core.Repositories;

public interface IProvinceRepository
{
    Task<IEnumerable<Province>> GetAllAsync(bool disableTracking = true);
    Task UpdateProvinceActiveStatus(int provinceId);
}

