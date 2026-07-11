using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class SkillRepository
    : GenericRepository<Skill>, ISkillRepository
{
    public SkillRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}