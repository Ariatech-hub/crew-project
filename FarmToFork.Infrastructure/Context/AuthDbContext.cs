namespace FarmToFork.Infrastructure.Context;

public class AuthDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRole, string>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    { }
}

