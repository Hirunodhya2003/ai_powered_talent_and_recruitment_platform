using Domain.Enums;

namespace Domain.Entities;

public class JobApplication : BaseEntity
{
    public Guid CandidateId { get; set; }

    public Guid JobId { get; set; }

    // Nullable because Resume can be deleted
    public Guid? ResumeId { get; set; }

    public string? CoverLetter { get; set; }

    public ApplicationStatus Status { get; set; }
        = ApplicationStatus.Pending;

    public DateTime AppliedAt { get; set; }
        = DateTime.UtcNow;

    public decimal? AIMatchScore { get; set; }

    public string? RecruiterNotes { get; set; }

    // Navigation Properties
    public Candidate Candidate { get; set; } = null!;

    public Job Job { get; set; } = null!;

    public Resume? Resume { get; set; }
}