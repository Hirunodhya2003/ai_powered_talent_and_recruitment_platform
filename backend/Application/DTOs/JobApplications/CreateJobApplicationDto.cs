namespace Application.DTOs.JobApplications;

public class CreateJobApplicationDto
{
    public Guid JobId { get; set; }

    public Guid? ResumeId { get; set; }

    public string? CoverLetter { get; set; }
}