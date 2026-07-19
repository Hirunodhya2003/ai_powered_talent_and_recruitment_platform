namespace Application.DTOs.Interviews;

public class UpdateInterviewDto
{
    public DateTime InterviewDate { get; set; }

    public TimeSpan InterviewTime { get; set; }

    public string? Location { get; set; }

    public string? MeetingLink { get; set; }
}