using Infrastructure.Tenancy;

namespace Infrastructure.Persistence.DbInitializers;

internal interface ITenantDbInitializer
{
    Task InitializeDatabaseAsync(CancellationToken cancellationToken);
    // Task InitializeDatabaseWithTenantAsync(CancellationToken cancellationToken);
    // Task InitializeApplicationDbForTenantAsync(SchoolTenantInfo tenantInfo, CancellationToken cancellationToken);
}