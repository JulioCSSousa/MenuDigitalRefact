using MenuDigital.Infrastructure.Persistence.MySQLContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MenuDigital.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");

        var cs = $"Server={dbHost};Port={dbPort};Database={dbName};User={dbUser};Password={dbPass};";

        optionsBuilder.UseMySql(cs, ServerVersion.AutoDetect(cs),
            b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));

        return new AppDbContext(optionsBuilder.Options);
    }
}
