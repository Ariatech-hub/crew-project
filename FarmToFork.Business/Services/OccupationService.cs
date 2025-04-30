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
    public interface IOccupationService
    {
        Task<IEnumerable<OccupationDto>> GetAllOccupations();
        Task<IEnumerable<OccupationDto>> GetActiveOccupations();
        Task<OccupationInsertDto> Insert(OccupationInsertDto occupationInsertDto);
        Task Update(OccupationUpdateDto occupationUpdateDto);
        Task<OccupationDeleteDto> Delete(OccupationDeleteDto occupationDeleteDto);
    }
    public class OccupationService : IOccupationService
    {
        private readonly IOccupationRepository _occupationRepository;
        private readonly IGeneralUtility _generalUtility;
        

        public OccupationService(IOccupationRepository occupationRepository, IGeneralUtility generalUtility)
        {
            _occupationRepository = occupationRepository;
            _generalUtility = generalUtility;
            
        }
        public async Task<IEnumerable<OccupationDto>> GetAllOccupations()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<OccupationDto>>(await _occupationRepository.GetAllAsync());
        }

        public async Task<IEnumerable<OccupationDto>> GetActiveOccupations()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<OccupationDto>>(await _occupationRepository.GetAsync(x => x.IsActive));
        }



        public async Task Update(OccupationUpdateDto occupationUpdateDto)
        {
            occupationUpdateDto.Name = occupationUpdateDto.Name.Trim();
            occupationUpdateDto.NepaliName = occupationUpdateDto.NepaliName.Trim();
            occupationUpdateDto.Code = (!string.IsNullOrEmpty(occupationUpdateDto.Code)
                ? occupationUpdateDto.Code.Trim()
                : occupationUpdateDto.Code);
            Occupation mappedOccupation = ObjectMapper.Mapper.Map<Occupation>(occupationUpdateDto);

            mappedOccupation.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedOccupation.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            await _occupationRepository.UpdateAsync(mappedOccupation);
        }

        public async Task<OccupationInsertDto> Insert(OccupationInsertDto occupationInsertDto)
        {
            occupationInsertDto.Name = occupationInsertDto.Name.Trim();
            occupationInsertDto.NepaliName = occupationInsertDto.NepaliName.Trim();
            occupationInsertDto.Code = (!string.IsNullOrEmpty(occupationInsertDto.Code)
                ? occupationInsertDto.Code.Trim()
                : occupationInsertDto.Code);
            Occupation mappedOccupation = ObjectMapper.Mapper.Map<Occupation>(occupationInsertDto);
            mappedOccupation.IsActive = true;
            mappedOccupation.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedOccupation.CreatedDate = _generalUtility.GetCurrentNepalTime();

            return ObjectMapper.Mapper.Map<OccupationInsertDto>(await _occupationRepository.AddAsync(mappedOccupation));
        }

        public async Task<OccupationDeleteDto> Delete(OccupationDeleteDto occupationDeleteDto)
        {
            Occupation mappedOccupation = ObjectMapper.Mapper.Map<Occupation>(occupationDeleteDto);
            return ObjectMapper.Mapper.Map<OccupationDeleteDto>(await _occupationRepository.DeleteAsync(mappedOccupation));
        }
    }
}
