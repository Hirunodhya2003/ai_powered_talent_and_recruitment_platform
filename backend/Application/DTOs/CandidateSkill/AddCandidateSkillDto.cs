using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.CandidateSkills;

public class AddCandidateSkillDto
{
    [Required]
    public Guid SkillId { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProficiencyLevel { get; set; } = string.Empty;
}