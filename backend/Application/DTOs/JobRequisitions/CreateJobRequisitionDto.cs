namespace Application.DTOs.JobRequisitions;

public class CreateJobRequisitionDto
{
    public string JobTitle { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string? SalaryRange { get; set; }

    public string EmploymentType { get; set; } = string.Empty;

    public int NumberOfPositions { get; set; }

    public string? JobDescription { get; set; }
}