using Application.DTOs.Skill;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ======================================
    // Candidate / Recruiter / HiringManager / Admin
    // Get All Skills
    // ======================================

    [Authorize(Roles = "Candidate,Recruiter,HiringManager,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllSkills()
    {
        var skills = await _unitOfWork.Skills.GetAllAsync();
        return Ok(skills);
    }

    // ======================================
    // Candidate / Recruiter / HiringManager / Admin
    // Get Skill By Id
    // ======================================

    [Authorize(Roles = "Candidate,Recruiter,HiringManager,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkillById(Guid id)
    {
        var skill = await _unitOfWork.Skills.GetByIdAsync(id);

        if (skill == null)
            return NotFound(new { message = "Skill not found." });

        return Ok(skill);
    }

    // ======================================
    // Admin Only
    // Create Skill
    // ======================================

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateSkill(CreateSkillDto dto)
    {
        var skill = new Skill
        {
            Name = dto.Name
        };

        await _unitOfWork.Skills.AddAsync(skill);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetSkillById),
            new { id = skill.Id },
            skill);
    }

    // ======================================
    // Admin Only
    // Update Skill
    // ======================================

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkill(Guid id, UpdateSkillDto dto)
    {
        var existingSkill = await _unitOfWork.Skills.GetByIdAsync(id);

        if (existingSkill == null)
            return NotFound(new { message = "Skill not found." });

        existingSkill.Name = dto.Name;

        _unitOfWork.Skills.Update(existingSkill);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill updated successfully.",
            skill = existingSkill
        });
    }

    // ======================================
    // Admin Only
    // Delete Skill
    // ======================================

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(Guid id)
    {
        var skill = await _unitOfWork.Skills.GetByIdAsync(id);

        if (skill == null)
            return NotFound(new { message = "Skill not found." });

        _unitOfWork.Skills.Delete(skill);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill deleted successfully."
        });
    }
}