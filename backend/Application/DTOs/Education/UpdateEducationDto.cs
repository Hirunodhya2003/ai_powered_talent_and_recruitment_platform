using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Education;

public class UpdateEducationDto
{
    [Required]
    public string Institution { get; set; } = string.Empty;

    [Required]
    public string Degree { get; set; } = string.Empty;

    public string FieldOfStudy { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsCurrentlyStudying { get; set; }

    public string? Grade { get; set; }

    public string? Description { get; set; }
}