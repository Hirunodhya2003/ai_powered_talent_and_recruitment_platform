using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class EducationRepository
    : GenericRepository<Education>, IEducationRepository
{
    public EducationRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}