namespace Application.DTOs.Candidate;

public class CandidateProfileDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? ProfessionalHeadline { get; set; }

    public string? Summary { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Location { get; set; }

    public bool IsOpenToWork { get; set; }

    public string? PreferredJobRole { get; set; }

    public string? EmploymentType { get; set; }

    public string? PreferredLocation { get; set; }

    public string? WorkPreference { get; set; }

    public int ProfileCompleteness { get; set; }
}