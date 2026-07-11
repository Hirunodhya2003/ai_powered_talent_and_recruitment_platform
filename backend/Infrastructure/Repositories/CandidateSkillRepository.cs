using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CandidateSkillRepository
    : GenericRepository<CandidateSkill>, ICandidateSkillRepository
{
    public CandidateSkillRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<CandidateSkill>> GetSkillsByCandidateIdAsync(Guid candidateId)
    {
        return await _context.CandidateSkills
            .Where(cs => cs.CandidateId == candidateId)
            .Include(cs => cs.Skill)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid candidateId, Guid skillId)
    {
        return await _context.CandidateSkills
            .AnyAsync(cs =>
                cs.CandidateId == candidateId &&
                cs.SkillId == skillId);
    }
}