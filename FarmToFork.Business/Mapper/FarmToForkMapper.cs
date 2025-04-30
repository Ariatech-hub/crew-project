using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FarmToFork.Business.DTOs;
using FarmToFork.Core.Entities;

namespace FarmToFork.Business.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<CommunityMapper>();
                cfg.AddProfile<EducationLevelMapper>();
                cfg.AddProfile<GeneralMapper>();
                cfg.AddProfile<OccupationMapper>();
                cfg.AddProfile<FarmerMapper>();
                cfg.AddProfile<EthnicityMapper>();
                cfg.AddProfile<RelationMapper>();
                cfg.AddProfile<GenderMapper>();
                cfg.AddProfile<MenuProfile>();
                cfg.AddProfile<InternetProfile>();
                cfg.AddProfile<ObstacleProfile>();
                cfg.AddProfile<IncomeSourceProfile>();
                cfg.AddProfile<LabourDivisionProfile>();
                cfg.AddProfile<GrainProfile>();
                cfg.AddProfile<ParticipantOptionProfile>();
                cfg.AddProfile<ProductionPlanProfile>();
                cfg.AddProfile<YearProfile>();
                cfg.AddProfile<ResearchMapper>();
                cfg.AddProfile<MarketStatusMapper>();
                cfg.AddProfile<CardImageProfile>();
                cfg.AddProfile<CustomerProfile>();
                cfg.AddProfile<StatusProfile>();
                cfg.AddProfile<ProductionPlanMapper>();
                cfg.AddProfile<StockMapper>();
                cfg.AddProfile<DispatchMapper>();
                cfg.AddProfile<ReceiptProfile>();

                
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class StockMapper : Profile
    {
        public StockMapper()
        {
            CreateMap<Stock, StockDto>().ReverseMap();
        }
    }

    public class CommunityMapper : Profile
    {
        public CommunityMapper()
        {
            CreateMap<Community, CommunityDto>();
            CreateMap<Community, CommunityInsertDto>();
            CreateMap<Community, CommunityInsertDto>().ReverseMap();
            CreateMap<Community, CommunityUpdateDto>();
            CreateMap<Community, CommunityUpdateDto>().ReverseMap();
            CreateMap<Community, CommunityDeleteDto>();
            CreateMap<Community, CommunityDeleteDto>().ReverseMap();
        }

    }

    public class EducationLevelMapper : Profile
    {
        public EducationLevelMapper()
        {
            CreateMap<EducationLevel, EducationLevelInsertDto>();
            CreateMap<EducationLevel, EducationLevelInsertDto>().ReverseMap();
            CreateMap<EducationLevel, EducationLevelDto>().ReverseMap();
            CreateMap<EducationLevel, EducationLevelDto>();
            CreateMap<EducationLevel, EducationLevelUpdateDto>();
            CreateMap<EducationLevel, EducationLevelUpdateDto>().ReverseMap();
            CreateMap<EducationLevel, EducationLevelDeleteDto>();
            CreateMap<EducationLevel, EducationLevelDeleteDto>().ReverseMap();
        }
    }

    public class OccupationMapper : Profile

    {
        public OccupationMapper()
        {
            CreateMap<Occupation, OccupationDto>();
            CreateMap<Occupation, OccupationDto>().ReverseMap();
            CreateMap<Occupation, OccupationInsertDto>();
            CreateMap<Occupation, OccupationInsertDto>().ReverseMap();
            CreateMap<Occupation, OccupationUpdateDto>();
            CreateMap<Occupation, OccupationUpdateDto>().ReverseMap();
            CreateMap<Occupation, OccupationTableDto>();
            CreateMap<Occupation, OccupationTableDto>().ReverseMap();
            CreateMap<Occupation, OccupationDeleteDto>();
            CreateMap<Occupation, OccupationDeleteDto>().ReverseMap();
        }
    }

    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<LocationDto, Location>();
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<LocationInsertDto, Location>();
            CreateMap<LocationInsertDto, Location>().ReverseMap();
            CreateMap<LocationUpdateDto, Location>();
            CreateMap<LocationUpdateDto, Location>().ReverseMap();
            CreateMap<LocationDeleteDto, Location>();
            CreateMap<LocationDeleteDto, Location>().ReverseMap();

            CreateMap<Province, ProvinceDto>().ReverseMap();
            CreateMap<Province, ProvinceDto>(); 
            CreateMap<Province, ProvinceTableDto>();
            CreateMap<Province, ProvinceTableDto>().ReverseMap();
            CreateMap<Province, ProvinceActiveStatusDto>(); 
            CreateMap<Province, ProvinceActiveStatusDto>().ReverseMap(); 
            CreateMap<Province, ProvinceUpdateActiveStatusDto>(); 
            CreateMap<Province, ProvinceUpdateActiveStatusDto>().ReverseMap(); 

            CreateMap<District, DistrictDto>().ReverseMap();
            CreateMap<District, DistrictDto>();
            CreateMap<DistrictTableDto, District>();
            CreateMap<DistrictTableDto, District>().ReverseMap();

            CreateMap<Palika, PalikaDto>().ReverseMap();
            CreateMap<Palika, PalikaDto>();
            CreateMap<Palika, PalikaUpdateActiveStatusDto>();
            CreateMap<Palika, PalikaUpdateActiveStatusDto>().ReverseMap();

            
            CreateMap<AccessLevel, AccessLevelDto>();
            CreateMap<AccessLevel, AccessLevelDto>().ReverseMap();

            CreateMap<Month, MonthDto>().ReverseMap();
            CreateMap<Month, MonthDto>();

            CreateMap<MaritalStatusDto, MaritalStatus>();
            CreateMap<MaritalStatusDto, MaritalStatus>().ReverseMap();
            CreateMap<MaritalStatusDeleteDto, MaritalStatus>();
            CreateMap<MaritalStatusDeleteDto, MaritalStatus>().ReverseMap();
            CreateMap<MaritalStatusUpdateDto, MaritalStatus>();
            CreateMap<MaritalStatusUpdateDto, MaritalStatus>().ReverseMap();
            CreateMap<MaritalStatusInsertDto, MaritalStatus>();
            CreateMap<MaritalStatusInsertDto, MaritalStatus>().ReverseMap();

        }
    }


    public class DispatchMapper : Profile
    {
        public DispatchMapper()
        {
            CreateMap<Dispatch, DispatchDto>().ReverseMap();
            CreateMap<Dispatch, DispatchOverviewDto>().ReverseMap();
        }
    }
    public class FarmerMapper : Profile
    {
        public FarmerMapper()
        {
            CreateMap<Farmer, FarmerDto>();
            CreateMap<Farmer, FarmerDto>().ReverseMap();
            CreateMap<Farmer, FarmerMemberShipIdDto>().ReverseMap();
            CreateMap<Farmer, FarmerMemberShipIdDto>().ReverseMap();
            CreateMap<Farmer, FarmerDtoVm>();
            CreateMap<Farmer, FarmerDtoVm>().ReverseMap();
            CreateMap<Farmer, FarmerReportDto>();
            CreateMap<Farmer, FarmerReportDto>().ReverseMap();
            CreateMap<Farmer, FarmerTableDto>().ReverseMap();
            CreateMap<Farmer, FarmerTableDto>();
            CreateMap<Farmer, FarmerViewDto>();
            CreateMap<Farmer, FarmerViewDto>().ReverseMap();
            CreateMap<Farmer, FarmerLastProductionPlanDto>().ReverseMap();

            CreateMap<FarmerMember, FarmerMemberDto>().ReverseMap();
            CreateMap<FarmerMember, FarmerMemberDto>();
            CreateMap<FarmerMember, FarmerMemberReportDto>();
            CreateMap<FarmerMember, FarmerMemberReportDto>().ReverseMap();
            CreateMap<FarmerMember, FarmerMemberViewDto>().ReverseMap();
            CreateMap<FarmerMember, FarmerMemberViewDto>();
          
            CreateMap<FarmerMemberProperty, FarmerMemberPropertyDto>();
            CreateMap<FarmerMemberProperty, FarmerMemberPropertyDto>().ReverseMap();
            CreateMap<FarmerMemberProperty, FarmerMemberPropertyReportDto>().ReverseMap();
            CreateMap<FarmerMemberProperty, FarmerMemberPropertyReportDto>();
            CreateMap<FarmerMemberProperty, FarmerMemberPropertyViewDto>();
            CreateMap<FarmerMemberProperty, FarmerMemberPropertyViewDto>().ReverseMap();

            CreateMap<FarmerMemberTransaction, FarmerMemberTransactionDto>();
            CreateMap<FarmerMemberTransaction, FarmerMemberTransactionDto>().ReverseMap();
            CreateMap<FarmerMemberTransaction, FarmerMemberTransactionReportDto>();
            CreateMap<FarmerMemberTransaction, FarmerMemberTransactionReportDto>().ReverseMap();
            CreateMap<FarmerMemberTransaction, FarmerMemberTransactionViewDto>().ReverseMap();
            CreateMap<FarmerMemberTransaction, FarmerMemberTransactionViewDto>();

            CreateMap<FarmerMemberAgricultureParticipant, FarmerMemberParticipantDto>();
            CreateMap<FarmerMemberAgricultureParticipant, FarmerMemberParticipantDto>().ReverseMap();

           


            CreateMap<FarmerMemberAgricultureParticipant, FarmerMemberAgricultureParticipantDto>().ReverseMap();
            CreateMap<FarmerMemberAgricultureParticipant, FarmerMemberAgricultureParticipantDto>();

            CreateMap<LastSeasonProduction, LastSeasonProductionDto>();
            CreateMap<LastSeasonProduction, LastSeasonProductionDto>().ReverseMap();

           

            CreateMap<ProductionPlan, ProductionPlanDto>().ReverseMap();
            CreateMap<ProductionPlan, ProductionPlanActiveGrainCycleDto>().ReverseMap();
            CreateMap<FarmerSoldProductionPlanDto, ProductionPlan>().ReverseMap();


            CreateMap<FarmerReportRelated, FarmerViewDto>();
            CreateMap<FarmerMemberReportRelated, FarmerMemberViewDto>();
            CreateMap<FarmerMemberPropertyReportRelated, FarmerMemberPropertyViewDto>();

            CreateMap<LastSeasonProductionReportRelated, LastSeasonProductionDto>();
            CreateMap<ProductionPlanReportRelated, ProductionPlanViewDto>();
            CreateMap<ResearchReportRelated, ResearchViewDto>();
            CreateMap<ResearchFarmerMemberParticipantReportRelated, FarmerMemberAgricultureParticipantDto>();
            CreateMap<ResearchObstacleReportRelated, ResearchObstacleViewDto>();
            CreateMap<ResearchMarkerStatusRelated, MarketStatusDto>();
            CreateMap<ResearchInternetUseReportRelated, ResearchInternetUseViewDto>();


        }
    }

    public class ProductionPlanMapper : Profile
    {
        public ProductionPlanMapper()
        {
           
        }
    }


    public class ResearchMapper : Profile
    {
        public ResearchMapper()
        {
            CreateMap<Research, ResearchDto>();
            CreateMap<Research, ResearchDto>().ReverseMap();

            CreateMap<Research, ResearchViewDto>();
            CreateMap<Research, ResearchViewDto>().ReverseMap();

            CreateMap<ResearchObstacle, ResearchObstacleViewDto>();
            CreateMap<ResearchObstacle, ResearchObstacleViewDto>().ReverseMap();

            CreateMap<ResearchInternetUse, ResearchInternetUseViewDto>();
            CreateMap<ResearchInternetUse, ResearchInternetUseViewDto>().ReverseMap();

        }
    }

    public class MarketStatusMapper : Profile
    {
        public MarketStatusMapper()
        {
            CreateMap<MarketStatus, MarketStatusDto>().ReverseMap();
            CreateMap<MarketStatus, MarketStatusDto>();

           
        }
    }

    public class EthnicityMapper : Profile
    {
        public EthnicityMapper()
        {
            CreateMap<Ethnicity, EthnicityDto>();
            CreateMap<Ethnicity, EthnicityDto>().ReverseMap();
            CreateMap<Ethnicity, EthnicityInsertDto>().ReverseMap();
            CreateMap<Ethnicity, EthnicityInsertDto>();
            CreateMap<Ethnicity, EthnicityUpdateDto>().ReverseMap();
            CreateMap<Ethnicity, EthnicityUpdateDto>();
            CreateMap<Ethnicity, EthnicityDeleteDto>();
            CreateMap<Ethnicity, EthnicityDeleteDto>().ReverseMap();

        }
    }

    public class RelationMapper : Profile
    {
        public RelationMapper()
        {
            CreateMap<Relation, RelationDto>();
            CreateMap<Relation, RelationDto>().ReverseMap();
        }
    }

    public class GenderMapper : Profile
    {
        public GenderMapper()
        {
            CreateMap<Gender, GenderDto>();
            CreateMap<Gender, GenderDto>().ReverseMap();
        }
    }

    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuDto>();
            CreateMap<Menu, MenuDto>().ReverseMap();
        }
    }

    public class InternetProfile : Profile
    {
        public InternetProfile()
        {
            CreateMap<InternetType, InternetTypeDto>();
            CreateMap<InternetType, InternetTypeDto>().ReverseMap();
            CreateMap<InternetType, InternetTypeTableDto>().ReverseMap();
            CreateMap<InternetType, InternetTypeTableDto>();
            CreateMap<InternetType, InternetTypeInsertDto>();
            CreateMap<InternetType, InternetTypeInsertDto>().ReverseMap();
            CreateMap<InternetType, InternetTypeUpdateDto>();
            CreateMap<InternetType, InternetTypeUpdateDto>().ReverseMap();
            CreateMap<InternetType, InternetTypeDeleteDto>().ReverseMap();
            CreateMap<InternetType, InternetTypeDeleteDto>();

            CreateMap<InternetUse, InternetUseDto>();
            CreateMap<InternetUse, InternetUseDto>().ReverseMap();
            CreateMap<InternetUse, InternetUseTableDto>().ReverseMap();
            CreateMap<InternetUse, InternetUseTableDto>();
            CreateMap<InternetUse, InternetUseInsertDto>();
            CreateMap<InternetUse, InternetUseInsertDto>().ReverseMap();
            CreateMap<InternetUse, InternetUseUpdateDto>().ReverseMap();
            CreateMap<InternetUse, InternetUseUpdateDto>();
            CreateMap<InternetUse, InternetUseDeleteDto>();
            CreateMap<InternetUse, InternetUseDeleteDto>().ReverseMap();

        }
    }

    public class ObstacleProfile : Profile
    {
        public ObstacleProfile()
        {
            CreateMap<Obstacle, ObstacleDto>();
            CreateMap<Obstacle, ObstacleDto>().ReverseMap();
            CreateMap<Obstacle, ObstacleInsertDto>();
            CreateMap<Obstacle, ObstacleInsertDto>().ReverseMap();
            CreateMap<Obstacle, ObstacleUpdateDto>();
            CreateMap<Obstacle, ObstacleUpdateDto>().ReverseMap();
            CreateMap<Obstacle, ObstacleTableDto>();
            CreateMap<Obstacle, ObstacleTableDto>().ReverseMap();
            CreateMap<Obstacle, ObstacleDeleteDto>();
            CreateMap<Obstacle, ObstacleDeleteDto>().ReverseMap();

        }
    }
   

    public class GrainProfile : Profile
    {
        public GrainProfile()
        {
            CreateMap<Grain, GrainDto>();
            CreateMap<GrainDto, Grain>();
            CreateMap<Grain, GrainInsertDto>();
            CreateMap<Grain, GrainInsertDto>().ReverseMap();
            CreateMap<Grain, GrainUpdateDto>();
            CreateMap<Grain, GrainUpdateDto>().ReverseMap();
            CreateMap<Grain, GrainDeleteDto>().ReverseMap();
            CreateMap<Grain, GrainDeleteDto>();
            CreateMap<Grain, GrainTableDto>();
            CreateMap<Grain, GrainTableDto>().ReverseMap();
            


            CreateMap<GrainCycle, GrainCycleDto>();
            CreateMap<GrainCycleDto, GrainCycle>();
            CreateMap<GrainCycle, GrainCycleTableDto>();
            CreateMap<GrainCycle, GrainCycleInsertDto>();
            CreateMap<GrainCycle, GrainCycleInsertDto>().ReverseMap();
            CreateMap<GrainCycleUpdateDto, GrainCycle>();
            CreateMap<GrainCycleDeleteDto, GrainCycle>();
            CreateMap<GrainCycleDeleteDto, GrainCycle>().ReverseMap();
            CreateMap<GrainCycleGetByIdDto, GrainCycle>();
            CreateMap<GrainCycleGetByIdDto, GrainCycle>().ReverseMap();
            CreateMap<GrainCycleUpdateStatusDto, GrainCycle>().ReverseMap();

        }
    }

    public class ParticipantOptionProfile : Profile
    {
        public ParticipantOptionProfile()
        {
            CreateMap<ParticipantOptionDto, ParticipationOption>();
            CreateMap<ParticipationOption, ParticipantOptionDto>();
            CreateMap<ParticipationOption, ParticipantOptionDeleteDto>();
            CreateMap<ParticipationOption, ParticipantOptionDeleteDto>().ReverseMap();
            CreateMap<ParticipantOptionInsertDto, ParticipationOption>();
            CreateMap<ParticipantOptionInsertDto, ParticipationOption>().ReverseMap();
            CreateMap<ParticipationOption, ParticipantOptionTableDto>();
            CreateMap<ParticipantOptionUpdateDto, ParticipationOption>();
            
        }
    }

    public class ProductionPlanProfile : Profile
    {
        public ProductionPlanProfile()
        {
            CreateMap<ProductionPlan, ProductionPlanDto>();
            CreateMap<ProductionPlan, ProductionPlanDto>().ReverseMap();

            CreateMap<ProductionPlan, ProductionPlanViewDto>();
            CreateMap<ProductionPlan, ProductionPlanViewDto>().ReverseMap();
            
        }
    }
    public class IncomeSourceProfile : Profile
    {
        public IncomeSourceProfile()
        {
            CreateMap<IncomeSource, IncomeSourceDto>().ReverseMap();
            CreateMap<IncomeSource, IncomeSourceDto>();
            CreateMap<IncomeSource, IncomeSourceInsertDto>();
            CreateMap<IncomeSource, IncomeSourceInsertDto>().ReverseMap();
            CreateMap<IncomeSource, IncomeSourceUpdateDto>();
            CreateMap<IncomeSource, IncomeSourceUpdateDto>().ReverseMap();
            CreateMap<IncomeSource, IncomeSourceTableDto>();
            CreateMap<IncomeSource, IncomeSourceTableDto>().ReverseMap();
            CreateMap<IncomeSource, IncomeSourceDeleteDto>();
            CreateMap<IncomeSource, IncomeSourceDeleteDto>().ReverseMap();

        }
    }
    public class LabourDivisionProfile : Profile
    {
        public LabourDivisionProfile()
        {
            CreateMap<LabourDivision, LabourDivisionDto>().ReverseMap();
            CreateMap<LabourDivision, LabourDivisionDto>();

            CreateMap<LabourDivision, LaborDivisionDto>().ReverseMap();
            CreateMap<LabourDivision, LaborDivisionDto>();

            CreateMap<LabourDivision, LabourDivisionInsertDto>();
            CreateMap<LabourDivision, LabourDivisionInsertDto>().ReverseMap();

            CreateMap<LabourDivision, LabourDivisionUpdateDto>();
            CreateMap<LabourDivision, LabourDivisionUpdateDto>().ReverseMap();
            CreateMap<LabourDivision, LabourDivisionTableDto>();

            CreateMap<LabourDivision, LabourDivisionTableDto>().ReverseMap();
            CreateMap<LabourDivision, LabourDivisionDeleteDto>();

            CreateMap<LabourDivision, LabourDivisionDeleteDto>().ReverseMap();

            

        }
    }

    public class YearProfile : Profile
    {
        public YearProfile()
        {
           
            CreateMap<Year, YearInsertDto>();
            CreateMap<Year, YearInsertDto>().ReverseMap();
            CreateMap<Year, YearUpdateDto>();
            CreateMap<Year, YearUpdateDto>().ReverseMap();
            CreateMap<Year, YearTableDto>().ReverseMap();
            CreateMap<Year, YearDeleteDto>();
            CreateMap<Year, YearDeleteDto>().ReverseMap();

        }
    }
    public class CardImageProfile : Profile
    {
        public CardImageProfile()
        {
            CreateMap<CardImage, CardImageDto>().ReverseMap();
            CreateMap<CardImage, CardImageInsertDto>().ReverseMap();
            CreateMap<CardImage, CardImageDeleteDto>().ReverseMap();
        }
    }

    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerInsertDto>().ReverseMap();
            CreateMap<CustomerContactDetail, CustomerContactDetailDto>().ReverseMap();
            CreateMap<CustomerContactDetail, CustomerContactDetailInsertDto>().ReverseMap();
            CreateMap<CustomerGrainCycle, CustomerGrainCycleInsertDto>().ReverseMap();
            CreateMap<CustomerGrainCycle, CustomerGrainCycleDto>().ReverseMap();
            CreateMap<CustomerGrainCycleStatus, CustomerGrainCycleStatusDto>().ReverseMap();
            CreateMap<CustomerGrainCycleStatusUpdateDto, CustomerGrainCycleStatus>();
            CreateMap<CustomerGrainCycle, CustomerGrainCycleCustomerViewDto>().ReverseMap();
            CreateMap<CustomerGrainCycleStatus, CustomerGrainCycleStatusCustomerViewDto>().ReverseMap();
            CreateMap<CustomerGrainCycle, CustomerGrainCycleReportDto>();

        }
    }
    
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>().ReverseMap();
        }
    }

    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptDto, ReceiptDetail>().ReverseMap();
            CreateMap<CustomerGrainCycleStatus, BuyerReceiptDto>();
        }
    }
}
