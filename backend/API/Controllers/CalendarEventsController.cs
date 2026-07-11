using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalendarEventsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CalendarEventsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCalendarEvents()
    {
        var eventsList = await _unitOfWork.CalendarEvents.GetAllAsync();
        return Ok(eventsList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCalendarEventById(Guid id)
    {
        var calendarEvent = await _unitOfWork.CalendarEvents.GetByIdAsync(id);

        if (calendarEvent == null)
            return NotFound(new { message = "Calendar Event not found." });

        return Ok(calendarEvent);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCalendarEvent(CalendarEvent calendarEvent)
    {
        await _unitOfWork.CalendarEvents.AddAsync(calendarEvent);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCalendarEventById),
            new { id = calendarEvent.Id },
            calendarEvent);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCalendarEvent(Guid id, CalendarEvent calendarEvent)
    {
        if (id != calendarEvent.Id)
            return BadRequest(new { message = "Calendar Event ID mismatch." });

        var existing = await _unitOfWork.CalendarEvents.GetByIdAsync(id);

        if (existing == null)
            return NotFound(new { message = "Calendar Event not found." });

        _unitOfWork.CalendarEvents.Update(calendarEvent);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Calendar Event updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCalendarEvent(Guid id)
    {
        var calendarEvent = await _unitOfWork.CalendarEvents.GetByIdAsync(id);

        if (calendarEvent == null)
            return NotFound(new { message = "Calendar Event not found." });

        _unitOfWork.CalendarEvents.Delete(calendarEvent);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Calendar Event deleted successfully."
        });
    }
}