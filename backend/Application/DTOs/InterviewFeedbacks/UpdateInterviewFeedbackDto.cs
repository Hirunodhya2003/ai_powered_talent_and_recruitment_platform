namespace Application.DTOs.InterviewFeedbacks;

public class UpdateInterviewFeedbackDto
{
    public decimal TechnicalScore { get; set; }

    public decimal CommunicationScore { get; set; }

    public decimal OverallScore { get; set; }

    public string? Notes { get; set; }

    public string? Recommendation { get; set; }
}