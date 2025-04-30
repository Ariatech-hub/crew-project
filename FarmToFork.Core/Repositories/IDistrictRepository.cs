
namespace FarmToFork.Core.Repositories;
public interface IDistrictRepository : IRepository<District>
{
    Task UpdateActiveStatusOfDistrict(int districtId, string createdBy, DateTime createdDate);
}