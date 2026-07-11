using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class HiringManagerRepository
    : GenericRepository<HiringManager>, IHiringManagerRepository
{
    public HiringManagerRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}