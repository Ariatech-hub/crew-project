

namespace FarmToFork.Infrastructure.Repositories
{
    internal class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _appDbContext;

        public ReceiptRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> GetLatestReceipt()
        {
            Receipt? recentReceiptNo = await _appDbContext.Receipts.OrderByDescending(x => x.ReceiptNumber).FirstOrDefaultAsync();
            return recentReceiptNo?.ReceiptNumber ?? 100;

        }

        public async Task Add(Receipt receipt)
        {
            await _appDbContext.AddAsync(receipt);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReceiptDetail>> GetAllReceiptDetail()
        {
            var data = await _appDbContext
                .ReceiptDetails
                .Select(x=> new ReceiptDetail()
                {
                    Id = x.Id,
                    UnitPrice = x.UnitPrice,
                    Quantity = x.Quantity,
                    Total = x.UnitPrice * x.Quantity,
                    ProductionPlan = new ProductionPlan()
                    {
                        Id = x.ProductionPlan.Id,
                        LandArea = x.ProductionPlan.LandArea,
                        TotalProduction = x.ProductionPlan.TotalProduction,
                        ActualSalesThroughCooperative = x.ProductionPlan.ActualSalesThroughCooperative,
                        Grain = new Grain()
                        {
                            Id = x.ProductionPlan.Grain.Id,
                            Name = x.ProductionPlan.Grain.Name,
                            NepaliName = x.ProductionPlan.Grain.NepaliName
                        },
                        GrainCycle = new GrainCycle()
                        {
                            Id = x.ProductionPlan.GrainCycle.Id,
                            Name = x.ProductionPlan.GrainCycle.Name,
                            NepaliName = x.ProductionPlan.GrainCycle.NepaliName
                        }
                    }
                })
                .OrderByDescending(x => x.Id)
                .ToListAsync()
                ;
            return data;
        }
    }
}
