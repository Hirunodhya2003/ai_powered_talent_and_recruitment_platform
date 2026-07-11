using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Users;

public class UpdateUserDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Optional - only when changing the password
    public string? PasswordHash { get; set; }

    [Required]
    public UserRole Role { get; set; }

    public bool IsActive { get; set; }
}