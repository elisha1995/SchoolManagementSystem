using Finbuckle.MultiTenant;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Persistence.DbInitializers;

internal class TenantDbInitializer(TenantDbContext tenantDbContext,
    IServiceProvider serviceProvider) : ITenantDbInitializer
{
    private readonly TenantDbContext _tenantDbContext = tenantDbContext;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task InitializeDatabaseAsync(CancellationToken cancellationToken)
    {
        await InitializeDatabaseWithTenantAsync(cancellationToken);

        foreach (var tenant in await _tenantDbContext.TenantInfo.ToListAsync(cancellationToken))
        {
            await InitializeApplicationDbForTenantAsync(tenant, cancellationToken);
        }
    }
    
    private async Task InitializeDatabaseWithTenantAsync(CancellationToken cancellationToken)
    {
        // check if root tenant exists
        if (await _tenantDbContext.TenantInfo.FindAsync([TenancyConstants.Root.Id],
                cancellationToken: cancellationToken) is null)
        {
            // create root tenant
            var rootTenant = new SchoolTenantInfo
            {
                Id = TenancyConstants.Root.Id,
                Identifier = TenancyConstants.Root.Name,
                Name = TenancyConstants.Root.Name,
                AdminEmail = TenancyConstants.Root.Email,
                IsActive = true,
                ValidUpto = DateTime.UtcNow.AddYears(1)
            };
            
            await _tenantDbContext.TenantInfo.AddAsync(rootTenant, cancellationToken);
            await _tenantDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    private async Task InitializeApplicationDbForTenantAsync(SchoolTenantInfo tenantInfo, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var contextSetter = scope.ServiceProvider.GetRequiredService<IMultiTenantContextSetter>();
        contextSetter.MultiTenantContext = new MultiTenantContext<SchoolTenantInfo>
        {
            TenantInfo = tenantInfo
        };
        
        await _serviceProvider.GetRequiredService<ApplicationDbInitializer>()
            .InitializeDatabaseAsync(cancellationToken);
    }
}