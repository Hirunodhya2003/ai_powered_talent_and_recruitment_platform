namespace Application.DTOs.CandidateSkill;

public class CreateCandidateSkillDto
{
    public Guid SkillId { get; set; }

    public string ProficiencyLevel { get; set; } = string.Empty;
}