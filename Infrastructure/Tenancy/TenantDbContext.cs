using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Infrastructure.Persistence.DbConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tenancy;

public class TenantDbContext(DbContextOptions<TenantDbContext> options) : EFCoreStoreDbContext<SchoolTenantInfo>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<SchoolTenantInfo>()
            .ToTable("Tenants", SchemaNames.Multitenancy);;
    }
}