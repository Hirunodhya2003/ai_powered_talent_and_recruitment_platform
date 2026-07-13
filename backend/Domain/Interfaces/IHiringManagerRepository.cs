using Domain.Entities;

namespace Domain.Interfaces;

public interface IHiringManagerRepository : IGenericRepository<HiringManager>
{
    Task<HiringManager?> GetByUserIdAsync(Guid userId);
}