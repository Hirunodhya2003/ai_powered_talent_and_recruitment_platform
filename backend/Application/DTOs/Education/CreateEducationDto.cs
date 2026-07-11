namespace Application.DTOs.Education;

public class CreateEducationDto
{
    //public Guid UserId { get; set; }

    public string Institution { get; set; } = string.Empty;

    public string Degree { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}