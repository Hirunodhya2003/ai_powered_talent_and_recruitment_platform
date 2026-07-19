namespace Application.DTOs.HiringDecisions;

public class UpdateHiringDecisionDto
{
    public decimal? AIMatchScore { get; set; }

    public string? Notes { get; set; }

    public DateTime? DecisionDate { get; set; }
}