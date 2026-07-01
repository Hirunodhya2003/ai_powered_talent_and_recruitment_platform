namespace Domain.Entities;

public class JobApplication : BaseEntity
{
    public Guid CandidateId { get; set; }

    public Guid JobId { get; set; }

    public Guid ResumeId { get; set; }

    public DateTime AppliedDate { get; set; }

    public decimal? AIMatchScore { get; set; }

    public string? RecruiterNotes { get; set; }

    public Candidate Candidate { get; set; } = null!;

    public Job Job { get; set; } = null!;

    public Resume Resume { get; set; } = null!;
}