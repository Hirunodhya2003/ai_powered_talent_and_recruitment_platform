using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EducationRepository
    : GenericRepository<Education>, IEducationRepository
{
    public EducationRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Education>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Set<Education>()
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }
}