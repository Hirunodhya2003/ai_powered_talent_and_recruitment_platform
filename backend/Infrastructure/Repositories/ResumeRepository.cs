using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ResumeRepository
    : GenericRepository<Resume>, IResumeRepository
{
    private readonly RecruitmentDbContext _context;

    public ResumeRepository(RecruitmentDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<Resume?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Resumes
            .FirstOrDefaultAsync(r => r.UserId == userId && r.IsActive);
    }
}