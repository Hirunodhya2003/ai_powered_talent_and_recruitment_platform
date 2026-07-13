using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ExperienceRepository
    : GenericRepository<Experience>, IExperienceRepository
{
    public ExperienceRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Experience>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Set<Experience>()
            .Where(e => e.UserId == userId)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }
}