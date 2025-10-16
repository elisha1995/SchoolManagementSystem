using Domain.Entities;
using Finbuckle.MultiTenant.Abstractions;
using Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Contexts;

public class ApplicationDbContext(
    IMultiTenantContextAccessor<SchoolTenantInfo> multiTenantContextAccessor, DbContextOptions options)
    : BaseDbContext(multiTenantContextAccessor, options)
{
    public DbSet<School> Schools => Set<School>();
}