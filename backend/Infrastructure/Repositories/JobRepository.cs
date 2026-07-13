using Domain.Entities;
using Domain.Enums;
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
        return await _context.Jobs
            .Where(j => j.RecruiterId == recruiterId)
            .Include(j => j.Organization)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetByOrganizationIdAsync(Guid orgId)
    {
        return await _context.Jobs
            .Where(j => j.OrganizationId == orgId)
            .Include(j => j.Recruiter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> GetActiveJobsAsync()
    {
        return await _context.Jobs
            .Where(j =>
                j.Status == JobStatus.Active &&
                j.ExpiryDate > DateTime.UtcNow)
            .Include(j => j.Organization)
            .ToListAsync();
    }

    public async Task<IEnumerable<Job>> SearchJobsAsync(
        string keyword,
        string location,
        string type)
    {
        return await _context.Jobs
            .Where(j =>
                (string.IsNullOrWhiteSpace(keyword) ||
                    j.Title.Contains(keyword) ||
                    j.Description.Contains(keyword))
                &&
                (string.IsNullOrWhiteSpace(location) ||
                    j.Location.Contains(location))
                &&
                (string.IsNullOrWhiteSpace(type) ||
                    j.EmploymentType == type)
                &&
                j.Status == JobStatus.Active
                &&
                j.ExpiryDate > DateTime.UtcNow)
            .Include(j => j.Organization)
            .ToListAsync();
    }

    public async Task<Job?> GetJobWithApplicationsAsync(Guid jobId)
    {
        return await _context.Jobs
            .Include(j => j.Organization)
            .Include(j => j.Recruiter)
            .Include(j => j.JobApplications)
            .FirstOrDefaultAsync(j => j.Id == jobId);
    }
}