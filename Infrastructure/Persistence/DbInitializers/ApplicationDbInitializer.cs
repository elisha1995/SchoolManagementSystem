using Infrastructure.Identity.Constants;
using Infrastructure.Identity.Models;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Tenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DbInitializers;

internal class ApplicationDbInitializer(SchoolTenantInfo tenant, RoleManager<ApplicationRole> roleManager, 
    UserManager<ApplicationUser> userManager)
{
    private readonly SchoolTenantInfo _tenant = tenant;
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    
    public async Task InitializeDatabaseAsync(ApplicationDbContext applicationDbContext, CancellationToken cancellationToken)
    {
        await InitializeDefaultRolesAsync(applicationDbContext, cancellationToken);
        await InitializeAdminUserAsync();
    }

    private async Task InitializeDefaultRolesAsync(ApplicationDbContext applicationDbContext,
        CancellationToken cancellationToken)
    {
        foreach (string roleName in RoleConstants.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(role => role.Name == roleName, cancellationToken) is not ApplicationRole incomingRole)
            {
                incomingRole = new ApplicationRole()
                {
                    Name = roleName,
                    Description = $"{roleName} Role"
                };
                
                await _roleManager.CreateAsync(incomingRole);
            }
            
            // Assign permissions to newly added role
            if (roleName == RoleConstants.Basic)
            {
                await AssignPermissionsToRole(applicationDbContext, SchoolPermissions.Basic, incomingRole, cancellationToken);
            }
            else if (roleName == RoleConstants.Admin)
            {
                await AssignPermissionsToRole(applicationDbContext, SchoolPermissions.Admin, incomingRole, cancellationToken);
            }
        }
    }

    private async Task InitializeAdminUserAsync()
    {
        if (string.IsNullOrEmpty(_tenant.AdminEmail))
        {
            return;
        }
        
        if (await _userManager.Users.FirstOrDefaultAsync(user => user.Email == _tenant.AdminEmail) is not
            ApplicationUser adminUser)
        {
            adminUser = new ApplicationUser()
            {
                FirstName = TenancyConstants.FirstName,
                LastName = TenancyConstants.LastName,
                UserName = _tenant.AdminEmail,
                Email = _tenant.AdminEmail,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = _tenant.AdminEmail.ToUpperInvariant(),
                NormalizedUserName = _tenant.AdminEmail.ToUpperInvariant(),
                IsActive = true,
            };
            
            var password = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, TenancyConstants.DefaultPassword);
            
            await _userManager.CreateAsync(adminUser);
        }

        if (!await _userManager.IsInRoleAsync(adminUser, RoleConstants.Admin))
        {
            // assign admin role
            await _userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
        }
    }

    private async Task AssignPermissionsToRole(
        ApplicationDbContext dbContext,
        IReadOnlyList<SchoolPermission> rolePermissions,
        ApplicationRole currentRole, CancellationToken cancellationToken)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(currentRole);

        foreach (var rolePermission in rolePermissions)
        {
            if (!currentClaims.Any(c => c.Type == ClaimConstants.Permission && c.Value == rolePermission.Name))
            {
                await dbContext.RoleClaims.AddAsync(new IdentityRoleClaim<string>
                {
                    RoleId = currentRole.Id,
                    ClaimType = ClaimConstants.Permission,
                    ClaimValue = rolePermission.Name
                }, cancellationToken);
                
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}