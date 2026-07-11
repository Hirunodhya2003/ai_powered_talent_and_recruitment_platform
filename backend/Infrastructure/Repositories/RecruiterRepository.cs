using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RecruiterRepository
    : GenericRepository<Recruiter>, IRecruiterRepository
{
    public RecruiterRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<Recruiter?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Recruiters
            .FirstOrDefaultAsync(r => r.UserId == userId);
    }
}