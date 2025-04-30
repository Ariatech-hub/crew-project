using AutoMapper.Internal.Mappers;
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
    public interface IIncomeSourceService
    {
        public Task<IEnumerable<IncomeSourceDto>> GetAllActiveIncomeSource();
        public Task<IEnumerable<IncomeSourceTableDto>> GetAllIncomeSource();
        Task<IncomeSourceInsertDto> Insert(IncomeSourceInsertDto incomeSourceInsertDto);
        Task Update(IncomeSourceUpdateDto incomeSourceUpdateDto);
        Task<IncomeSourceDeleteDto> Delete(IncomeSourceDeleteDto incomeSourceDeleteDto);
    }

    public class IncomeSourceService : IIncomeSourceService
    {
        private readonly IIncomeSourceRepository _incomeSourceRepository;
        private readonly IGeneralUtility _generalUtility;
        public IncomeSourceService(IIncomeSourceRepository incomeSourceRepository, IGeneralUtility generalUtility)
        {
            _incomeSourceRepository = incomeSourceRepository;
            _generalUtility = generalUtility;
                
        }
        public async Task<IEnumerable<IncomeSourceDto>> GetAllActiveIncomeSource()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<IncomeSourceDto>>(await _incomeSourceRepository.GetAsync(x => x.IsActive));
        }

        public async Task<IEnumerable<IncomeSourceTableDto>> GetAllIncomeSource()
        {
            var data = await _incomeSourceRepository.GetAllAsync();
            return ObjectMapper.Mapper.Map<IEnumerable<IncomeSourceTableDto>>(data);

        }

        public async Task<IncomeSourceDeleteDto> Delete(IncomeSourceDeleteDto incomeSourceDeleteDto)
        {
            IncomeSource mappedIncomeSource = ObjectMapper.Mapper.Map<IncomeSource>(incomeSourceDeleteDto);
            return ObjectMapper.Mapper.Map<IncomeSourceDeleteDto>(await _incomeSourceRepository.DeleteAsync(mappedIncomeSource));
        }

        public async Task<IncomeSourceInsertDto> Insert(IncomeSourceInsertDto incomeSourceInsertDto)
        {
            incomeSourceInsertDto.Name = incomeSourceInsertDto.Name.Trim();
            incomeSourceInsertDto.NepaliName = incomeSourceInsertDto.NepaliName.Trim();
            incomeSourceInsertDto.Code = (!string.IsNullOrEmpty(incomeSourceInsertDto.Code)
                ? incomeSourceInsertDto.Code.Trim()
                : incomeSourceInsertDto.Code);
            IncomeSource mappedIncomeSource = ObjectMapper.Mapper.Map<IncomeSource>(incomeSourceInsertDto);
            mappedIncomeSource.IsActive = true;
            mappedIncomeSource.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedIncomeSource.CreatedDate = _generalUtility.GetCurrentNepalTime();

            return ObjectMapper.Mapper.Map<IncomeSourceInsertDto>(await _incomeSourceRepository.AddAsync(mappedIncomeSource));


        }
        public async Task Update(IncomeSourceUpdateDto incomeSourceUpdateDto)
        {
            incomeSourceUpdateDto.Name = incomeSourceUpdateDto.Name.Trim();
            incomeSourceUpdateDto.NepaliName = incomeSourceUpdateDto.NepaliName.Trim();
            incomeSourceUpdateDto.Code = (!string.IsNullOrEmpty(incomeSourceUpdateDto.Code)
                ? incomeSourceUpdateDto.Code.Trim()
                : incomeSourceUpdateDto.Code);

            IncomeSource mappedIncomeSource = ObjectMapper.Mapper.Map<IncomeSource>(incomeSourceUpdateDto);

            mappedIncomeSource.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedIncomeSource.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            await _incomeSourceRepository.UpdateAsync(mappedIncomeSource);
        }

    }

}
