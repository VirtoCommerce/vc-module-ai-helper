using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using VirtoCommerce.AiHelper.Data.Repositories;

namespace VirtoCommerce.AiHelper.Data.SqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AiHelperDbContext>
{
    public AiHelperDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AiHelperDbContext>();
        var connectionString = args.Length != 0 ? args[0] : "Server=(local);User=virto;Password=virto;Database=VirtoCommerce3;";

        builder.UseSqlServer(
            connectionString,
            options => options.MigrationsAssembly(typeof(SqlServerDataAssemblyMarker).Assembly.GetName().Name));

        return new AiHelperDbContext(builder.Options);
    }
}
