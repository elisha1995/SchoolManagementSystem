using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using Infrastructure.Identity.Models;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Contexts;

public abstract class BaseDbContext : 
    MultiTenantIdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, 
        IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    protected BaseDbContext(IMultiTenantContextAccessor<SchoolTenantInfo> multiTenantContextAccessor, 
        DbContextOptions options) : base(multiTenantContextAccessor, options)
    {
        
    }
}