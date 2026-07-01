using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
{
    public CandidateRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<Candidate?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Candidates
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Candidate?> GetWithSkillsAsync(Guid candidateId)
    {
        return await _context.Candidates
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == candidateId);
    }

    public async Task<Candidate?> GetWithEducationAsync(Guid candidateId)
    {
        return await _context.Candidates
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == candidateId);
    }

    public async Task<Candidate?> GetWithExperienceAsync(Guid candidateId)
    {
        return await _context.Candidates
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == candidateId);
    }

    public async Task<Candidate?> GetFullProfileAsync(Guid candidateId)
    {
        return await _context.Candidates
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == candidateId);
    }

    public async Task<IEnumerable<Candidate>> SearchCandidatesAsync(string keyword)
    {
        keyword = keyword.ToLower();

        return await _context.Candidates
            .Include(c => c.User)
            .Where(c =>
                (c.ProfessionalHeadline != null &&
                 c.ProfessionalHeadline.ToLower().Contains(keyword))
                ||
                (c.Location != null &&
                 c.Location.ToLower().Contains(keyword))
                ||
                (c.PreferredJobRole != null &&
                 c.PreferredJobRole.ToLower().Contains(keyword)))
            .ToListAsync();
    }
}