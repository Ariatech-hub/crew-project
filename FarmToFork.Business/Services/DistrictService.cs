using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{

    public interface IDistrictService
    {
        Task<IEnumerable<DistrictDto>> GetAllActiveDistricts();

        Task UpdateActiveStatusOfDistrict(int districtId);

        Task<IEnumerable<DistrictTableDto>> GetAllDistrictsForTable();

        Task<IEnumerable<DistrictDto>> GetAllDistrictForSelection();
    }

    internal class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IGeneralUtility _generalUtility;

        public DistrictService(IDistrictRepository districtRepository, IGeneralUtility generalUtility)
        {
            _districtRepository = districtRepository;
            _generalUtility = generalUtility;
        }

        public async Task<IEnumerable<DistrictDto>> GetAllActiveDistricts()
        {
            var mappedDistricts =  ObjectMapper.Mapper.Map<IEnumerable<DistrictDto>>(await _districtRepository.GetAsync(x=>x.IsActive));
            return mappedDistricts;
        }

        public async Task UpdateActiveStatusOfDistrict(int districtId)
        {
            await _districtRepository.UpdateActiveStatusOfDistrict(districtId, _generalUtility.GetLoggedInUsername(),
                _generalUtility.GetCurrentNepalTime());
        }

        public async Task<IEnumerable<DistrictTableDto>> GetAllDistrictsForTable()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<DistrictTableDto>>(await _districtRepository.GetAllAsync());
        }

        public async Task<IEnumerable<DistrictDto>> GetAllDistrictForSelection()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<DistrictDto>>(await _districtRepository.GetAllAsync());
        }
    }
}
