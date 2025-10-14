using Finbuckle.MultiTenant.Abstractions;

namespace Infrastructure.Tenancy;

public class SchoolTenantInfo : ITenantInfo
{
    public string Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
    public string AdminEmail { get; set; }
    public DateTime ValidUpto { get; set; }
    public bool IsActive { get; set; }
}