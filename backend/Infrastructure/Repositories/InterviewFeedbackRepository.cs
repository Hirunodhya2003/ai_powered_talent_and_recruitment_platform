using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class InterviewFeedbackRepository
    : GenericRepository<InterviewFeedback>, IInterviewFeedbackRepository
{
    public InterviewFeedbackRepository(RecruitmentDbContext context)
        : base(context)
    {
    }
}