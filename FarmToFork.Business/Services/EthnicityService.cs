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

    public interface IEthnicityService
    {
        Task<EthnicityInsertDto> Insert(EthnicityInsertDto ethnicityInsertDto);
        Task Update(EthnicityUpdateDto ethnicityUpdateDto);
        Task<IEnumerable<EthnicityDto>> GetAll();
        Task<EthnicityDto> GetEthnicityById(int id);
        Task<EthnicityDeleteDto> Delete(EthnicityDeleteDto ethnicityDeleteDto);
    }

    public class EthnicityService : IEthnicityService
    {
        private readonly IEthnicityRepository _ethnicityRepository;
        private readonly IGeneralUtility _generalUtility;

        public EthnicityService(IEthnicityRepository ethnicityRepository, IGeneralUtility generalUtility)
        {
            _ethnicityRepository = ethnicityRepository;
            _generalUtility = generalUtility;
        }
        public async Task<EthnicityInsertDto> Insert(EthnicityInsertDto ethnicityInsertDto)
        {
            ethnicityInsertDto.Name = ethnicityInsertDto.Name.Trim();
            ethnicityInsertDto.NepaliName = ethnicityInsertDto.NepaliName.Trim();
            ethnicityInsertDto.Code = ethnicityInsertDto.Code?.Trim();
            Ethnicity mappedEthnicity = ObjectMapper.Mapper.Map<Ethnicity>(ethnicityInsertDto);
            mappedEthnicity.IsActive = true;
            mappedEthnicity.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedEthnicity.CreatedDate = _generalUtility.GetCurrentNepalTime();
            return ObjectMapper.Mapper.Map<EthnicityInsertDto>(await _ethnicityRepository.AddAsync(mappedEthnicity));
        }

        public async Task Update(EthnicityUpdateDto ethnicityUpdateDto)
        {
            ethnicityUpdateDto.Name = ethnicityUpdateDto.Name.Trim();
            ethnicityUpdateDto.NepaliName = ethnicityUpdateDto.NepaliName.Trim();
            ethnicityUpdateDto.Code = ethnicityUpdateDto.Code?.Trim();
            Ethnicity ethnicity = ObjectMapper.Mapper.Map<Ethnicity>(ethnicityUpdateDto);
            await _ethnicityRepository.UpdateAsync(ethnicity);
            
        }

        public async Task<IEnumerable<EthnicityDto>> GetAll()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<EthnicityDto>>(await _ethnicityRepository.GetAllAsync());
        }

        public async Task<EthnicityDto> GetEthnicityById(int id)
        {
            return ObjectMapper.Mapper.Map<EthnicityDto>(await _ethnicityRepository.GetByIdAsync(id));
        }

        public async Task<EthnicityDeleteDto> Delete(EthnicityDeleteDto ethnicityDeleteDto)
        {
            Ethnicity mappedEthnicity = ObjectMapper.Mapper.Map<Ethnicity>(ethnicityDeleteDto);
            return ObjectMapper.Mapper.Map<EthnicityDeleteDto>(await _ethnicityRepository.DeleteAsync(mappedEthnicity));
        }
    }
}
