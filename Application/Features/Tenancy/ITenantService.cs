using Application.Features.Tenancy.Commands;

namespace Application.Features.Tenancy;

public interface ITenantService
{
    // Creation of a Tenant
    Task<string> CreateTenantAsync(CreateTenantRequest createTenant);
}