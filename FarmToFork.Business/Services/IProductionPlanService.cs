using FarmToFork.Core.Exception;

namespace FarmToFork.Business.Services;

public interface IProductionPlanService
{
    Task<IEnumerable<ProductionPlanDto>> GetAll();
    Task<IEnumerable<ProductionPlanViewDto>> GetAll(ProductionPlanSearchDto productionPlanSearchDto);

    Task ProcessSale(List<ProductionPlanDto> productionPlanDtos);

    Task FarmerNonMemberSync(FarmerNonMemberProductionPlanDto farmerNonMemberProductionPlanDto);

}

public class ProductionPlanService : IProductionPlanService
{
    private readonly IProductionPlanRepository _productionPlanRepository;
    private readonly IFarmerRepository _farmerRepository;
    private readonly IGeneralUtility _generalUtility;
    private readonly IReceiptRepository _receiptRepository;
    private readonly IStockRepository _stockRepository;

    public ProductionPlanService(IProductionPlanRepository productionPlanRepository, IFarmerRepository farmerRepository, IGeneralUtility  generalUtility, IReceiptRepository receiptRepository, IStockRepository stockRepository)
    {
        _productionPlanRepository = productionPlanRepository;
        _farmerRepository = farmerRepository;
        _generalUtility = generalUtility;
        _receiptRepository = receiptRepository;
        _stockRepository = stockRepository;
    }
    public async Task<IEnumerable<ProductionPlanDto>> GetAll()
    {
        IEnumerable<ProductionPlan> productionPlans = await _productionPlanRepository.GetAllProductionPlan();
        IEnumerable<ProductionPlanDto> mappedProductionPlan = ObjectMapper.Mapper.Map<IEnumerable<ProductionPlanDto>>(productionPlans);
        return mappedProductionPlan;
    }

    public async Task<IEnumerable<ProductionPlanViewDto>> GetAll(ProductionPlanSearchDto productionPlanSearchDto)
    {
        IEnumerable<ProductionPlan> productionPlans = await _productionPlanRepository.GetAllProductionPlan();
        IEnumerable<ProductionPlanViewDto> mappedProductionPlanViewDtos = ObjectMapper.Mapper.Map<IEnumerable<ProductionPlanViewDto>>(productionPlans);
        if (productionPlanSearchDto.CommunityIds.Any())
        {
            mappedProductionPlanViewDtos =  mappedProductionPlanViewDtos.Where(x => productionPlanSearchDto.CommunityIds.Contains(x.FarmerCommunityId));
        }
        if (productionPlanSearchDto.GrainIds.Any())
        {
            mappedProductionPlanViewDtos = mappedProductionPlanViewDtos.Where(x => productionPlanSearchDto.GrainIds.Contains(x.GrainId));
        }

        if (productionPlanSearchDto.YearIds.Any())
        {
            mappedProductionPlanViewDtos = mappedProductionPlanViewDtos.Where(x => productionPlanSearchDto.YearIds.Contains(x.GrainCycleYearId));
        }
        return mappedProductionPlanViewDtos;
    }

    public async Task ProcessSale(List<ProductionPlanDto> productionPlanDtos)
    {
        string adminName = _generalUtility.GetLoggedInUsername();
        DateTime date = _generalUtility.GetCurrentNepalTime();

        if (productionPlanDtos is null || !productionPlanDtos.Any())
        {
            throw new DataValidationException("Invalid Production plan");
        }
        Receipt receipt = new ()
        {
            Amount = productionPlanDtos.Sum(x=>x.ActualSalesThroughCooperative ?? 0 * x.UnitPrice ?? 0),
            CreatedBy = adminName,
            CreatedDate = date,
            FarmerId = productionPlanDtos[0].FarmerId,
            ReceiptNumber = await GetRecentReceiptNo(),
        };

        List<ReceiptDetail> receiptDetails = new List<ReceiptDetail>();
        foreach (var productionPlan in productionPlanDtos)
        {
           ProductionPlan insertedProductionPlan  = await _productionPlanRepository.SoldGrainCycle(ObjectMapper.Mapper.Map<ProductionPlan>(productionPlan), adminName, date);
           receiptDetails.Add(new ReceiptDetail()
           {
               ProductionPlanId = insertedProductionPlan.Id,
               Quantity = productionPlan.ActualSalesThroughCooperative ?? 0,
               UnitPrice = productionPlan.UnitPrice ?? 0,
               CreatedBy = adminName,
               CreatedDate = date,
           });

           if (await _stockRepository.IsStockExist(productionPlan.GrainCycleId))
           {
               await _stockRepository.UpdateQuantity(new Stock()
               {
                   GrainCycleId = productionPlan.GrainCycleId,
                   Quantity = productionPlan.ActualSalesThroughCooperative ?? 0,
                   ModifiedBy = adminName,
                   ModifiedDate = date,
               });
           }
           else
           {
               await _stockRepository.Add(new Stock()
               {
                   GrainCycleId = productionPlan.GrainCycleId,
                   Quantity = productionPlan.ActualSalesThroughCooperative ?? 0,
                   RemainingQuantity = productionPlan.ActualSalesThroughCooperative ?? 0,
                   CreatedBy = adminName,
                   CreatedDate = date,
               });
           }
        }
        receipt.ReceiptDetails = receiptDetails;
        await _receiptRepository.Add(receipt);
    }

    public async Task FarmerNonMemberSync(FarmerNonMemberProductionPlanDto farmerNonMemberProductionPlanDto)
    {
        Farmer farmer = new ()
        {
            FirstName = farmerNonMemberProductionPlanDto.FirstName,
            MiddleName = farmerNonMemberProductionPlanDto.MiddleName,
            LastName = farmerNonMemberProductionPlanDto.LastName,
            ProvinceId = farmerNonMemberProductionPlanDto.ProvinceId,
            DistrictId = farmerNonMemberProductionPlanDto.DistrictId,
            PalikaId = farmerNonMemberProductionPlanDto.PalikaId,
            Ward = farmerNonMemberProductionPlanDto.Ward,
            CreatedDate = _generalUtility.GetCurrentNepalTime(),
            FullName = _generalUtility.AddName(farmerNonMemberProductionPlanDto.FirstName , farmerNonMemberProductionPlanDto.MiddleName , farmerNonMemberProductionPlanDto.LastName),
            Identifier = Guid.NewGuid().ToString(),
            IsActive = true,
            IsNonMember = true,
            ProductionPlans = ObjectMapper.Mapper.Map<List<ProductionPlan>>(farmerNonMemberProductionPlanDto.ProductionPlan),
        };
        foreach(var productionPlan in farmer.ProductionPlans)
        {
            productionPlan.CreatedBy = _generalUtility.GetLoggedInUsername();
            productionPlan.CreatedDate = _generalUtility.GetCurrentNepalTime();
            productionPlan.Id = 0;
        }

        await _farmerRepository.AddFarmer(farmer);

    }

    private async Task<int> GetRecentReceiptNo()
    {
        int receiptNo = await _receiptRepository.GetLatestReceipt();
        return receiptNo + 1;
    }
}