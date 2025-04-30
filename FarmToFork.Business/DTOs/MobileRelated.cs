using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    internal class MobileRelated
    {
    }

    public class InitialDataDto
    {

        public IEnumerable<OccupationDto> Occupations { get; set; } = new List<OccupationDto>();
        public IEnumerable<EducationLevelDto> EducationLevels { get; set; } = new List<EducationLevelDto>();
        public IEnumerable<RelationDto> Relationships { get; set; } = new List<RelationDto>();
        public IEnumerable<DistrictDto> Districts { get; set; } = new List<DistrictDto>();
        public IEnumerable<PalikaDto> Palikas { get; set; } = new List<PalikaDto>();
        public IEnumerable<GenderDto> Genders { get; set; } = new List<GenderDto>();
        public IEnumerable<LocationDto> Locations { get; set; } = new List<LocationDto>();
        public IEnumerable<ProvinceDto> Provinces { get; set; } = new List<ProvinceDto>();
        public IEnumerable<EthnicityDto> Ethnicities { get; set; } = new List<EthnicityDto>();
        public IEnumerable<CommunityDto> Communities { get; set; } = new List<CommunityDto>();
        public IEnumerable<MaritalStatusDto> MaritalStatus { get; set; } = new List<MaritalStatusDto>();
        public IEnumerable<GrainDto> Grains { get; set; } = new List<GrainDto>();
        public IEnumerable<ParticipantOptionDto> ParticipantOptions { get; set; } = new List<ParticipantOptionDto>();
        public IEnumerable<InternetTypeDto> InternetTypes { get; set; } = new List<InternetTypeDto>();
        public IEnumerable<InternetUseDto> InternetUse { get; set; } = new List<InternetUseDto>();
        public IEnumerable<LaborDivisionDto> LaborDivisions { get; set; } = new List<LaborDivisionDto>();
        public IEnumerable<ObstacleDto> Obstacles { get; set; } = new List<ObstacleDto>();
        public IEnumerable<MonthDto> Months { get; set; } = new List<MonthDto>();

        public IEnumerable<GrainCycleDto> GrainCycles { get; set; } = new List<GrainCycleDto>();

        public IEnumerable<FarmerMemberShipIdDto> Participants { get; set; } = new List<FarmerMemberShipIdDto>();

        public IEnumerable<FarmerTableDto> Farmers { get; set; } = Enumerable.Empty<FarmerTableDto>();






    }
}
