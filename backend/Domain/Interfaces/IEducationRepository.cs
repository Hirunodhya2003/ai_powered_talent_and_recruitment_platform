using Domain.Entities;

namespace Domain.Interfaces;

public interface IEducationRepository : IGenericRepository<Education>
{
    Task<IEnumerable<Education>> GetByUserIdAsync(Guid userId);
}