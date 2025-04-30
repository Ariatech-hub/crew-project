

namespace FarmToFork.Infrastructure.Infrastructure;

public interface IConnectionFactory
{
    public SqlConnection GetConnectionString(AppDbContext dbContext);
}
internal class ConnectionFactory : IConnectionFactory
{
    public SqlConnection GetConnectionString(AppDbContext dbContext)
    {
        var connectionString = dbContext.Database.GetDbConnection().ConnectionString;

        return new SqlConnection(connectionString);
    }
}

