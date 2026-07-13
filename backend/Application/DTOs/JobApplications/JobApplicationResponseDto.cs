using Domain.Enums;

namespace Application.DTOs.JobApplications;

public class JobApplicationResponseDto
{
    public Guid Id { get; set; }

    public Guid CandidateId { get; set; }

    public Guid JobId { get; set; }

    public Guid? ResumeId { get; set; }

    public string? CoverLetter { get; set; }

    public ApplicationStatus Status { get; set; }

    public DateTime AppliedAt { get; set; }

    public decimal? AIMatchScore { get; set; }

    public string? RecruiterNotes { get; set; }
}