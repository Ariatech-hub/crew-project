using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services 
{ 

public interface ILocationService
{
Task<IEnumerable<LocationDto>> GetAllLocation();
Task<LocationInsertDto> Insert(LocationInsertDto locationInsertDto);
Task Update(LocationUpdateDto locationUpdateDto);
Task<LocationDeleteDto> Delete(LocationDeleteDto locationDeleteDto);

}
public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IGeneralUtility _generalUtility;

    public LocationService(ILocationRepository locationRepository, IGeneralUtility generalUtility)
    {
        _locationRepository = locationRepository;
        _generalUtility = generalUtility;
    }

    public async Task<LocationDeleteDto> Delete(LocationDeleteDto locationDeleteDto)
    {
        Location mappedLocation = ObjectMapper.Mapper.Map<Location>(locationDeleteDto);
        return ObjectMapper.Mapper.Map<LocationDeleteDto>(await _locationRepository.DeleteAsync(mappedLocation));
    }

    public Task<ObstacleDeleteDto> Delete(ObstacleDeleteDto obstacleDeleteDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<LocationDto>> GetAllLocation()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<LocationDto>>(await _locationRepository.GetAllAsync());
    }

    public async Task<LocationInsertDto> Insert(LocationInsertDto locationInsertDto)
    {
        locationInsertDto.Name = locationInsertDto.Name.Trim();
        locationInsertDto.NepaliName = locationInsertDto.NepaliName.Trim();
        locationInsertDto.Code = (!string.IsNullOrEmpty(locationInsertDto.Code)
            ? locationInsertDto.Code.Trim() : locationInsertDto.Code);

        Location mappedLocation = ObjectMapper.Mapper.Map<Location>(locationInsertDto);
        mappedLocation.IsActive = true;
        mappedLocation.CreatedBy = _generalUtility.GetLoggedInUsername();
        mappedLocation.CreatedDate = _generalUtility.GetCurrentNepalTime();

        return ObjectMapper.Mapper.Map<LocationInsertDto>(await _locationRepository.AddAsync(mappedLocation));
    }

    public async Task Update(LocationUpdateDto locationUpdateDto)
    {
        locationUpdateDto.Name = locationUpdateDto.Name.Trim();
        locationUpdateDto.NepaliName = locationUpdateDto.NepaliName.Trim();
        locationUpdateDto.Code = (!string.IsNullOrEmpty(locationUpdateDto.Code)
            ? locationUpdateDto.Code.Trim()
            : locationUpdateDto.Code);
        
        Location mappedLocation = ObjectMapper.Mapper.Map<Location>(locationUpdateDto);
        mappedLocation.ModifiedBy = _generalUtility.GetLoggedInUsername();
        mappedLocation.ModifiedDate = _generalUtility.GetCurrentNepalTime();
        await _locationRepository.UpdateAsync(mappedLocation);
    }
    }
}

