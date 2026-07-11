namespace Domain.Entities;

public class Candidate : BaseEntity
{
    public Guid UserId { get; set; }

    public string? ProfessionalHeadline { get; set; }

    public string? Summary { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Location { get; set; }

    public bool IsOpenToWork { get; set; }

    public string? PreferredJobRole { get; set; }

    public string? EmploymentType { get; set; }

    public string? PreferredLocation { get; set; }

    public string? WorkPreference { get; set; }

    public int ProfileCompleteness { get; set; }

    public User User { get; set; } = null!;

    public ICollection<JobApplication> JobApplications { get; set; }
    = new List<JobApplication>();

    public string? LinkedInUrl { get; set; }

public string? GithubUrl { get; set; }

public string? PortfolioUrl { get; set; }

public string? ProfileImageUrl { get; set; }

public DateOnly? DateOfBirth { get; set; }

public string? Gender { get; set; }

public ICollection<CandidateSkill> CandidateSkills { get; set; }
    = new List<CandidateSkill>();


    //public ICollection<JobApplication> JobApplications { get; set; }
   // = new List<JobApplication>();
}