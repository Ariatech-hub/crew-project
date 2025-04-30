using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services
{

    public interface IInternetService
    {
        Task<IEnumerable<InternetTypeDto>> GetAllActiveInternetTypes();

        Task<IEnumerable<InternetUseDto>> GetAllActiveInternetUse();

        Task<InternetTypeInsertDto> InsertInternetTye(InternetTypeInsertDto internetTypeInsert);
        Task  UpdateInternetType(InternetTypeUpdateDto internetTypeUpdate);
        Task<IEnumerable<InternetTypeTableDto>> GetAllInternetTypes();

        Task<InternetTypeDeleteDto> DeleteInternetType(InternetTypeDeleteDto internetTypeDelete);

        Task<InternetUseInsertDto> InsertInternetUse(InternetUseInsertDto internetUseInsertDto);
        Task UpdateInternetUse(InternetUseUpdateDto internetUseUpdate);
        Task<InternetUseDeleteDto> DeleteInternetUse(InternetUseDeleteDto internetUseDelete);
        Task<IEnumerable<InternetUseTableDto>> GetAllInternetUse();



    }
    public class InternetService : IInternetService
    {
        private readonly IInternetRepository _internetRepository;
        private readonly IGeneralUtility _generalUtility;

        public InternetService(IInternetRepository internetRepository, IGeneralUtility generalUtility)
        {
            _internetRepository = internetRepository;
            _generalUtility = generalUtility;
        }
        public async Task<IEnumerable<InternetTypeDto>> GetAllActiveInternetTypes()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<InternetTypeDto>>(await _internetRepository.GetAsync(x => x.IsActive));
        }

        public async Task<IEnumerable<InternetUseDto>> GetAllActiveInternetUse()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<InternetUseDto>>(await _internetRepository.GetAllActiveInternetUsage());
        }

        public async Task<InternetTypeInsertDto> InsertInternetTye(InternetTypeInsertDto internetTypeInsert)
        {

            internetTypeInsert.Name = internetTypeInsert.Name.Trim();
            internetTypeInsert.NepaliName = internetTypeInsert.NepaliName.Trim();
            internetTypeInsert.Code = string.IsNullOrEmpty(internetTypeInsert.Code)
                ? internetTypeInsert.Code
                : internetTypeInsert.Code.Trim();

            InternetType mappedInternetType = ObjectMapper.Mapper.Map<InternetType>(internetTypeInsert);
            mappedInternetType.IsActive = true;
            mappedInternetType.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedInternetType.CreatedDate = _generalUtility.GetCurrentNepalTime();
            return ObjectMapper.Mapper.Map<InternetTypeInsertDto>(await _internetRepository.AddAsync(mappedInternetType));
        }

        public async Task UpdateInternetType(InternetTypeUpdateDto internetTypeUpdate)
        {
            internetTypeUpdate.Name = internetTypeUpdate.Name.Trim();
            internetTypeUpdate.NepaliName = internetTypeUpdate.NepaliName.Trim();
            internetTypeUpdate.Code = string.IsNullOrEmpty(internetTypeUpdate.Code)
                ? internetTypeUpdate.Code
                : internetTypeUpdate.Code.Trim();

            InternetType mappedInternetType = ObjectMapper.Mapper.Map<InternetType>(internetTypeUpdate);
            
            mappedInternetType.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedInternetType.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            await _internetRepository.UpdateAsync(mappedInternetType);
        }

        public async Task<IEnumerable<InternetTypeTableDto>> GetAllInternetTypes()
        {

           return ObjectMapper.Mapper.Map<IEnumerable<InternetTypeTableDto>>(await _internetRepository.GetAllAsync());
           
        }

        public async Task<InternetTypeDeleteDto> DeleteInternetType(InternetTypeDeleteDto internetTypeDelete)
        {
            return ObjectMapper.Mapper.Map<InternetTypeDeleteDto>(await _internetRepository.DeleteAsync(ObjectMapper.Mapper.Map<InternetType>(internetTypeDelete)));
        }

        public async Task<InternetUseInsertDto> InsertInternetUse(InternetUseInsertDto internetUseInsertDto)
        {
            internetUseInsertDto.Name = internetUseInsertDto.Name.Trim();
            internetUseInsertDto.NepaliName = internetUseInsertDto.NepaliName.Trim();
            internetUseInsertDto.Code = !string.IsNullOrEmpty( internetUseInsertDto.Code) ? internetUseInsertDto.Code.Trim() : internetUseInsertDto.Code;

            InternetUse internetUse = ObjectMapper.Mapper.Map<InternetUse>(internetUseInsertDto);
            internetUse.IsActive = true;
            internetUse.CreatedBy = _generalUtility.GetLoggedInUsername();
            internetUse.CreatedDate = _generalUtility.GetCurrentNepalTime();
            return ObjectMapper.Mapper.Map<InternetUseInsertDto>(await _internetRepository.InsertInternetUse(internetUse));

        }

        public async Task UpdateInternetUse(InternetUseUpdateDto internetUseUpdate)
        {
            internetUseUpdate.Name = internetUseUpdate.Name;
            internetUseUpdate.NepaliName = internetUseUpdate.NepaliName;
            internetUseUpdate.Code = internetUseUpdate.Code;
            InternetUse internetUse = ObjectMapper.Mapper.Map<InternetUse>(internetUseUpdate);
            await _internetRepository.UpdateInternetUse(internetUse);
            
        }

        public async Task<InternetUseDeleteDto> DeleteInternetUse(InternetUseDeleteDto internetUseDelete)
        {
            InternetUse internetUse = ObjectMapper.Mapper.Map<InternetUse>(internetUseDelete);
            return ObjectMapper.Mapper.Map<InternetUseDeleteDto>(await _internetRepository.DeleteInternetUse(internetUse));


        }

        public async Task<IEnumerable<InternetUseTableDto>> GetAllInternetUse()
        {
            var data = await _internetRepository.GetAllInternetUses();
            var dto = ObjectMapper.Mapper.Map<IEnumerable<InternetUseTableDto>>(data);
            return dto;
        }
    }
}
