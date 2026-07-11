using Domain.Entities;

namespace Domain.Interfaces;

public interface IResumeRepository : IGenericRepository<Resume>
{
    Task<Resume?> GetByUserIdAsync(Guid userId);
}