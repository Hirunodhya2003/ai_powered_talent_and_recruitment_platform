using Application.DTOs.Experience;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExperienceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ExperienceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ======================================
    // Recruiter/Admin - View All Experiences
    // ======================================

    [Authorize(Roles = "Recruiter,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllExperiences()
    {
        var experiences = await _unitOfWork.Experiences.GetAllAsync();
        return Ok(experiences);
    }

    // ======================================
    // Candidate - My Experiences
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyExperiences()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var experiences = await _unitOfWork.Experiences.GetByUserIdAsync(Guid.Parse(userIdClaim));

        return Ok(experiences);
    }

    // ======================================
    // Recruiter/Admin - View Experience By Id
    // ======================================

    [Authorize(Roles = "Recruiter,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetExperienceById(Guid id)
    {
        var experience = await _unitOfWork.Experiences.GetByIdAsync(id);

        if (experience == null)
            return NotFound(new { message = "Experience not found." });

        return Ok(experience);
    }

    // ======================================
    // Candidate - Create Experience
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpPost]
    public async Task<IActionResult> CreateExperience(CreateExperienceDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var experience = new Experience
        {
            UserId = Guid.Parse(userIdClaim),
            Company = dto.Company,
            Position = dto.Position,
            EmploymentType = dto.EmploymentType,
            Location = dto.Location,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            IsCurrentJob = dto.IsCurrentJob,
            Description = dto.Description
        };

        await _unitOfWork.Experiences.AddAsync(experience);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetExperienceById),
            new { id = experience.Id },
            experience);
    }

    // ======================================
    // Candidate - Update Experience
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExperience(Guid id, UpdateExperienceDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var experience = await _unitOfWork.Experiences.GetByIdAsync(id);

        if (experience == null)
            return NotFound(new { message = "Experience not found." });

        if (experience.UserId != Guid.Parse(userIdClaim))
            return Forbid();

        experience.Company = dto.Company;
        experience.Position = dto.Position;
        experience.EmploymentType = dto.EmploymentType;
        experience.Location = dto.Location;
        experience.StartDate = dto.StartDate;
        experience.EndDate = dto.EndDate;
        experience.IsCurrentJob = dto.IsCurrentJob;
        experience.Description = dto.Description;

        _unitOfWork.Experiences.Update(experience);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Experience updated successfully.",
            experience
        });
    }

    // ======================================
    // Candidate - Delete Experience
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperience(Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var experience = await _unitOfWork.Experiences.GetByIdAsync(id);

        if (experience == null)
            return NotFound(new { message = "Experience not found." });

        if (experience.UserId != Guid.Parse(userIdClaim))
            return Forbid();

        _unitOfWork.Experiences.Delete(experience);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Experience deleted successfully."
        });
    }
}