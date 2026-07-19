namespace Application.DTOs.Interviews;

public class InterviewResponseDto
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid CandidateId { get; set; }

    public Guid RecruiterId { get; set; }

    public DateTime InterviewDate { get; set; }

    public TimeSpan InterviewTime { get; set; }

    public string? Location { get; set; }

    public string? MeetingLink { get; set; }

    public DateTime CreatedAt { get; set; }
}