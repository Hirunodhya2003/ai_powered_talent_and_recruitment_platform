using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public NotificationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/notifications
    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications = await _unitOfWork.Notifications.GetAllAsync();
        return Ok(notifications);
    }

    // GET: api/notifications/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(Guid id)
    {
        var notification = await _unitOfWork.Notifications.GetByIdAsync(id);

        if (notification == null)
            return NotFound(new { message = "Notification not found." });

        return Ok(notification);
    }

    // POST: api/notifications
    [HttpPost]
    public async Task<IActionResult> CreateNotification(Notification notification)
    {
        await _unitOfWork.Notifications.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetNotificationById),
            new { id = notification.Id },
            notification);
    }

    // PUT: api/notifications/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(Guid id, Notification notification)
    {
        if (id != notification.Id)
            return BadRequest(new { message = "Notification ID mismatch." });

        var existingNotification = await _unitOfWork.Notifications.GetByIdAsync(id);

        if (existingNotification == null)
            return NotFound(new { message = "Notification not found." });

        _unitOfWork.Notifications.Update(notification);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Notification updated successfully."
        });
    }

    // DELETE: api/notifications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        var notification = await _unitOfWork.Notifications.GetByIdAsync(id);

        if (notification == null)
            return NotFound(new { message = "Notification not found." });

        _unitOfWork.Notifications.Delete(notification);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Notification deleted successfully."
        });
    }
}