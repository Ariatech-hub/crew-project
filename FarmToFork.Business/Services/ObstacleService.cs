using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Business.Utilities;
using FarmToFork.Core.Entities;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services;


public interface IObstacleService
{
    Task<IEnumerable<ObstacleDto>> GetAllActiveObstacles();
    Task<IEnumerable<ObstacleTableDto>> GetAllObstacles();
    Task<ObstacleInsertDto> Insert(ObstacleInsertDto obstacleInsertDto);
    Task Update(ObstacleUpdateDto obstacleUpdateDto);
    Task<ObstacleDeleteDto> Delete(ObstacleDeleteDto obstacleDeleteDto);

}
public class ObstacleService : IObstacleService
{
    private readonly IObstacleRepository _obstacleRepository;
    private readonly IGeneralUtility _generalUtility;

    public ObstacleService(IObstacleRepository obstacleRepository, IGeneralUtility generalUtility)
    {
        _obstacleRepository = obstacleRepository;
        _generalUtility = generalUtility;
    }
    public async Task<IEnumerable<ObstacleDto>> GetAllActiveObstacles()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<ObstacleDto>>(await _obstacleRepository.GetAsync(x => x.IsActive));
    }

    public async Task<IEnumerable<ObstacleTableDto>> GetAllObstacles()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<ObstacleTableDto>>(await _obstacleRepository.GetAllAsync());
    }

    public async Task<ObstacleInsertDto> Insert(ObstacleInsertDto obstacleInsertDto)
    {
        obstacleInsertDto.Name = obstacleInsertDto.Name.Trim();
        obstacleInsertDto.NepaliName = obstacleInsertDto.NepaliName.Trim();
        obstacleInsertDto.Code = (!string.IsNullOrEmpty(obstacleInsertDto.Code)
            ? obstacleInsertDto.Code.Trim()
            : obstacleInsertDto.Code);
        if (obstacleInsertDto.IsSale && obstacleInsertDto.IsProduction)
        {
            throw new Exception("Obstacle cannot be sale and production at the same time");
        }
        Obstacle mappedObstacle = ObjectMapper.Mapper.Map<Obstacle>(obstacleInsertDto);
        mappedObstacle.IsActive = true;
        mappedObstacle.CreatedBy = _generalUtility.GetLoggedInUsername();
        mappedObstacle.CreatedDate = _generalUtility.GetCurrentNepalTime();

        return ObjectMapper.Mapper.Map<ObstacleInsertDto>(await _obstacleRepository.AddAsync(mappedObstacle));


    }

    public async Task Update(ObstacleUpdateDto obstacleUpdateDto)
    {
        obstacleUpdateDto.Name = obstacleUpdateDto.Name.Trim();
        obstacleUpdateDto.NepaliName = obstacleUpdateDto.NepaliName.Trim();
        obstacleUpdateDto.Code = (!string.IsNullOrEmpty(obstacleUpdateDto.Code)
            ? obstacleUpdateDto.Code.Trim()
            : obstacleUpdateDto.Code);
        if (obstacleUpdateDto.IsSale && obstacleUpdateDto.IsProduction)
        {
            throw new Exception("Obstacle cannot be sale and production at the same time");
        }
        Obstacle mappedObstacle = ObjectMapper.Mapper.Map<Obstacle>(obstacleUpdateDto);
        
        mappedObstacle.ModifiedBy = _generalUtility.GetLoggedInUsername();
        mappedObstacle.ModifiedDate = _generalUtility.GetCurrentNepalTime();
        await _obstacleRepository.UpdateAsync(mappedObstacle);
    }

    public async Task<ObstacleDeleteDto> Delete(ObstacleDeleteDto obstacleDeleteDto)
    {
        Obstacle mappedObstacle = ObjectMapper.Mapper.Map<Obstacle>(obstacleDeleteDto);
        return ObjectMapper.Mapper.Map<ObstacleDeleteDto>(await _obstacleRepository.DeleteAsync(mappedObstacle));
    }
}