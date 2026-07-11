using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Candidate;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CandidatesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/candidates
    [HttpGet]
    public async Task<IActionResult> GetAllCandidates()
    {
        var candidates = await _unitOfWork.Candidates.GetAllAsync();
        return Ok(candidates);
    }

    // GET: api/candidates/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCandidateById(Guid id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
            return NotFound(new { message = "Candidate not found." });

        return Ok(candidate);
    }

    // POST: api/candidates
    [HttpPost]
    public async Task<IActionResult> CreateCandidate(Candidate candidate)
    {
        await _unitOfWork.Candidates.AddAsync(candidate);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetCandidateById),
            new { id = candidate.Id },
            candidate);
    }

    // PUT: api/candidates/{id}
  // PUT: api/candidates/{id}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateCandidate(
    Guid id,
    UpdateCandidateProfileDto dto)
{
    var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

    if (candidate == null)
        return NotFound(new { message = "Candidate not found." });

    candidate.ProfessionalHeadline = dto.ProfessionalHeadline;
    candidate.Summary = dto.Summary;
    candidate.PhoneNumber = dto.PhoneNumber;
    candidate.Location = dto.Location;
    candidate.IsOpenToWork = dto.IsOpenToWork;
    candidate.PreferredJobRole = dto.PreferredJobRole;
    candidate.EmploymentType = dto.EmploymentType;
    candidate.PreferredLocation = dto.PreferredLocation;
    candidate.WorkPreference = dto.WorkPreference;

    _unitOfWork.Candidates.Update(candidate);
    await _unitOfWork.SaveChangesAsync();

    return Ok(new
    {
        message = "Candidate profile updated successfully.",
        candidate
    });
}
    // DELETE: api/candidates/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCandidate(Guid id)
    {
        var candidate = await _unitOfWork.Candidates.GetByIdAsync(id);

        if (candidate == null)
            return NotFound(new { message = "Candidate not found." });

        _unitOfWork.Candidates.Delete(candidate);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Candidate deleted successfully."
        });
    }
}