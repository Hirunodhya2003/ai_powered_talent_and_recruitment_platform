namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ICandidateRepository Candidates { get; }
    IJobRepository Jobs { get; }

    IRefreshTokenRepository RefreshTokens { get; }
    IApplicationRepository Applications { get; }
    IInterviewRepository Interviews { get; }
    IMessageRepository Messages { get; }
    INotificationRepository Notifications { get; }
    IOrganizationRepository Organizations { get; }
    IAuditLogRepository AuditLogs { get; }

    IRecruiterRepository Recruiters { get; }

    IHiringManagerRepository HiringManagers { get; }

    ITalentPoolRepository TalentPools { get; }

    IResumeRepository Resumes { get; }

    IEducationRepository Educations { get; }

    IExperienceRepository Experiences { get; }

    ISkillRepository Skills { get; }

    IJobRequisitionRepository JobRequisitions { get; }

    IHiringDecisionRepository HiringDecisions { get; }

    IEvaluationRepository Evaluations { get; }

    IInterviewFeedbackRepository InterviewFeedbacks { get; }

    ICalendarEventRepository CalendarEvents { get; }

    ICandidateSkillRepository CandidateSkills { get; }


    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}