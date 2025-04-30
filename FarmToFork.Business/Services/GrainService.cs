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
    public interface IGrainService
    {
        Task<IEnumerable<GrainDto>> GetAllActiveGrains();

        Task<GrainInsertDto> InsertGrain(GrainInsertDto grainInsertDto);
        Task UpdateGrain(GrainUpdateDto grainUpdate);
        Task<GrainDeleteDto> DeleteGrain(GrainDeleteDto grainDelete);
        Task<IEnumerable<GrainTableDto>> GetAllGrains();

        Task<IEnumerable<GrainCycleDto>> GetAllActiveGrainCycles();

        Task<GrainCycleInsertDto> InsertGrainCycle(GrainCycleInsertDto grainCycleInsertDto);

        Task GrainUpdateCycle(GrainCycleUpdateDto grainCycleUpdateDto);
        Task<IEnumerable<GrainCycleTableDto>> GetAllGrainCycles();
        Task<GrainCycleGetByIdDto> GetGrainCycleById(int id);
        Task<GrainCycleDeleteDto> DeleteGrainCycle(GrainCycleDeleteDto grainCycleDeleteDto);
        Task<GrainCycleUpdateStatusDto> GrainCycleActiveStatusChanged(GrainCycleUpdateStatusDto grainCycleDto);
        Task<IEnumerable<GrainCycleTableDto>> GetGrainCyclesByGrainId(int id);
    }
    public class GrainService : IGrainService
    {
        private readonly IGrainRepository _grainRepository;
        private readonly IGrainCycleRepository _grainCycleRepository;
        private readonly IGeneralUtility _generalUtility;

        public GrainService(IGrainRepository grainRepository, IGrainCycleRepository grainCycleRepository, IGeneralUtility generalUtility)
        {
            _grainRepository = grainRepository;
            _grainCycleRepository = grainCycleRepository;
            _generalUtility = generalUtility;
        }
        public async Task<IEnumerable<GrainDto>> GetAllActiveGrains()
        {

            return ObjectMapper.Mapper.Map<IEnumerable<GrainDto>>(await _grainRepository.GetAsync(x => x.IsActive));
        }

        public async Task<GrainCycleGetByIdDto> GetGrainCycleById(int id)
        {
            GrainCycle grainCycle = await _grainCycleRepository.GetByIdAsync(id);
            GrainCycleGetByIdDto mappedGrainCycle = ObjectMapper.Mapper.Map<GrainCycleGetByIdDto>(grainCycle);
            return mappedGrainCycle;
        }

        public async Task<GrainInsertDto> InsertGrain(GrainInsertDto grainInsertDto)
        {

            grainInsertDto.Name = grainInsertDto.Name.Trim();
            grainInsertDto.NepaliName = grainInsertDto.NepaliName.Trim();
            grainInsertDto.Code = string.IsNullOrEmpty(grainInsertDto.Code)
                ? grainInsertDto.Code
                : grainInsertDto.Code.Trim();
            Grain mappedGrain = ObjectMapper.Mapper.Map<Grain>(grainInsertDto);
            mappedGrain.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedGrain.CreatedDate = _generalUtility.GetCurrentNepalTime();
            mappedGrain.IsActive = true;

            return ObjectMapper.Mapper.Map<GrainInsertDto>(await _grainRepository.AddAsync(mappedGrain));
        }

        public async Task UpdateGrain(GrainUpdateDto grainUpdate)
        {
            grainUpdate.Name = grainUpdate.Name.Trim();
            grainUpdate.NepaliName = grainUpdate.NepaliName.Trim();
            grainUpdate.Code = string.IsNullOrEmpty(grainUpdate.Code)
                ? grainUpdate.Code
                : grainUpdate.Code.Trim();
            Grain mappedGrain = ObjectMapper.Mapper.Map<Grain>(grainUpdate);
            mappedGrain.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            mappedGrain.ModifiedBy = _generalUtility.GetLoggedInUsername();
            await _grainRepository.UpdateAsync(mappedGrain);

        }

        public async Task<GrainDeleteDto> DeleteGrain(GrainDeleteDto grainDelete)
        {
            return ObjectMapper.Mapper.Map<GrainDeleteDto>(await _grainRepository.DeleteAsync(ObjectMapper.Mapper.Map<Grain>(grainDelete)));
        }

        public async Task<IEnumerable<GrainTableDto>> GetAllGrains()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<GrainTableDto>>(await _grainRepository.GetAllAsync());
        }

        public async Task<IEnumerable<GrainCycleDto>> GetAllActiveGrainCycles()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<GrainCycleDto>>(await _grainCycleRepository.GetAsync(x => x.IsActive));
        }

        public async Task<GrainCycleInsertDto> InsertGrainCycle(GrainCycleInsertDto grainCycleInsertDto)
        {
            grainCycleInsertDto.Name = grainCycleInsertDto.Name.Trim();
            grainCycleInsertDto.NepaliName = grainCycleInsertDto.NepaliName.Trim();
            GrainCycle mappedGrainCycle = ObjectMapper.Mapper.Map<GrainCycle>(grainCycleInsertDto);
            mappedGrainCycle.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedGrainCycle.CreatedDate = _generalUtility.GetCurrentNepalTime();
            mappedGrainCycle.IsActive = true;

            return ObjectMapper.Mapper.Map<GrainCycleInsertDto>(await _grainCycleRepository.AddAsync(mappedGrainCycle));
        }

        public async Task GrainUpdateCycle(GrainCycleUpdateDto grainCycleUpdateDto)
        {

            grainCycleUpdateDto.Name = grainCycleUpdateDto.Name.Trim();
            grainCycleUpdateDto.NepaliName = grainCycleUpdateDto.NepaliName.Trim();
            GrainCycle mappedGrainCycle = ObjectMapper.Mapper.Map<GrainCycle>(grainCycleUpdateDto);
            mappedGrainCycle.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            mappedGrainCycle.ModifiedBy = _generalUtility.GetLoggedInUsername();
            await _grainCycleRepository.UpdateAsync(mappedGrainCycle);
        }

        public async Task<IEnumerable<GrainCycleTableDto>> GetAllGrainCycles()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<GrainCycleTableDto>>(await _grainCycleRepository.GetAllAsync());
        }

        public async Task<GrainCycleDeleteDto> DeleteGrainCycle(GrainCycleDeleteDto grainCycleDeleteDto)
        {
            GrainCycle mappedGrainCycle = ObjectMapper.Mapper.Map<GrainCycle>(grainCycleDeleteDto);
            return ObjectMapper.Mapper.Map<GrainCycleDeleteDto>(await _grainCycleRepository.DeleteAsync(mappedGrainCycle));
        }

        public async Task<IEnumerable<GrainCycleTableDto>> GetGrainCyclesByGrainId(int id)
        {
            IEnumerable<GrainCycle> grainCycles = await _grainCycleRepository.GetGrainCycleByGrainId(id);
            IEnumerable<GrainCycleTableDto> mappedGrainCycles = ObjectMapper.Mapper.Map<IEnumerable<GrainCycleTableDto>>(grainCycles);
            return mappedGrainCycles;
        }
        public async Task<GrainCycleUpdateStatusDto> GrainCycleActiveStatusChanged(GrainCycleUpdateStatusDto grainCycleDto)
        {
            GrainCycle mappedGrainCycle = ObjectMapper.Mapper.Map<GrainCycle>(grainCycleDto);
            mappedGrainCycle.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedGrainCycle.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            GrainCycle grainCycle = await _grainCycleRepository.GrainCycleActiveStatusChanged(mappedGrainCycle);
            return ObjectMapper.Mapper.Map<GrainCycleUpdateStatusDto>(grainCycle);
        }
    }
}
