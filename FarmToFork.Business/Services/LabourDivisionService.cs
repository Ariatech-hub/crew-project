using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.Services
{
    public interface ILabourDivisionService
    {
        public Task<IEnumerable<LabourDivisionDto>> GetAllActiveLabourDivision();
        public Task<IEnumerable<LabourDivisionTableDto>> GetAllLabourDivision();
        Task<LabourDivisionInsertDto> Insert(LabourDivisionInsertDto labourDivisionInsertDto);
        Task Update(LabourDivisionUpdateDto labourDivisionUpdateDto);
        Task<LabourDivisionDeleteDto> Delete(LabourDivisionDeleteDto labourDivisionDeleteDto);
    }

    public class LabourDivisonService : ILabourDivisionService
    {
        private readonly ILabourDivisionRepository _labourDivisionRepository;
        private readonly IGeneralUtility _generalUtility;
        public LabourDivisonService(ILabourDivisionRepository labourDivisionRepository, IGeneralUtility generalUtility)
        {
            _labourDivisionRepository = labourDivisionRepository;
            _generalUtility = generalUtility;

        }
        public async Task<LabourDivisionDeleteDto> Delete(LabourDivisionDeleteDto labourDivisionDeleteDto)
        {
            LabourDivision mappedLabourDivision = ObjectMapper.Mapper.Map<LabourDivision>(labourDivisionDeleteDto);
            return ObjectMapper.Mapper.Map<LabourDivisionDeleteDto>(await _labourDivisionRepository.DeleteAsync(mappedLabourDivision));
        }

        public async Task<IEnumerable<LabourDivisionDto>> GetAllActiveLabourDivision()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<LabourDivisionDto>>(await _labourDivisionRepository.GetAsync(x => x.IsActive));
        }

        public async Task<IEnumerable<LabourDivisionTableDto>> GetAllLabourDivision()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<LabourDivisionTableDto>>(await _labourDivisionRepository.GetAllAsync());
        }

        public async Task<LabourDivisionInsertDto> Insert(LabourDivisionInsertDto labourDivisionInsertDto)
        {
            labourDivisionInsertDto.Name = labourDivisionInsertDto.Name.Trim();
            labourDivisionInsertDto.NepaliName = labourDivisionInsertDto.NepaliName.Trim();
            labourDivisionInsertDto.Code = (!string.IsNullOrEmpty(labourDivisionInsertDto.Code)
                ? labourDivisionInsertDto.Code.Trim()
                : labourDivisionInsertDto.Code);
            LabourDivision mappedLabourDivision = ObjectMapper.Mapper.Map<LabourDivision>(labourDivisionInsertDto);
            mappedLabourDivision.IsActive = true;
            mappedLabourDivision.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedLabourDivision.CreatedDate = _generalUtility.GetCurrentNepalTime();

            return ObjectMapper.Mapper.Map<LabourDivisionInsertDto>(await _labourDivisionRepository.AddAsync(mappedLabourDivision));
        }

        public async Task Update(LabourDivisionUpdateDto labourDivisionUpdateDto)
        {
            labourDivisionUpdateDto.Name = labourDivisionUpdateDto.Name.Trim();
            labourDivisionUpdateDto.NepaliName = labourDivisionUpdateDto.NepaliName.Trim();
            labourDivisionUpdateDto.Code = (!string.IsNullOrEmpty(labourDivisionUpdateDto.Code)
                ? labourDivisionUpdateDto.Code.Trim()
                : labourDivisionUpdateDto.Code);

            LabourDivision mappedLabourDivision = ObjectMapper.Mapper.Map<LabourDivision>(labourDivisionUpdateDto);

            mappedLabourDivision.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedLabourDivision.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            await _labourDivisionRepository.UpdateAsync(mappedLabourDivision);
        }
    }

}
