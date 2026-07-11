using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public InterviewsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/interviews
    [HttpGet]
    public async Task<IActionResult> GetAllInterviews()
    {
        var interviews = await _unitOfWork.Interviews.GetAllAsync();
        return Ok(interviews);
    }

    // GET: api/interviews/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInterviewById(Guid id)
    {
        var interview = await _unitOfWork.Interviews.GetByIdAsync(id);

        if (interview == null)
            return NotFound(new { message = "Interview not found." });

        return Ok(interview);
    }

    // POST: api/interviews
    [HttpPost]
    public async Task<IActionResult> CreateInterview(Interview interview)
    {
        await _unitOfWork.Interviews.AddAsync(interview);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetInterviewById),
            new { id = interview.Id },
            interview);
    }

    // PUT: api/interviews/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInterview(Guid id, Interview interview)
    {
        if (id != interview.Id)
            return BadRequest(new { message = "Interview ID mismatch." });

        var existingInterview = await _unitOfWork.Interviews.GetByIdAsync(id);

        if (existingInterview == null)
            return NotFound(new { message = "Interview not found." });

        _unitOfWork.Interviews.Update(interview);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Interview updated successfully."
        });
    }

    // DELETE: api/interviews/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInterview(Guid id)
    {
        var interview = await _unitOfWork.Interviews.GetByIdAsync(id);

        if (interview == null)
            return NotFound(new { message = "Interview not found." });

        _unitOfWork.Interviews.Delete(interview);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Interview deleted successfully."
        });
    }
}