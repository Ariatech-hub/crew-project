using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services;

public interface IYearService
{
    Task<IEnumerable<YearDto>> GetAllActiveYears();
    Task<IEnumerable<YearTableDto>> GetAllYear();
    Task<YearInsertDto> Insert(YearInsertDto yearInsertDto);
    Task Update(YearUpdateDto yearUpdateDto);
    Task<YearDeleteDto> Delete(YearDeleteDto yearDeleteDto);
}


public class YearService : IYearService
{
    private readonly IYearRepository _yearRepository;
    private readonly IGeneralUtility _generalUtility;


    public YearService(IYearRepository yearRepository, IGeneralUtility generalUtility)
    {
        _yearRepository = yearRepository;
        _generalUtility = generalUtility;
    }

    public async Task<YearDeleteDto> Delete(YearDeleteDto yearDeleteDto)
    {
        Year mappedYear = ObjectMapper.Mapper.Map<Year>(yearDeleteDto);
        return ObjectMapper.Mapper.Map<YearDeleteDto>(await _yearRepository.DeleteAsync(mappedYear));
    }

    public async Task<IEnumerable<YearDto>> GetAllActiveYears()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<YearDto>>(await _yearRepository.GetAsync(x => x.IsActive));
    }

    public async Task<IEnumerable<YearTableDto>> GetAllYear()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<YearTableDto>>(await _yearRepository.GetAllAsync());
    }

    public async Task<YearInsertDto> Insert(YearInsertDto yearInsertDto)
    {
        yearInsertDto.Name = yearInsertDto.Name.Trim();
        yearInsertDto.NepaliName = yearInsertDto.NepaliName.Trim();
        if (DateOnly.TryParse(yearInsertDto.StartDate, out DateOnly startDateAd))
        {
            yearInsertDto.StartDateAd = startDateAd;
        }
        else
        {
            throw new Exception("Invalid Start Date");
        }
        if (DateOnly.TryParse(yearInsertDto.EndDate, out DateOnly endDateAd))
        {
            yearInsertDto.EndDateAd = endDateAd;
        }
        else
        {
            throw new Exception("Invalid End Date");
        }

        Year mappedYear = ObjectMapper.Mapper.Map<Year>(yearInsertDto);
        mappedYear.IsActive = true;
        mappedYear.CreatedBy = _generalUtility.GetLoggedInUsername();
        mappedYear.CreatedDate = _generalUtility.GetCurrentNepalTime();

        return ObjectMapper.Mapper.Map<YearInsertDto>(await _yearRepository.AddAsync(mappedYear));
    }

    public async Task Update(YearUpdateDto yearUpdateDto)
    {
        yearUpdateDto.Name = yearUpdateDto.Name.Trim();
        yearUpdateDto.NepaliName = yearUpdateDto.NepaliName.Trim();
        yearUpdateDto.StartDateAd = DateOnly.Parse(yearUpdateDto.StartDate);
        yearUpdateDto.EndDateAd = DateOnly.Parse(yearUpdateDto.EndDate);
        Year mappedYear = ObjectMapper.Mapper.Map<Year>(yearUpdateDto);
        mappedYear.ModifiedBy = _generalUtility.GetLoggedInUsername();
        mappedYear.ModifiedDate = _generalUtility.GetCurrentNepalTime();
        await _yearRepository.UpdateAsync(mappedYear);
    }
}