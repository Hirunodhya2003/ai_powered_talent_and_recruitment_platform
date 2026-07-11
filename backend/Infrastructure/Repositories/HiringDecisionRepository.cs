using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class HiringDecisionRepository
    : GenericRepository<HiringDecision>, IHiringDecisionRepository
{
    public HiringDecisionRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}