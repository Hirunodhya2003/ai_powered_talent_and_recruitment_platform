using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly RecruitmentDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(RecruitmentDbContext context)
    {
        _context = context;

        Users = new UserRepository(context);
        Candidates = new CandidateRepository(context);
        Jobs = new JobRepository(context);
        Organizations = new OrganizationRepository(context);

        RefreshTokens = new RefreshTokenRepository(context);

        CandidateSkills = new CandidateSkillRepository(context);

        // Remaining repositories
        Applications = new ApplicationRepository(context);
        Interviews = new InterviewRepository(context);
        Messages = new MessageRepository(context);
        Notifications = new NotificationRepository(context);
        AuditLogs = new AuditLogRepository(context);
        Recruiters = new RecruiterRepository(context);

        HiringManagers = new HiringManagerRepository(context);

        TalentPools = new TalentPoolRepository(context);

        Resumes = new ResumeRepository(context);

        Educations = new EducationRepository(context);

        Experiences = new ExperienceRepository(context);

        Skills = new SkillRepository(context);

        JobRequisitions = new JobRequisitionRepository(context);

        HiringDecisions = new HiringDecisionRepository(context);

        Evaluations = new EvaluationRepository(context);

        InterviewFeedbacks = new InterviewFeedbackRepository(context);

        CalendarEvents = new CalendarEventRepository(context);

        //CandidateSkills = new CandidateSkillRepository(context);
    }

    public IUserRepository Users { get; }

    public ICandidateRepository Candidates { get; }

    public IJobRepository Jobs { get; }

    public IRefreshTokenRepository RefreshTokens { get; }

    public IApplicationRepository Applications { get; }

    public IInterviewRepository Interviews { get; }

    public IMessageRepository Messages { get; }

    public INotificationRepository Notifications { get; }

    public IOrganizationRepository Organizations { get; }

    public IAuditLogRepository AuditLogs { get; }

    public IRecruiterRepository Recruiters { get; }

    public IHiringManagerRepository HiringManagers { get; }

    public ITalentPoolRepository TalentPools { get; }

    public IResumeRepository Resumes { get; }

    public IEducationRepository Educations { get; }

    public IExperienceRepository Experiences { get; }

    public ISkillRepository Skills { get; }

    public IJobRequisitionRepository JobRequisitions { get; }
    
    public IHiringDecisionRepository HiringDecisions { get; }

    public IEvaluationRepository Evaluations { get; }

    public IInterviewFeedbackRepository InterviewFeedbacks { get; }

    public ICalendarEventRepository CalendarEvents { get; }

    public ICandidateSkillRepository CandidateSkills { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }

    
}