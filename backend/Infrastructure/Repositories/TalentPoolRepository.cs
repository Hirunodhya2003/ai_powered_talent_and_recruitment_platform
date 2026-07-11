using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TalentPoolRepository
    : GenericRepository<TalentPool>, ITalentPoolRepository
{
    public TalentPoolRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}