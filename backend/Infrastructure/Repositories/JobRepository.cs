using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JobRepository : GenericRepository<Job>, IJobRepository
{
    public JobRepository(RecruitmentDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Job>> GetByRecruiterIdAsync(Guid recruiterId)
    {
        // RecruiterId property එක Job entity එකේ නැති නිසා
        return await _context.Jobs.ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetByOrganizationIdAsync(Guid orgId)
    {
        return await _context.Jobs
            .Where(j => j.OrganizationId == orgId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetActiveJobsAsync()
    {
        return await _context.Jobs
            .Where(j => j.ExpiryDate > DateTime.UtcNow)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> SearchJobsAsync(
        string keyword,
        string location,
        string type)
    {
        return await _context.Jobs
            .Where(j =>
                (string.IsNullOrEmpty(keyword) || j.Title.Contains(keyword)) &&
                (string.IsNullOrEmpty(location) || j.Location.Contains(location)))
            .ToListAsync();
    }

    public async Task<Job?> GetJobWithApplicationsAsync(Guid jobId)
    {
        return await _context.Jobs
            .Include(j => j.Organization)
            .FirstOrDefaultAsync(j => j.Id == jobId);
    }
}