namespace Application.DTOs.InterviewFeedbacks;

public class InterviewFeedbackResponseDto
{
    public Guid Id { get; set; }

    public Guid InterviewId { get; set; }

    public Guid HiringManagerId { get; set; }

    public decimal TechnicalScore { get; set; }

    public decimal CommunicationScore { get; set; }

    public decimal OverallScore { get; set; }

    public string? Notes { get; set; }

    public string? Recommendation { get; set; }

    public DateTime CreatedAt { get; set; }
}