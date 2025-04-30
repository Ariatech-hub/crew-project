using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{
    public interface IParticipantOptionService
    {
        Task<IEnumerable<ParticipantOptionDto>> GetAllActiveParticipantOptions();
        Task<IEnumerable<ParticipantOptionTableDto>> GetAllParticipantOption();
        Task<ParticipantOptionInsertDto> Insert(ParticipantOptionInsertDto participantOptionInsertDto);
        Task Update(ParticipantOptionUpdateDto participantOptionUpdateDto);
        Task<ParticipantOptionDeleteDto> Delete(ParticipantOptionDeleteDto participantOptionDeleteDto);
    }
    public class ParticipantOptionService : IParticipantOptionService
    {
        private readonly IParticipationOptionRepository _participationOptionRepository;
        private readonly IGeneralUtility _generalUtility;

        public ParticipantOptionService(IParticipationOptionRepository participationOptionRepository, IGeneralUtility generalUtility)
        {
            _participationOptionRepository = participationOptionRepository;
            _generalUtility = generalUtility;
        }
        public async Task<IEnumerable<ParticipantOptionDto>> GetAllActiveParticipantOptions()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<ParticipantOptionDto>>(await _participationOptionRepository.GetAsync(x => x.IsActive));
        }

        public async Task<IEnumerable<ParticipantOptionTableDto>> GetAllParticipantOption()
        {
            var data = await _participationOptionRepository.GetAllAsync();
            return ObjectMapper.Mapper.Map<IEnumerable<ParticipantOptionTableDto>>(data);

        }

        public async Task<ParticipantOptionDeleteDto> Delete(ParticipantOptionDeleteDto ParticipantOptionDeleteDto)
        {
            ParticipationOption mappedParticipantOption = ObjectMapper.Mapper.Map<ParticipationOption>(ParticipantOptionDeleteDto);
            return ObjectMapper.Mapper.Map<ParticipantOptionDeleteDto>(await _participationOptionRepository.DeleteAsync(mappedParticipantOption));
        }

        public async Task<ParticipantOptionInsertDto> Insert(ParticipantOptionInsertDto participantOptionInsertDto)
        {
            participantOptionInsertDto.Name = participantOptionInsertDto.Name.Trim();
            participantOptionInsertDto.NepaliName = participantOptionInsertDto.NepaliName.Trim();
            ParticipationOption mappedParticipantOption = ObjectMapper.Mapper.Map<ParticipationOption>(participantOptionInsertDto);
            mappedParticipantOption.IsActive = true;
            mappedParticipantOption.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedParticipantOption.CreatedDate = _generalUtility.GetCurrentNepalTime();

            return ObjectMapper.Mapper.Map<ParticipantOptionInsertDto>(await _participationOptionRepository.AddAsync(mappedParticipantOption));


        }
        public async Task Update(ParticipantOptionUpdateDto ParticipantOptionUpdateDto)
        {
            ParticipantOptionUpdateDto.Name = ParticipantOptionUpdateDto.Name.Trim();
            ParticipantOptionUpdateDto.NepaliName = ParticipantOptionUpdateDto.NepaliName.Trim();
            ParticipationOption mappedParticipantOption = ObjectMapper.Mapper.Map<ParticipationOption>(ParticipantOptionUpdateDto);
            mappedParticipantOption.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedParticipantOption.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            await _participationOptionRepository.UpdateAsync(mappedParticipantOption);
        }
    }

}

