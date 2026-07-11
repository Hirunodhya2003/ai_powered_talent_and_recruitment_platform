using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewFeedbacksController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public InterviewFeedbacksController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInterviewFeedbacks()
    {
        var feedbacks = await _unitOfWork.InterviewFeedbacks.GetAllAsync();
        return Ok(feedbacks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInterviewFeedbackById(Guid id)
    {
        var feedback = await _unitOfWork.InterviewFeedbacks.GetByIdAsync(id);

        if (feedback == null)
            return NotFound(new { message = "Interview Feedback not found." });

        return Ok(feedback);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInterviewFeedback(InterviewFeedback feedback)
    {
        await _unitOfWork.InterviewFeedbacks.AddAsync(feedback);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetInterviewFeedbackById),
            new { id = feedback.Id },
            feedback);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInterviewFeedback(Guid id, InterviewFeedback feedback)
    {
        if (id != feedback.Id)
            return BadRequest(new { message = "Interview Feedback ID mismatch." });

        var existing = await _unitOfWork.InterviewFeedbacks.GetByIdAsync(id);

        if (existing == null)
            return NotFound(new { message = "Interview Feedback not found." });

        _unitOfWork.InterviewFeedbacks.Update(feedback);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Interview Feedback updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInterviewFeedback(Guid id)
    {
        var feedback = await _unitOfWork.InterviewFeedbacks.GetByIdAsync(id);

        if (feedback == null)
            return NotFound(new { message = "Interview Feedback not found." });

        _unitOfWork.InterviewFeedbacks.Delete(feedback);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Interview Feedback deleted successfully."
        });
    }
}