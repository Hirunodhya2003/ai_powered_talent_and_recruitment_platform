using Domain.Enums;

namespace Application.DTOs.JobApplications;

public class UpdateJobApplicationDto
{
    public ApplicationStatus Status { get; set; }

    public string? RecruiterNotes { get; set; }

    public decimal? AIMatchScore { get; set; }
}