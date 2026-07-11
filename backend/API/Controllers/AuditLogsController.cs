using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditLogsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditLogsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/auditlogs
    [HttpGet]
    public async Task<IActionResult> GetAllAuditLogs()
    {
        var logs = await _unitOfWork.AuditLogs.GetAllLogsAsync();
        return Ok(logs);
    }

    // GET: api/auditlogs/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuditLogById(Guid id)
    {
        var log = await _unitOfWork.AuditLogs.GetByIdAsync(id);

        if (log == null)
            return NotFound(new { message = "Audit Log not found." });

        return Ok(log);
    }

    // GET: api/auditlogs/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetLogsByUser(Guid userId)
    {
        var logs = await _unitOfWork.AuditLogs.GetByUserIdAsync(userId);
        return Ok(logs);
    }

    // GET: api/auditlogs/action/{action}
    [HttpGet("action/{action}")]
    public async Task<IActionResult> GetLogsByAction(string action)
    {
        var logs = await _unitOfWork.AuditLogs.GetByActionAsync(action);
        return Ok(logs);
    }

    // GET: api/auditlogs/daterange?from=2026-01-01&to=2026-12-31
    [HttpGet("daterange")]
    public async Task<IActionResult> GetLogsByDateRange(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        var logs = await _unitOfWork.AuditLogs.GetLogsByDateRangeAsync(from, to);
        return Ok(logs);
    }
}