namespace Application.DTOs.Auth;

using Domain.Enums;
using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; }

    // Recruiter only
    public Guid? OrganizationId { get; set; }

    public string? Department { get; set; }

    public string? JobTitle { get; set; }

    public string? PhoneNumber { get; set; }
}