namespace Application.DTOs.Experience;

public class UpdateExperienceDto
{
    public string Company { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;

    public string EmploymentType { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool IsCurrentJob { get; set; }

    public string? Description { get; set; }
}