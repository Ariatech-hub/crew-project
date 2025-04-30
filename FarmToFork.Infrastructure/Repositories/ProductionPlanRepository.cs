using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Infrastructure.Repositories
{
    internal class ProductionPlanRepository : IProductionPlanRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductionPlanRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        

        public async Task<ProductionPlan> SoldGrainCycle(ProductionPlan productionPlan, string adminName, DateTime soldDate)
        {
            ProductionPlan productionPlanToBeUpdated = await _appDbContext.ProductionPlans.FirstAsync(x => x.Id == productionPlan.Id);
            productionPlanToBeUpdated.ActualSalesThroughCooperative = productionPlan.ActualSalesThroughCooperative;
            productionPlanToBeUpdated.SaleBy = adminName;
            productionPlanToBeUpdated.SaleDate = soldDate;
            productionPlanToBeUpdated.Remarks = productionPlan.Remarks;
            await _appDbContext.SaveChangesAsync();
            return productionPlanToBeUpdated;
        }

        public async Task<IEnumerable<ProductionPlan>> GetAllProductionPlan()
        {
            IEnumerable<ProductionPlan> productionPlans = await _appDbContext.ProductionPlans
                .Where(x => x.ActualSalesThroughCooperative == null && x.TotalSalesThroughCooperative > 0)
                .Select(x=> new ProductionPlan()
                {
                    Id = x.Id,
                    FarmerId = x.FarmerId,
                    GrainId = x.GrainId,
                    GrainCycleId = x.GrainCycleId,
                    LandArea = x.LandArea,
                    TotalProduction = x.TotalProduction,
                    HomeUse = x.HomeUse,
                    TotalSalesThroughCooperative = x.TotalSalesThroughCooperative,
                    Farmer = new Farmer()
                    {
                        Id = x.Farmer.Id,
                        FullName = x.Farmer.FullName,
                        Code = x.Farmer.Code,
                        CommunityId = x.Farmer.CommunityId,
                        //Community = x.Farmer.Community
                    },
                    GrainCycle = new GrainCycle()
                    {
                        Id = x.GrainCycle.Id,
                        Name = x.GrainCycle.Name,
                        YearId = x.GrainCycle.YearId,
                        Year = new Year()
                        {
                            Id = x.GrainCycle.Id,
                            Name = x.GrainCycle.Name
                        },
                        FromMonth = new Month()
                        {
                            Id = x.GrainCycle.FromMonth.Id,
                            Name = x.GrainCycle.FromMonth.Name
                        }
                        
                    },
                    Grain = new Grain()
                    {
                        Id = x.Grain.Id,
                        Name = x.Grain.Name,
                    }
                }).
                AsNoTracking().ToListAsync();
            return productionPlans;



        }
    }
}
