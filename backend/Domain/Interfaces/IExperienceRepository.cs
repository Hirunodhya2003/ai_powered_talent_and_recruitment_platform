using Domain.Entities;

namespace Domain.Interfaces;

public interface IExperienceRepository : IGenericRepository<Experience>
{
    Task<IEnumerable<Experience>> GetByUserIdAsync(Guid userId);
}