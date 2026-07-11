namespace Application.Interfaces;

public interface IAuditService
{
    Task LogAsync(
        Guid userId,
        string action,
        string entity,
        string? entityId,
        string? description,
        string? ipAddress);
}