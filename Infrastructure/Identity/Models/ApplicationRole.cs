using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models;

public class ApplicationRole : IdentityRole
{
    [MaxLength(256)]
    [Required]
    public string Description { get; set; }
}