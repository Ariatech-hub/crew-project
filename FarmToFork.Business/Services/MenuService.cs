using FarmToFork.Business.DTOs;
using FarmToFork.Business.Mapper;
using FarmToFork.Core.Repositories;

namespace FarmToFork.Business.Services;

public interface IMenuService
{
    Task<IEnumerable<MenuDto>> GetAllMenus();
    Task<IEnumerable<MenuDto>> GetMenusByUser(string username);
    Task<bool> HasAccessForMenu(int menuCode, string username);
    Task<IEnumerable<MenuDto>> GetMenusByAccessLevelForAssociation(int accessLevelId);
    Task UpdateMenusForAccessLevel(UpdateMenusForAccessLevel updateData);

    Task UpdateMenusForUser(UpdateMenusForUser updateData);
}

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }
    public async Task<IEnumerable<MenuDto>> GetAllMenus()
    {
        return ObjectMapper.Mapper.Map<IEnumerable<MenuDto>>(await _menuRepository.GetAllMenus());
    }

    public async Task<IEnumerable<MenuDto>> GetMenusByUser(string username)
    {
        return ObjectMapper.Mapper.Map<IEnumerable<MenuDto>>(await _menuRepository.GetMenusByUser(username));
    }

    public async Task<bool> HasAccessForMenu(int menuCode, string username)
    {
        return await _menuRepository.HasAccessForMenu(menuCode, username);
    }

    public async Task<IEnumerable<MenuDto>> GetMenusByAccessLevelForAssociation(int accessLevelId)
    {
        return ObjectMapper.Mapper.Map<IEnumerable<MenuDto>>(await _menuRepository.GetMenusByAccessLevelForAssociation(accessLevelId));
    }

    public async Task UpdateMenusForAccessLevel(UpdateMenusForAccessLevel updateData)
    {
        await _menuRepository.UpdateMenusForAccessLevel(updateData.Id, updateData.MenuIds ?? Array.Empty<int>());
    }

    public async Task UpdateMenusForUser(UpdateMenusForUser updateData)
    {
        await _menuRepository.UpdateMenusForUser(updateData.UserId, updateData.MenuIds ?? Array.Empty<int>());
    }
}