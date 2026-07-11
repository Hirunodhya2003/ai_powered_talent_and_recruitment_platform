using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class JobRequisitionRepository
    : GenericRepository<JobRequisition>, IJobRequisitionRepository
{
    public JobRequisitionRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}