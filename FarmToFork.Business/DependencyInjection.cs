using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.Services;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmToFork.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesFromBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ICommunityService, CommunityService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IEducationLevelService, EducationLevelService>();
            services.AddTransient<IEthnicityService, EthnicityService>();
            services.AddTransient<IGenderService, GenderServices>();
            services.AddTransient<IGeneralService, GeneralService>();
            services.AddTransient<IOccupationService, OccupationService>();
            services.AddTransient<IPalikaService, PalikaService>();
            services.AddTransient<IRelationService, RelationService>();
            services.AddTransient<IGeneralUtility, GeneralUtility>();
            services.AddTransient<IFarmerService, FarmerService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IGrainService,GrainService>();
            services.AddTransient<IParticipantOptionService,ParticipantOptionService>();
            services.AddTransient<IObstacleService,ObstacleService>();
            services.AddTransient<IYearService,YearService>();
            services.AddTransient<IInternetService,InternetService>();
            services.AddTransient<IIncomeSourceService, IncomeSourceService>();
            services.AddTransient<ILabourDivisionService, LabourDivisonService>();
            services.AddTransient<IMobileService, MobileService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IMaritalStatusService, MaritalStatusService>();
            services.AddTransient<ICardImageService, CardImageService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IProductionPlanService , ProductionPlanService>();
            services.AddTransient<IStockService , StockService>();
            services.AddTransient<IDispatchService , DispatchService>();
            services.AddTransient<IReceiptService , ReceiptSercice>();

            return services;
        }
    }
}
