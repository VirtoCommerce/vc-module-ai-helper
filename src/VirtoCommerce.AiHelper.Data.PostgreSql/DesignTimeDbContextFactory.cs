using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.AiHelper.Data.Repositories;

namespace VirtoCommerce.AiHelper.Data.PostgreSql;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AiHelperDbContext>
{
    public AiHelperDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AiHelperDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=localhost;Username=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseNpgsql(
            connectionString,
            options => options.MigrationsAssembly(typeof(PostgreSqlDataAssemblyMarker).Assembly.GetName().Name));

        return new AiHelperDbContext(builder.Options);
    }
}
