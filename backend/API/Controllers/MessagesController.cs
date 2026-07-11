using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MessagesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/messages
    [HttpGet]
    public async Task<IActionResult> GetAllMessages()
    {
        var messages = await _unitOfWork.Messages.GetAllAsync();
        return Ok(messages);
    }

    // GET: api/messages/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessageById(Guid id)
    {
        var message = await _unitOfWork.Messages.GetByIdAsync(id);

        if (message == null)
            return NotFound(new { message = "Message not found." });

        return Ok(message);
    }

    // GET: api/messages/conversation/{senderId}/{receiverId}
    [HttpGet("conversation/{senderId}/{receiverId}")]
    public async Task<IActionResult> GetConversation(Guid senderId, Guid receiverId)
    {
        var conversation = await _unitOfWork.Messages.GetConversationAsync(senderId, receiverId);
        return Ok(conversation);
    }

    // GET: api/messages/unread/{userId}
    [HttpGet("unread/{userId}")]
    public async Task<IActionResult> GetUnreadCount(Guid userId)
    {
        var count = await _unitOfWork.Messages.GetUnreadCountAsync(userId);
        return Ok(new { unreadMessages = count });
    }

    // POST: api/messages
    [HttpPost]
    public async Task<IActionResult> CreateMessage(Message message)
    {
        message.SentAt = DateTime.UtcNow;

        await _unitOfWork.Messages.AddAsync(message);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetMessageById),
            new { id = message.Id },
            message);
    }

    // PUT: api/messages/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMessage(Guid id, Message message)
    {
        if (id != message.Id)
            return BadRequest(new { message = "Message ID mismatch." });

        var existingMessage = await _unitOfWork.Messages.GetByIdAsync(id);

        if (existingMessage == null)
            return NotFound(new { message = "Message not found." });

        _unitOfWork.Messages.Update(message);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Message updated successfully."
        });
    }

    // PUT: api/messages/read/{id}
    [HttpPut("read/{id}")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        await _unitOfWork.Messages.MarkAsReadAsync(id);

        return Ok(new
        {
            message = "Message marked as read."
        });
    }

    // DELETE: api/messages/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(Guid id)
    {
        var message = await _unitOfWork.Messages.GetByIdAsync(id);

        if (message == null)
            return NotFound(new { message = "Message not found." });

        _unitOfWork.Messages.Delete(message);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Message deleted successfully."
        });
    }
}