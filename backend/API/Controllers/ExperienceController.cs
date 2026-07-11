using Application.DTOs.Experience;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExperienceController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ExperienceController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExperiences()
    {
        var experiences = await _unitOfWork.Experiences.GetAllAsync();
        return Ok(experiences);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExperienceById(Guid id)
    {
        var experience = await _unitOfWork.Experiences.GetByIdAsync(id);

        if (experience == null)
            return NotFound(new { message = "Experience not found." });

        return Ok(experience);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExperience(CreateExperienceDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var experience = new Experience
        {
            UserId = Guid.Parse(userIdClaim),
            Company = dto.Company,
            Position = dto.Position,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };

        await _unitOfWork.Experiences.AddAsync(experience);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetExperienceById),
            new { id = experience.Id },
            experience);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExperience(
        Guid id,
        UpdateExperienceDto dto)
    {
        var experience =
            await _unitOfWork.Experiences.GetByIdAsync(id);

        if (experience == null)
            return NotFound(new
            {
                message = "Experience not found."
            });

        experience.Company = dto.Company;
        experience.Position = dto.Position;
        experience.StartDate = dto.StartDate;
        experience.EndDate = dto.EndDate;

        _unitOfWork.Experiences.Update(experience);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Experience updated successfully.",
            experience
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExperience(Guid id)
    {
        var experience =
            await _unitOfWork.Experiences.GetByIdAsync(id);

        if (experience == null)
            return NotFound(new
            {
                message = "Experience not found."
            });

        _unitOfWork.Experiences.Delete(experience);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Experience deleted successfully."
        });
    }
}