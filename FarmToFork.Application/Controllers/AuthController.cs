

using FarmToFork.Core.Exception;

namespace FarmToFork.Application.Controllers;

[Route("api/auth"), Authorize]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IGeneralUtility _generalUtility;
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    private readonly IMenuService _menuService;


    public AuthController(IGeneralUtility generalUtility, ILogger<AuthController> logger, IAuthService authService, IConfiguration configuration, IMenuService menuService)
    {
        _generalUtility = generalUtility;
        _logger = logger;
        _authService = authService;
        _configuration = configuration;
        _menuService = menuService;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model)
    {
        try
        {

            if (string.IsNullOrEmpty(model.Username))

                throw new DataValidationException("Username cannot be empty!");

            if (string.IsNullOrEmpty(model.Password))
                throw new DataValidationException("Password cannot be empty!");

            _logger.LogInformation("Login Initiated for {username}", model.Username);

            bool result = await _authService.Login(model.Username, model.Password);

            if (result)
            {
                var user = await _authService.GetUserByUsername(model.Username);

                var role = await _authService.GetRoleByUser(user);

                if (string.IsNullOrEmpty(role))
                {
                    throw new DataNotFoundException($"Role is not assigned for {model.Username}");
                }


                var _tempToken = await GenerateJwtToken(user);
                return Ok(new
                {
                    AccessToken = _tempToken.ToString(),
                    ExpiresIn = Convert.ToInt32(_configuration["JwtExpireDays"]) * 24 * 60
                });
            }
            else
            {
                throw new DataValidationException("Invalid Username or Password!");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login Failure for {username}", model.Username);
            return BadRequest(ex.Message);
        }
    }

    private async Task<object> GenerateJwtToken(ApplicationUser user)
    {
        var theRole = await _authService.GetRoleByUser(user);

        var claims = new List<Claim>
            {
                new Claim("username", user.UserName),
                new Claim(ClaimTypes.Role, theRole )
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],
            _configuration["JwtIssuer"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            var username = _generalUtility.GetLoggedInUsername();
            var user = await _authService.GetUserByUsername(username);
            string role = await _authService.GetRoleByUser(user);
            IEnumerable<MenuDto> menus = await _menuService.GetMenusByUser(username);
            var menuTree = GetMenuTree(menus);
            string _tempIds = "";

            foreach (var menu in menuTree)
            {
                _tempIds += menu.Id + ",";
                if (menu.Children.Any())
                {
                    foreach (var child in menu.Children)
                    {
                        _tempIds += child.Id + ",";
                    }
                }
            }

            _tempIds = _tempIds.TrimEnd(',');

            string[] abc = _tempIds.Split(',');

            int[] menuIds = new int[abc.Length];

            for (int i = 0; i < abc.Length; i++)
            {
                if (!string.IsNullOrEmpty(abc[i]))
                {
                    menuIds[i] = int.Parse(abc[i]);
                }
            }

            return Ok(new { username, role, menuTree, menuIds });
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception Occurred while getting user", ex.Message);
            return BadRequest(ex.Message);
        }
    }

    private IEnumerable<MenuDto> GetMenuTree(IEnumerable<MenuDto> menus)
    {
        if (menus != null && menus.Any())
        {
            foreach (var menu in menus)
            {
                var childMenus = menus.Where(x => x.ParentId == menu.Id);

                menu.Children = childMenus;
                foreach (var item in menu.Children)
                {
                    GetMenuTree(menu.Children);
                }
                
            }
            return menus.Where(x => x.ParentId == 0).OrderBy(x => x.OrderId);
        }
        else
        {
            return new List<MenuDto>();
        }
    }

    [HttpPost, Route("reset-password")]
    public async Task<IActionResult> ResetPassword(PasswordResetVm passwordResetVm)
    {
        try
        {
            await _authService.ResetPassword(passwordResetVm);
            return Ok($"Password has been reset for {passwordResetVm.Username}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception Occurred while getting user", ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost, Route("unregister-user")]
    public async Task<IActionResult> UnregisterUser(UnRegisterVm unRegisterVm)
    {
        try
        {
            await _authService.UnRegisterUser(unRegisterVm);
            return Ok($"User {unRegisterVm.UserName} has been unregistered");
        }
        catch (Exception e)
        {
            _logger.LogError("Exception occurred while unregistered user", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordVm changePasswordVm)
    {
        try
        {
            await _authService.ChangePassword(changePasswordVm);
            return Ok("Password has been changed");

        }
        catch (Exception e)
        {
            _logger.LogError("Exception occurred while change password", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("users")]
    public IActionResult GetAllUsers()
    {
        try
        {
            return Ok(_authService.GetAllUsers());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception occurred while getting all users", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("roles")]
    public IActionResult GetAllRoles()
    {
        try
        {
            return Ok(_authService.GetAllRoles());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception occurred while getting all roles", e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet, Route("user-with-role")]
    public async Task<IActionResult> GetUsersWithRole()
    {
        try
        {
            return Ok(await _authService.GetAllUsersWithRole());
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting users with role");
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    [Route("user/register")]
    public async Task<IActionResult> RegisterUser(RegisterModel model)
    {
        try
        {
            await _authService.Register(model.Username, model.Password, model.ConfirmPassword, model.Role);
            return Ok($"User {model.Username} has been added");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet, Route("username/{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        try
        {
            ApplicationUser applicationUser = await _authService.GetUserById(userId);
            return Ok(applicationUser.UserName);
        }
        catch (Exception e)
        {
            _logger.LogError("Exception Occurred while getting username from user Id");
            return BadRequest(e.Message);
        }
    }

    [HttpPost, Route("create-role")]
    public async Task<IActionResult> CreateRole(Role role)
    {
        try
        {
            await _authService.CreateRole(roleName: role.RoleName);
            return Ok("Role has been created");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }




}

