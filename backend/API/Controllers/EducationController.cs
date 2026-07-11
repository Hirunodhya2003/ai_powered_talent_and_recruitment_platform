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

    // GET: api/education
    [HttpGet]
    public async Task<IActionResult> GetAllEducations()
    {
        var educations = await _unitOfWork.Educations.GetAllAsync();
        return Ok(educations);
    }

    // GET: api/education/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEducationById(Guid id)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id);

        if (education == null)
            return NotFound(new { message = "Education not found." });

        return Ok(education);
    }

    // POST: api/education
    [HttpPost]
    public async Task<IActionResult> CreateEducation(CreateEducationDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "User is not authenticated." });

        var education = new Education
        {
            UserId = Guid.Parse(userIdClaim),
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

    // PUT: api/education/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEducation(Guid id, UpdateEducationDto dto)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id);

        if (education == null)
            return NotFound(new { message = "Education not found." });

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

    // DELETE: api/education/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducation(Guid id)
    {
        var education = await _unitOfWork.Educations.GetByIdAsync(id);

        if (education == null)
            return NotFound(new { message = "Education not found." });

        _unitOfWork.Educations.Delete(education);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Education deleted successfully."
        });
    }
}