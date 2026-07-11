using Domain.Entities;

namespace Domain.Interfaces;

public interface IRecruiterRepository : IGenericRepository<Recruiter>
{
    Task<Recruiter?> GetByUserIdAsync(Guid userId);
}