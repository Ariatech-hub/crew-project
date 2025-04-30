


namespace FarmToFork.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IConnectionFactory _connectionFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IGeneralUtility _generalUtility;

    public MenuRepository(AppDbContext appDbContext, IConnectionFactory connectionFactory, IHttpContextAccessor httpContextAccessor, IGeneralUtility generalUtility)
    {
        _appDbContext = appDbContext;
        _connectionFactory = connectionFactory;
        _httpContextAccessor = httpContextAccessor;
        _generalUtility = generalUtility;
    }

    public async Task<IEnumerable<Menu>> GetMenusByUser(string username)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        const string query = $"SELECT m.* FROM [MenuAspNetUser] mau " +
                        $"INNER JOIN [AspNetUsers] anu ON anu.[Id] = mau.[AspNetUserId] " +
                        $"INNER JOIN [Menu] m ON m.[Id] = mau.[MenuId] " +
                        $"WHERE anu.[Username] = @Username AND m.IsActive = 1 ORDER BY m.ParentId ASC , m.OrderId ASC ";
        var param = new DynamicParameters();
        param.Add("Username", username);
        return await conn.QueryAsync<Menu>(query, param, commandType: CommandType.Text);
    }

    public async Task<IEnumerable<Menu>> GetAllMenus()
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        var query = $"SELECT * FROM [Menu] ORDER BY [Id]";
        return await conn.QueryAsync<Menu>(query, commandType: CommandType.Text);
    }

    public async Task<bool> HasAccessForMenu(int menuCode, string username)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        string query = $"DECLARE @UserId AS VARCHAR(512); " +
                       $"SET @UserId = (SELECT[Id] FROM[AspNetUsers] WHERE[UserName] = @Username); " +
                       $"IF EXISTS(SELECT* FROM [MenuAspNetUser] WHERE[AspNetUserId] = @UserId AND[MenuId] = @MenuCode) " +
                       $"SELECT 1; " +
                       $"ELSE " +
                       $"SELECT 0; ";

        var param = new DynamicParameters();
        param.Add("@MenuCode", menuCode);
        param.Add("@Username", username);
        return await conn.ExecuteScalarAsync<bool>(query, param, commandType: CommandType.Text);
    }

    public async Task<IEnumerable<Menu>> GetMenusByAccessLevelForAssociation(int accessLevelId)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        string query = $"SELECT m.*, CASE WHEN ISNULL(mal.MenuId, 0) = 0 THEN 0 ELSE 1 END IsSelected " +
                       $"FROM[Menu] m " +
                       $"LEFT JOIN [MenuAccessLevel] mal ON mal.[MenuId] = m.[Id] AND mal.[AccessLevelId] = @AccessLevelId " +
                       $"WHERE m.[IsActive] = 1";
        var param = new DynamicParameters();
        param.Add("@AccessLevelId", accessLevelId);
        return await conn.QueryAsync<Menu>(query, param, commandType: CommandType.Text);
    }

    public async Task UpdateMenusForAccessLevel(int id, int[] menuIds)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        string query = $"MERGE [MenuAccessLevel] AS TARGET " +
                       $"USING STRING_SPLIT(@MenuIds, ',') AS SOURCE " +
                       $"ON TARGET.[MenuId] = SOURCE.[Value] AND TARGET.[AccessLevelId] = @AccessLevelId " +
                       $"WHEN MATCHED THEN UPDATE " +
                       $"SET " +
                       $"TARGET.[AdminName] = @AdminName, " +
                       $"TARGET.[ModifiedDate] = @ModifiedDate " +
                       $"WHEN NOT MATCHED BY TARGET " +
                       $"THEN " +
                       $"INSERT([AccessLevelId], [MenuId], [AdminName], [CreatedDate]) " +
                       $"VALUES(@AccessLevelId, SOURCE.[Value], @AdminName, @ModifiedDate) " +
                       $"WHEN NOT MATCHED BY SOURCE AND TARGET.[AccessLevelId] = @AccessLevelId THEN DELETE; ";

        var param = new DynamicParameters();
        param.Add("@AccessLevelId", id);
        param.Add("@MenuIds", string.Join(",", menuIds));
        param.Add("@AdminName", _generalUtility.GetLoggedInUsername());
        param.Add("@ModifiedDate", _generalUtility.GetCurrentNepalTime());

        await conn.ExecuteAsync(query, param, commandType: CommandType.Text);
    }

    public async Task UpdateMenusForUser(string userId, int[] menuIds)
    {
        await using var conn = _connectionFactory.GetConnectionString(_appDbContext);
        const string query = $"IF(@MenuIds != '') " +
                       $"BEGIN " +
                       $"MERGE [MenuAspNetUser] AS TARGET " +
                       $"USING STRING_SPLIT(@MenuIds, ',') AS SOURCE " +
                       $"ON TARGET.[MenuId] = SOURCE.[Value] AND TARGET.[AspNetUserId] = @UserId " +
                       $"WHEN NOT MATCHED BY TARGET " +
                       $"THEN " +
                       $"INSERT([AspNetUserId], [MenuId]) VALUES(@UserId, SOURCE.[Value]) " +
                       $"WHEN NOT MATCHED BY SOURCE AND TARGET.[AspNetUserId] = @UserId THEN DELETE; " +
                       $"END " +
                       $"ELSE " +
                       $"BEGIN " +
                       $"DELETE FROM [MenuAspNetUser] WHERE [AspNetUserId] = @UserId " +
                       $"END";
        var param = new DynamicParameters();
        param.Add("@UserId", userId);
        param.Add("@MenuIds", string.Join(",", menuIds));
        await conn.ExecuteAsync(query, param, commandType: CommandType.Text);
    }
}

