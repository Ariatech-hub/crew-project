namespace FarmToFork.Core.Repositories;

public interface IPalikaRepository : IRepository<Palika>
{

    Task UpdatePalikaActiveStatus(int palikaId, string createdBy, DateTime createdDate);
}