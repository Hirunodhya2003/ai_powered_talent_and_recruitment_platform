using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Services;

public class AuditService : IAuditService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task LogAsync(
        Guid userId,
        string action,
        string entity,
        string? entityId,
        string? description,
        string? ipAddress)
    {
        var audit = new AuditLog
        {
            UserId = userId,
            Action = action,
            Entity = entity,
            EntityId = entityId,
            Description = description,
            IpAddress = ipAddress
        };

        await _unitOfWork.AuditLogs.AddAsync(audit);

        await _unitOfWork.SaveChangesAsync();
    }
}