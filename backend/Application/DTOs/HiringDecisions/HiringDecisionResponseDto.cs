namespace Application.DTOs.HiringDecisions;

public class HiringDecisionResponseDto
{
    public Guid Id { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid HiringManagerId { get; set; }

    public decimal? AIMatchScore { get; set; }

    public string? Notes { get; set; }

    public DateTime? DecisionDate { get; set; }

    public DateTime CreatedAt { get; set; }
}