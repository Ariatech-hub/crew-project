using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;
using FarmToFork.Core.Repositories.Base;

namespace FarmToFork.Business.Services
{

    public interface IEducationLevelService
    {
        Task<EducationLevelInsertDto> Insert(EducationLevelInsertDto educationLevelInsertDto);
        Task Update(EducationLevelUpdateDto educationLevelUpdateDto);
        Task<IEnumerable<EducationLevelDto>> GetAll();
        Task<IEnumerable<EducationLevelDto>> GetAllActiveEducationLevels();
        Task<EducationLevelDto> GetById(int id);
        Task<EducationLevelDeleteDto> Delete(EducationLevelDeleteDto educationLevelDeleteDto);
        



       
    }

    public class EducationLevelService : IEducationLevelService
    {
        private readonly IEducationLevelRepository _educationLevelRepository;
        private readonly IGeneralUtility _generalUtility;

        public EducationLevelService(IEducationLevelRepository educationLevelRepository,
            IGeneralUtility generalUtility)
        {
            _educationLevelRepository = educationLevelRepository;
            _generalUtility = generalUtility;
        }


        public async Task<EducationLevelInsertDto> Insert(EducationLevelInsertDto educationLevelInsertDto)
        {

            educationLevelInsertDto.Name = educationLevelInsertDto.Name.Trim();
            educationLevelInsertDto.NepaliName = educationLevelInsertDto.NepaliName.Trim();
            educationLevelInsertDto.Code = string.IsNullOrEmpty(educationLevelInsertDto.Code)
                ? null
                : educationLevelInsertDto.Code.Trim();
            EducationLevel mappedEducationLevel = ObjectMapper.Mapper.Map<EducationLevel>(educationLevelInsertDto);
            mappedEducationLevel.IsActive = true;
            mappedEducationLevel.CreatedBy = _generalUtility.GetLoggedInUsername();
            mappedEducationLevel.CreatedDate = _generalUtility.GetCurrentNepalTime();
            return ObjectMapper.Mapper.Map<EducationLevelInsertDto>(
                await _educationLevelRepository.AddAsync(mappedEducationLevel));
        }

        public async Task Update(EducationLevelUpdateDto educationLevelUpdateDto)
        {

            educationLevelUpdateDto.Name = educationLevelUpdateDto.Name.Trim();
            educationLevelUpdateDto.NepaliName = educationLevelUpdateDto.NepaliName.Trim();
            educationLevelUpdateDto.Code = string.IsNullOrEmpty(educationLevelUpdateDto.Code)
                ? null
                : educationLevelUpdateDto.Code.Trim();

            EducationLevel mappedEducationLevel = ObjectMapper.Mapper.Map<EducationLevel>(educationLevelUpdateDto);
            mappedEducationLevel.ModifiedBy = _generalUtility.GetLoggedInUsername();
            mappedEducationLevel.ModifiedDate = _generalUtility.GetCurrentNepalTime();
            await _educationLevelRepository.UpdateAsync(mappedEducationLevel);
        }

        public async Task<IEnumerable<EducationLevelDto>> GetAll()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<EducationLevelDto>>(
                await _educationLevelRepository.GetAllAsync());
        }

        public async Task<IEnumerable<EducationLevelDto>> GetAllActiveEducationLevels()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<EducationLevelDto>>(await _educationLevelRepository.GetAsync(x=>x.IsActive));
        }

        public async Task<EducationLevelDto> GetById(int id)
        {
            return ObjectMapper.Mapper.Map<EducationLevelDto>(await _educationLevelRepository.GetByIdAsync(id));
        }

        public async Task<EducationLevelDeleteDto> Delete(EducationLevelDeleteDto educationLevelDeleteDto)
        {
            EducationLevel mappedEdcEducationLevel = ObjectMapper.Mapper.Map<EducationLevel>(educationLevelDeleteDto);
            return ObjectMapper.Mapper.Map<EducationLevelDeleteDto>(await _educationLevelRepository.DeleteAsync(mappedEdcEducationLevel));

        }
    }
}
