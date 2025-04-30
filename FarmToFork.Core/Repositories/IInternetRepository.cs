namespace FarmToFork.Core.Repositories;

public interface IInternetRepository : IRepository<InternetType>
{

    Task<IEnumerable<InternetUse>> GetAllActiveInternetUsage();

    Task<InternetUse> InsertInternetUse(InternetUse internetUse);
    Task UpdateInternetUse(InternetUse internetUse);
    Task<InternetUse> DeleteInternetUse(InternetUse internetUse);

    Task<IEnumerable<InternetUse>> GetAllInternetUses();

}

