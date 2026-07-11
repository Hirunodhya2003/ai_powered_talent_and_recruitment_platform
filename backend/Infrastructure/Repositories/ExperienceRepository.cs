using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ExperienceRepository
    : GenericRepository<Experience>, IExperienceRepository
{
    public ExperienceRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}