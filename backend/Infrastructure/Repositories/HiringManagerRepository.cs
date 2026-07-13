using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HiringManagerRepository
    : GenericRepository<HiringManager>, IHiringManagerRepository
{
    public HiringManagerRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<HiringManager?> GetByUserIdAsync(Guid userId)
    {
        return await _context.HiringManagers
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}