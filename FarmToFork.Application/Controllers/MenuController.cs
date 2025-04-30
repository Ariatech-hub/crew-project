
namespace FarmToFork.Application.Controllers;

    [Route("api"), Authorize]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuController> _logger;
        private readonly IAuthService _authService;

        public MenuController(IMenuService menuService, ILogger<MenuController> logger, IAuthService authService)
        {
            _menuService = menuService;
            _logger = logger;
            _authService = authService;
        }

        [HttpGet, Route("~/api/menus"), CheckAccessForMenu(MenuCode = 2)]
        public async Task<IActionResult> GetMenus()
        {
            try
            {
                var menus = await _menuService.GetAllMenus();
                return Ok(GetMenuTree(menus));
            }
            catch (Exception e)
            {
                _logger.LogError("exception Occurred while getting all menus", e.Message);
                return BadRequest(e.Message);
            }
        }

        private IEnumerable<MenuDto> GetMenuTree(IEnumerable<MenuDto> menus)
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


        [HttpGet, Route("menus-by-user/{aspNetUserId}")]
        public async Task<IActionResult> GetMenusByUser(string aspNetUserId)
        {
            try
            {
                var user = await _authService.GetUserById(aspNetUserId);
                IEnumerable<MenuDto> menus = await _menuService.GetMenusByUser(user.UserName);
                return Ok(menus);
            }
            catch (Exception e)
            {
                _logger.LogError("exception Occurred while getting all menus", e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("menus/accesslevel/{accessLevelId}"), CheckAccessForMenu(MenuCode = 2)]
        public async Task<IActionResult> GetMenusByAccessLevel(int accessLevelId)
        {
            try
            {
                var menus = await _menuService.GetMenusByAccessLevelForAssociation(accessLevelId);
                return Ok(menus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("menus/accesslevel/update"), CheckAccessForMenu(MenuCode = 2)]

        public async Task<IActionResult> UpdateMenusForAccessLevel(UpdateMenusForAccessLevel data)
        {
            try
            {
                await _menuService.UpdateMenusForAccessLevel(data);
                return Ok("Done!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("menus/user/update"), CheckAccessForMenu(MenuCode = 2)]
        public async Task<IActionResult> UpdateMenusForUser(UpdateMenusForUser data)
        {
            try
            {
                await _menuService.UpdateMenusForUser(data);
                return Ok("Done!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

