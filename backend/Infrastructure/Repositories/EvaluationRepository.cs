using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class EvaluationRepository
    : GenericRepository<Evaluation>, IEvaluationRepository
{
    public EvaluationRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}