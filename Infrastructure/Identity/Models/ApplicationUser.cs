using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    [MaxLength(60)]
    public string FirstName { get; set; }
    
    [MaxLength(60)]
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}