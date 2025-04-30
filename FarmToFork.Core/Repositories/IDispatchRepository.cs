namespace FarmToFork.Core.Repositories;

public interface IDispatchRepository
{
    Task Add(Dispatch dispatch);

    Task<IEnumerable<Dispatch>> GetAll();
}