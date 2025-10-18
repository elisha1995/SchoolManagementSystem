using Finbuckle.MultiTenant;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.DbConfiguration;

internal class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .ToTable("Users", SchemaNames.Identity)
            .IsMultiTenant();
    }
}

internal class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder
            .ToTable("Roles", SchemaNames.Identity)
            .IsMultiTenant()
            .AdjustUniqueIndexes();
        
        builder
            .Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(r => r.NormalizedName)
            .IsRequired(false)
            .HasMaxLength(256);   
    }
}

internal class ApplicationRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder
            .ToTable("RoleClaims", SchemaNames.Identity)
            .IsMultiTenant();
    }
}

internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder
            .ToTable("UserRoles", SchemaNames.Identity)
            .IsMultiTenant();   
    }
}

internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder
            .ToTable("UserClaims", SchemaNames.Identity)
            .IsMultiTenant();
    }
}

internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
    {
        builder
            .ToTable("UserLogins", SchemaNames.Identity)
            .IsMultiTenant();
    }
}

internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder
            .ToTable("UserTokens", SchemaNames.Identity)
            .IsMultiTenant();   
    }
}