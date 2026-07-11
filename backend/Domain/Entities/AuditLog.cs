namespace Domain.Entities;

public class AuditLog : BaseEntity
{
    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    // Action (LOGIN, LOGOUT, CREATE, UPDATE, DELETE...)
    public string Action { get; set; } = string.Empty;

    // Entity Name (User, Job, Candidate...)
    public string Entity { get; set; } = string.Empty;

    // Entity Id
    public string? EntityId { get; set; }

    // Description
    public string? Description { get; set; }

    // Client IP
    public string? IpAddress { get; set; }
}