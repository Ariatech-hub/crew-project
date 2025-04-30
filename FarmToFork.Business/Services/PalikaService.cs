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

    public interface IPalikaService
    {
        Task<IEnumerable<PalikaDto>> GetAllActivePalikas();
        Task<IEnumerable<PalikaDto>> GetPalikaByDistrictId(int districtId);
        Task<IEnumerable<PalikaUpdateActiveStatusDto>> GetAll();

        Task<IEnumerable<PalikaDto>> GetPalikasForSelection();

        Task UpdateActiveStatusOfPalika(int palikaId);
    }
    public class PalikaService : IPalikaService
    {
        private readonly IPalikaRepository _palikaRepository;
        private readonly IGeneralUtility _generalUtility;

        public PalikaService(IPalikaRepository palikaRepository, IGeneralUtility generalUtility)
        {
            _palikaRepository = palikaRepository;
            _generalUtility = generalUtility;
        }
        public async Task<IEnumerable<PalikaDto>> GetAllActivePalikas()
        {
            return  ObjectMapper.Mapper.Map<IEnumerable<PalikaDto>>(await _palikaRepository.GetAsync(x => true));
        }

        public async Task<IEnumerable<PalikaDto>> GetPalikaByDistrictId(int districtId)
        {
            return ObjectMapper.Mapper.Map<IEnumerable<PalikaDto>>(await _palikaRepository.GetAsync(x => x.DistrictId == districtId));
        }

        public async Task<IEnumerable<PalikaUpdateActiveStatusDto>> GetAll()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<PalikaUpdateActiveStatusDto>>(await _palikaRepository.GetAllAsync());
        }

        public async Task<IEnumerable<PalikaDto>> GetPalikasForSelection()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<PalikaDto>>(await _palikaRepository.GetAsync(x => true));
        }

        public async Task UpdateActiveStatusOfPalika(int palikaId)
        {
            await _palikaRepository.UpdatePalikaActiveStatus(palikaId, _generalUtility.GetLoggedInUsername(), _generalUtility.GetCurrentNepalTime());
        }

    }
}
