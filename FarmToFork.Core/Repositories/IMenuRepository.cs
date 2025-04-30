namespace FarmToFork.Core.Repositories;
public interface IMenuRepository
{
    Task<IEnumerable<Menu>> GetMenusByUser(string username);
    Task<IEnumerable<Menu>> GetAllMenus();

    Task<bool> HasAccessForMenu(int menuCode, string username);

    Task<IEnumerable<Menu>> GetMenusByAccessLevelForAssociation(int accessLevelId);

    Task UpdateMenusForAccessLevel(int Id, int[] menuIds);

    Task UpdateMenusForUser(string userId, int[] menuIds);

}

