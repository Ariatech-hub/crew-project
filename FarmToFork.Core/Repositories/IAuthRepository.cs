namespace FarmToFork.Core.Repositories;

public interface IAuthRepository
{
    Task<ApplicationUser> Register(string username, string password, string confirmPassword, string role);
    Task<string> GetRoleByUser(ApplicationUser user);
    Task<ApplicationUser> GetUserByUsername(string username);
    Task<bool> Login(string username, string password);
    Task Logout();
    Task CreateRole(string role);
    Task<bool> CheckIfUsernameExists(string username);
    Task<IdentityResult> RegisterWithoutPassword(ApplicationUser user, string role);
    Task<bool> UpdateUser(ApplicationUser user);
    Task<ApplicationUser> GetUserById(string userId);
    Task ResetPassword(string username, string password);

    Task UnRegisterUser(string userName);

    Task ChangePassword(string username, string oldPassword, string password);

    IQueryable<ApplicationUser> GetAllUsers();

    Task<IEnumerable<ApplicationUser>> GetUsers();

    IQueryable<ApplicationUserRole> GetAllRoles();

    Task<string?> GetRoleByUserId(string userId);



    //Task<IEnumerable<AspnetUserVm>> GetAllUsersWithRole();

}

