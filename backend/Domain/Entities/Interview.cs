namespace Domain.Entities;

public class Interview : BaseEntity
{
    public Guid ApplicationId { get; set; }

    public Guid CandidateId { get; set; }

    public Guid RecruiterId { get; set; }

    public DateTime InterviewDate { get; set; }

    public TimeSpan InterviewTime { get; set; }

    public string? Location { get; set; }

    public string? MeetingLink { get; set; }

    public JobApplication Application { get; set; } = null!;

    public Candidate Candidate { get; set; } = null!;

    public Recruiter Recruiter { get; set; } = null!;
}