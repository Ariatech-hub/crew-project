





public class AuthRepository : IAuthRepository
{
    readonly UserManager<ApplicationUser> _userManager;
    readonly RoleManager<ApplicationUserRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly AppDbContext _appDbContext;
    private readonly IGeneralUtility _generalUtility;

    public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRole> roleManager,
        SignInManager<ApplicationUser> signInManager, AppDbContext appDbContext, IGeneralUtility generalUtility)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _appDbContext = appDbContext;
        _generalUtility = generalUtility;
    }
    public async Task<ApplicationUser> Register(string username, string password, string confirmPassword, string role)
    {
        if (string.IsNullOrEmpty(role))
            throw new Exception("Role is required to register the User.");

        string roleName = (await _roleManager.FindByIdAsync(role)).Name;



        //if (!_roleManager.Roles.Any(x => string.CompareOrdinal(x.Name, role) == 0))

        //{
        //    throw new Exception("Role cannot be found!");
        //}

        if (string.IsNullOrEmpty(username))
            throw new Exception("Username is required.");

        if (string.IsNullOrEmpty(password))
            throw new Exception("Password is required.");

        if (password != confirmPassword)
            throw new Exception("Password and Confirm Password do not match.");

        using TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        {
            try
            {
                var toRegister = new ApplicationUser() { UserName = username };

                var result = await _userManager.CreateAsync(toRegister, password);

                if (result.Succeeded)
                {

                    var user = await _userManager.FindByNameAsync(username);
                    await _userManager.AddToRoleAsync(user, roleName);


                    //int accessLevelId = (await _appDbContext.AccessLevels.SingleAsync(x => x.RoleId == role)).Id;


                    //IEnumerable<MenuAccessLevel> accessLevels = await _appDbContext.MenuAccessLevels.Where(x => x.AccessLevelId == accessLevelId).ToListAsync();


                    //List<MenuAspNetUser> menuAspNetUsers = accessLevels.Select(item => new MenuAspNetUser() { MenuId = item.MenuId, AspNetUserId = user.Id }).ToList();


                    //await _appDbContext.MenuAspNetUsers.AddRangeAsync(menuAspNetUsers);
                    //await _appDbContext.SaveChangesAsync();
                    trans.Complete();
                    return user;
                }
                else
                {
                    throw new Exception(result.Errors?.FirstOrDefault()?.Description ?? "User cannot be registered now.");
                }
            }
            catch (Exception e)
            {
                trans.Dispose();
                throw new Exception(e.Message);
            }

        }

    }

    public async Task<string> GetRoleByUser(ApplicationUser user)
    {
        var result = await _userManager.GetRolesAsync(user);
        return result.FirstOrDefault()!;
    }

    public async Task<ApplicationUser> GetUserByUsername(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<bool> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

        if (result.Succeeded)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);

            AuditLogin auditLogin = new()
            {
                UserId = user.Id,
                LogInTime = _generalUtility.GetCurrentNepalTime()
            };
            await _appDbContext.AddAsync(auditLogin);
            await _appDbContext.SaveChangesAsync();
        }

        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task CreateRole(string role)
    {
        await _roleManager.CreateAsync(new ApplicationUserRole() { Name = role });
    }

    public async Task<bool> CheckIfUsernameExists(string username)
    {
        var result = await _userManager.FindByNameAsync(username);
        return result != null;
    }

    public async Task<IdentityResult> RegisterWithoutPassword(ApplicationUser user, string role)
    {
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
            return IdentityResult.Success;
        }
        else
        {
            throw new Exception(result.Errors.FirstOrDefault()?.Description);
        }
    }

    public Task<bool> UpdateUser(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new Exception("Invalid User");
        }

        return user;
    }

    public async Task ResetPassword(string username, string password)
    {
        ApplicationUser applicationUser = await _userManager.FindByNameAsync(username);
        if (applicationUser is null) throw new Exception("Invalid User");
        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
        IdentityResult identityResult = await _userManager.ResetPasswordAsync(applicationUser, resetToken, password);
        if (!identityResult.Succeeded)
        {
            throw new Exception(identityResult.Errors.FirstOrDefault()?.Description);
        }

    }

    public async Task UnRegisterUser(string userName)
    {
        ApplicationUser applicationUser = await _userManager.FindByNameAsync(userName);
        if (applicationUser is null) throw new Exception("Invalid user");
        var roleForUser = await _userManager.GetRolesAsync(applicationUser);
        var logins = await _userManager.GetLoginsAsync(applicationUser);

        if (logins.Any())
        {
            foreach (var login in logins.ToList())
            {
                IdentityResult removeLoginResult = await _userManager.RemoveLoginAsync(user: applicationUser, login.LoginProvider, login.ProviderKey);
                if (!removeLoginResult.Succeeded)
                {
                    throw new Exception(removeLoginResult.Errors.FirstOrDefault()?.Description);
                }
            }
        }

        if (roleForUser.Any())
        {
            foreach (var item in roleForUser.ToList())
            {
                IdentityResult result = await _userManager.RemoveFromRoleAsync(applicationUser, item);
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.FirstOrDefault()?.Description);
                }

            }

        }

        IEnumerable<MenuAspNetUser> menuAspNetUsers = await _appDbContext.MenuAspNetUsers.Where(x => x.AspNetUserId == applicationUser.Id).ToListAsync();

        if (menuAspNetUsers.Any())
        {
            _appDbContext.MenuAspNetUsers.RemoveRange(menuAspNetUsers);
            await _appDbContext.SaveChangesAsync();
        }

        await _userManager.DeleteAsync(user: applicationUser);
    }

    public async Task ChangePassword(string username, string oldPassword, string password)
    {
        ApplicationUser applicationUser = await _userManager.FindByNameAsync(username);
        if (applicationUser is null) throw new Exception("Invalid User");
        IdentityResult identityResult = await _userManager.ChangePasswordAsync(user: applicationUser, oldPassword, password);
        if (!identityResult.Succeeded)
        {
            throw new Exception(identityResult.Errors.FirstOrDefault()?.Description);
        }

    }

    public IQueryable<ApplicationUser> GetAllUsers()
    {
        return _userManager.Users;
    }

    public IQueryable<ApplicationUserRole> GetAllRoles()
    {
        return _roleManager.Roles;
    }

    public async Task<string?> GetRoleByUserId(string userId)
    {

        ApplicationUser user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new Exception("Invalid User");
        }

        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault();

    }

    public async Task<IEnumerable<ApplicationUser>> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return users;

    }
}

