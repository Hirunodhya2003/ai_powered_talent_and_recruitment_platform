namespace Application.DTOs.Jobs;

public class JobResponseDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string EmploymentType { get; set; } = string.Empty;

    public decimal MinimumSalary { get; set; }

    public decimal MaximumSalary { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public Guid RecruiterId { get; set; }

    public Guid OrganizationId { get; set; }
}