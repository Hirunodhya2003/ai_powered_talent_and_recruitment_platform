using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Application.DTOs.Notifications;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public NotificationsController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // ==========================================================
    // GET ALL NOTIFICATIONS
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications =
            await _unitOfWork.Notifications.GetAllAsync();

        var response =
            _mapper.Map<List<NotificationResponseDto>>(notifications);

        return Ok(response);
    }

    // ==========================================================
    // GET MY NOTIFICATIONS
    // Admin
    // Recruiter
    // HiringManager
    // Candidate
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyNotifications()
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            return Unauthorized(new
            {
                message = "Invalid token."
            });
        }

        var userId = Guid.Parse(userIdClaim);

        var notifications =
            (await _unitOfWork.Notifications.GetAllAsync())
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToList();

        var response =
            _mapper.Map<List<NotificationResponseDto>>(notifications);

        return Ok(response);
    }

    // ==========================================================
    // GET NOTIFICATION BY ID
    // Ownership Validation
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(Guid id)
    {
        var notification =
            await _unitOfWork.Notifications.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound(new
            {
                message = "Notification not found."
            });
        }

        if (!User.IsInRole("Admin"))
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                return Unauthorized(new
                {
                    message = "Invalid token."
                });
            }

            if (notification.UserId != Guid.Parse(userIdClaim))
            {
                return Forbid();
            }
        }

        var response =
            _mapper.Map<NotificationResponseDto>(notification);

        return Ok(response);
    }

        // ==========================================================
    // CREATE NOTIFICATION
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateNotification(
        CreateNotificationDto dto)
    {
        var notification = new Notification
        {
            UserId = dto.UserId,
            Title = dto.Title,
            MessageText = dto.Message,
            IsRead = false
        };

        await _unitOfWork.Notifications.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<NotificationResponseDto>(notification);

        return CreatedAtAction(
            nameof(GetNotificationById),
            new { id = response.Id },
            response);
    }

    // ==========================================================
    // MARK AS READ
    // Owner Only
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var notification =
            await _unitOfWork.Notifications.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound(new
            {
                message = "Notification not found."
            });
        }

        if (!User.IsInRole("Admin"))
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                return Unauthorized(new
                {
                    message = "Invalid token."
                });
            }

            if (notification.UserId != Guid.Parse(userIdClaim))
            {
                return Forbid();
            }
        }

        notification.IsRead = true;

        _unitOfWork.Notifications.Update(notification);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Notification marked as read."
        });
    }

    // ==========================================================
    // UPDATE NOTIFICATION
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(
        Guid id,
        UpdateNotificationDto dto)
    {
        var notification =
            await _unitOfWork.Notifications.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound(new
            {
                message = "Notification not found."
            });
        }

        notification.Title = dto.Title;
        notification.MessageText = dto.Message;
        notification.IsRead = dto.IsRead;

        _unitOfWork.Notifications.Update(notification);
        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<NotificationResponseDto>(notification);

        return Ok(new
        {
            message = "Notification updated successfully.",
            notification = response
        });
    }

        // ==========================================================
    // DELETE NOTIFICATION
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        var notification =
            await _unitOfWork.Notifications.GetByIdAsync(id);

        if (notification == null)
        {
            return NotFound(new
            {
                message = "Notification not found."
            });
        }

        _unitOfWork.Notifications.Delete(notification);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Notification deleted successfully."
        });
    }
}