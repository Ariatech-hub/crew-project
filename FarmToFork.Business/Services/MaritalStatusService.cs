
using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services;

public interface IMaritalStatusService
{
Task<IEnumerable<MaritalStatusDto>> GetAllMaritalStatus();
Task<MaritalStatusInsertDto> Insert(MaritalStatusInsertDto maritalStatusInsertDto);
Task Update(MaritalStatusUpdateDto maritalStatusUpdateDto);
Task<MaritalStatusDeleteDto> Delete(MaritalStatusDeleteDto maritalStatusDeleteDto);
}

public class MaritalStatusService : IMaritalStatusService
{
    private readonly IMaritalStatusRepository _maritalStatusRepository;
    private readonly IGeneralUtility _generalUtility;
    public MaritalStatusService(IMaritalStatusRepository maritalStatusRepository, IGeneralUtility generalUtility)
    {
        _maritalStatusRepository = maritalStatusRepository;
        _generalUtility = generalUtility;
    }
    public async Task<MaritalStatusDeleteDto> Delete(MaritalStatusDeleteDto maritalStatusDeleteDto)
    {
        MaritalStatus mappedMaritalStatus = ObjectMapper.Mapper.Map<MaritalStatus>(maritalStatusDeleteDto);
        return ObjectMapper.Mapper.Map<MaritalStatusDeleteDto>(await _maritalStatusRepository.DeleteAsync(mappedMaritalStatus));
    }

    public async Task<IEnumerable<MaritalStatusDto>> GetAllMaritalStatus()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<MaritalStatusDto>>(await _maritalStatusRepository.GetAllAsync());
    }

    public async Task<MaritalStatusInsertDto> Insert(MaritalStatusInsertDto maritalStatusInsertDto)
    {
        maritalStatusInsertDto.Name = maritalStatusInsertDto.Name.Trim();
        maritalStatusInsertDto.NepaliName = maritalStatusInsertDto.NepaliName.Trim();

        MaritalStatus mappedMaritalStatus = ObjectMapper.Mapper.Map<MaritalStatus>(maritalStatusInsertDto);
        mappedMaritalStatus.IsActive = true;
        mappedMaritalStatus.CreatedBy = _generalUtility.GetLoggedInUsername();
        mappedMaritalStatus.CreatedDate = _generalUtility.GetCurrentNepalTime();

        return ObjectMapper.Mapper.Map<MaritalStatusInsertDto>(await _maritalStatusRepository.AddAsync(mappedMaritalStatus));
    }

    public async Task Update(MaritalStatusUpdateDto maritalStatusUpdateDto)
    {
        maritalStatusUpdateDto.Name = maritalStatusUpdateDto.Name.Trim();
        maritalStatusUpdateDto.NepaliName = maritalStatusUpdateDto.NepaliName.Trim();

        MaritalStatus mappedMaritalStatus = ObjectMapper.Mapper.Map<MaritalStatus>(maritalStatusUpdateDto);
        mappedMaritalStatus.ModifiedBy = _generalUtility.GetLoggedInUsername();
        mappedMaritalStatus.ModifiedDate = _generalUtility.GetCurrentNepalTime();
        await _maritalStatusRepository.UpdateAsync(mappedMaritalStatus);
    }
}
