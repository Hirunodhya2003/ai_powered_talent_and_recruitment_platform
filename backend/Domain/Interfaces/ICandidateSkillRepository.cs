using Domain.Entities;

namespace Domain.Interfaces;

public interface ICandidateSkillRepository : IGenericRepository<CandidateSkill>
{
    Task<IEnumerable<CandidateSkill>> GetSkillsByCandidateIdAsync(Guid candidateId);

    Task<bool> ExistsAsync(Guid candidateId, Guid skillId);
}