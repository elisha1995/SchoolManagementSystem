using System.ComponentModel.DataAnnotations;
using Finbuckle.MultiTenant.Abstractions;

namespace Infrastructure.Tenancy;

public class SchoolTenantInfo : ITenantInfo
{
    [Required]
    [MaxLength(64)]
    public string Id { get; set; }
    
    [Required]
    [MaxLength(60)]
    public string Identifier { get; set; }
    
    [Required]
    [MaxLength(60)]
    public string Name { get; set; }
    
    [MaxLength(256)]
    public string ConnectionString { get; set; }
    
    [MaxLength(60)]
    [Required]
    public string AdminEmail { get; set; }
    
    [Required]
    public DateTime ValidUpto { get; set; }
    
    [Required]
    public bool IsActive { get; set; }
}