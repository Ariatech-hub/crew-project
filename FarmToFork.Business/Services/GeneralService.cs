using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{

    public interface IGeneralService
    {
        Task<IEnumerable<ProvinceDto>> GetAllProvinces();
        Task<IEnumerable<AccessLevelDto>> GetAllAccessLevels();
        Task<IEnumerable<MaritalStatusDto>> GetAllMartialStatus();
        Task<IEnumerable<LaborDivisionDto>> GetAllLaborDivisions();

        Task<IEnumerable<MonthDto>> GetAllMonths();
        Task<IEnumerable<LocationDto>> GetAllLocations();

    }
    internal class GeneralService : IGeneralService
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IGeneralUtility _generalUtility;
        

        public GeneralService(IGeneralRepository generalRepository, IGeneralUtility generalUtility, ILocationRepository locationRepository )
        {
            _generalRepository = generalRepository;
            _locationRepository = locationRepository;
            _generalUtility = generalUtility;
           
        }


        public async Task<IEnumerable<ProvinceDto>> GetAllProvinces()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<ProvinceDto>>(await _generalRepository.GetAllProvince());
        }

        public async Task<IEnumerable<AccessLevelDto>> GetAllAccessLevels()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<AccessLevelDto>>(await _generalRepository.GetAllAccessLevels());
        }

        public  async Task<IEnumerable<MaritalStatusDto>> GetAllMartialStatus()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<MaritalStatusDto>>(await _generalRepository.GetAllActiveMaritalStatus());
        }

        public async Task<IEnumerable<LaborDivisionDto>> GetAllLaborDivisions()
        {
            IEnumerable<LabourDivision> divisions = await _generalRepository.GetAllActiveLaborDivisions();
            IEnumerable<LaborDivisionDto> divisionDtos =  ObjectMapper.Mapper.Map<IEnumerable<LaborDivisionDto>>(divisions);
            return divisionDtos;
        }

        public async Task<IEnumerable<MonthDto>> GetAllMonths()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<MonthDto>>(await _generalRepository.GetAllMonths());
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocations()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<LocationDto>>(await _locationRepository.GetAllAsync());
        }
    }
}
