namespace FarmToFork.Core.Repositories;
public interface IGeneralRepository
{
    Task<IEnumerable<Province>> GetAllProvince();

    Task<IEnumerable<AccessLevel>> GetAllAccessLevels();

    Task<IEnumerable<MaritalStatus>> GetAllActiveMaritalStatus();

    Task<IEnumerable<LabourDivision>> GetAllActiveLaborDivisions();

    Task<IEnumerable<Month>> GetAllMonths();

}