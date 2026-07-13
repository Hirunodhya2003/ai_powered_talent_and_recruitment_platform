using Application.DTOs.Education;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EducationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EducationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ======================================
    // Recruiter/Admin - Get All Educations
    // ======================================

    [Authorize(Roles = "Recruiter,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllEducations()
    {
        var educations = await _unitOfWork.Educations.GetAllAsync();
        return Ok(educations);
    }

    // ======================================
    // Candidate - Get My Educations
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyEducations()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var educations = await _unitOfWork.Educations
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        return Ok(educations);
    }

    // ======================================
    // Recruiter/Admin - Get Education By Id
    // ======================================

    [Authorize(Roles = "Recruiter,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEducationById(Guid id)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id);

        if (education == null)
            return NotFound(new { message = "Education not found." });

        return Ok(education);
    }

    // ======================================
    // Candidate - Create Education
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpPost]
    public async Task<IActionResult> CreateEducation(CreateEducationDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var userId = Guid.Parse(userIdClaim);

        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user == null)
            return NotFound(new { message = "User not found." });

        var education = new Education
        {
            UserId = userId,
            Institution = dto.Institution,
            Degree = dto.Degree,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };

        await _unitOfWork.Educations.AddAsync(education);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEducationById),
            new { id = education.Id },
            education);
    }

    // ======================================
    // Candidate - Update Own Education
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEducation(Guid id, UpdateEducationDto dto)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id);

        if (education == null)
            return NotFound(new { message = "Education not found." });

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (education.UserId != userId)
            return Forbid();

        education.Institution = dto.Institution;
        education.Degree = dto.Degree;
        education.StartDate = dto.StartDate;
        education.EndDate = dto.EndDate;

        _unitOfWork.Educations.Update(education);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Education updated successfully.",
            education
        });
    }

    // ======================================
    // Candidate - Delete Own Education
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(Guid id)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id);

        if (education == null)
            return NotFound(new { message = "Education not found." });

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (education.UserId != userId)
            return Forbid();

        _unitOfWork.Educations.Delete(education);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Education deleted successfully."
        });
    }
}