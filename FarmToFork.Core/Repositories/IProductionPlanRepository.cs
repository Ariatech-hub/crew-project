using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Repositories
{
    public interface IProductionPlanRepository
    {
        Task<ProductionPlan> SoldGrainCycle(ProductionPlan productionPlan, string adminName , DateTime soldDate );
        Task<IEnumerable<ProductionPlan>> GetAllProductionPlan();

    }
}
