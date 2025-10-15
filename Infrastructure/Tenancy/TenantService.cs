using Application.Features.Tenancy;
using Application.Features.Tenancy.Commands;

namespace Infrastructure.Tenancy;

public class TenantService : ITenantService
{
    public Task<string> CreateTenantAsync(CreateTenantRequest createTenant)
    {
        throw new NotImplementedException();
    }
}