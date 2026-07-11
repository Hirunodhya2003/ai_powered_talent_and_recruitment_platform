using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecruitersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public RecruitersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/recruiters
    [HttpGet]
    public async Task<IActionResult> GetAllRecruiters()
    {
        var recruiters = await _unitOfWork.Recruiters.GetAllAsync();
        return Ok(recruiters);
    }

    // GET: api/recruiters/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecruiterById(Guid id)
    {
        var recruiter = await _unitOfWork.Recruiters.GetByIdAsync(id);

        if (recruiter == null)
            return NotFound(new { message = "Recruiter not found." });

        return Ok(recruiter);
    }

    // POST: api/recruiters
    [HttpPost]
    public async Task<IActionResult> CreateRecruiter(Recruiter recruiter)
    {
        await _unitOfWork.Recruiters.AddAsync(recruiter);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetRecruiterById),
            new { id = recruiter.Id },
            recruiter);
    }

    // PUT: api/recruiters/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecruiter(Guid id, Recruiter recruiter)
    {
        if (id != recruiter.Id)
            return BadRequest(new { message = "Recruiter ID mismatch." });

        var existingRecruiter = await _unitOfWork.Recruiters.GetByIdAsync(id);

        if (existingRecruiter == null)
            return NotFound(new { message = "Recruiter not found." });

        _unitOfWork.Recruiters.Update(recruiter);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Recruiter updated successfully."
        });
    }

    // DELETE: api/recruiters/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecruiter(Guid id)
    {
        var recruiter = await _unitOfWork.Recruiters.GetByIdAsync(id);

        if (recruiter == null)
            return NotFound(new { message = "Recruiter not found." });

        _unitOfWork.Recruiters.Delete(recruiter);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Recruiter deleted successfully."
        });
    }
}