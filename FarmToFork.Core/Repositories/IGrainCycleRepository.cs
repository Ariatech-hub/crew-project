namespace FarmToFork.Core.Repositories;
public interface IGrainCycleRepository : IRepository<GrainCycle>
{
    Task<IEnumerable<GrainCycle>> GetGrainCycleByGrainId(int id);
    Task<GrainCycle> GrainCycleActiveStatusChanged(GrainCycle grainCycle);
}
   

   