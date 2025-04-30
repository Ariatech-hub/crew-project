
using FarmToFork.Infrastructure.Context;
using FarmToFork.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmToFork.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesFromInfrastructure(this IServiceCollection services, IConfiguration Configuration)
        {
            var sqliteConnectionString = Configuration.GetConnectionString("cs");
            services.AddDbContext<SqlLiteDbContext>(options => options.UseSqlite(sqliteConnectionString));

            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICommunityRepository, CommunityRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IEducationLevelRepository, EducationLevelRepository>();
            services.AddScoped<IEthnicityRepository, EthnicityRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IGeneralRepository, GeneralRepository>();
            services.AddScoped<IOccupationRepository, OccupationRepository>();
            services.AddScoped<IPalikaRepository, PalikaRepository>();
            services.AddScoped<IFarmerRepository, FarmerRepository>();
            services.AddScoped<IRelationRepository, RelationRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IObstacleRepository, ObstacleRepository>();
            services.AddScoped<IYearRepository, YearRepository>();
            services.AddScoped<IGrainRepository, GrainRepository>();
            services.AddScoped<IInternetRepository, InternetRepository>();
            services.AddScoped<IParticipationOptionRepository, ParticipantOptionRepository>();
            services.AddScoped<IGrainCycleRepository, GrainCycleRepository>();
            services.AddScoped<IIncomeSourceRepository, IncomeSourceRepository>();
            services.AddScoped<ILabourDivisionRepository, LabourDivisonRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ILocationRepository, LocationRepositiory>();
            services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
            services.AddScoped<ISqlLiteRepository, SqlLiteRepository>();
            services.AddScoped<ICardImageRepository, CardImageRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IProductionPlanRepository, ProductionPlanRepository>();
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IDispatchRepository, DispatchRepository>();
            
            
           
            return services;
        }
    }
}
