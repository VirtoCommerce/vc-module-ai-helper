using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VirtoCommerce.AiHelper.Data.Models;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.AiHelper.Data.Repositories;

public class AiHelperDbContext : DbContextBase
{
    public AiHelperDbContext(DbContextOptions<AiHelperDbContext> options)
        : base(options)
    {
    }

    protected AiHelperDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AiRequestLogEntity>().ToTable("AiRequestLog").HasKey(x => x.Id);
        modelBuilder.Entity<AiRequestLogEntity>().Property(x => x.Id).HasMaxLength(128).ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);

        switch (Database.ProviderName)
        {
            case "Pomelo.EntityFrameworkCore.MySql":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.AiHelper.Data.MySql"));
                break;
            case "Npgsql.EntityFrameworkCore.PostgreSQL":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.AiHelper.Data.PostgreSql"));
                break;
            case "Microsoft.EntityFrameworkCore.SqlServer":
                modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("VirtoCommerce.AiHelper.Data.SqlServer"));
                break;
        }
    }
}
