using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public SkillsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSkills()
    {
        var skills = await _unitOfWork.Skills.GetAllAsync();
        return Ok(skills);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkillById(Guid id)
    {
        var skill = await _unitOfWork.Skills.GetByIdAsync(id);

        if (skill == null)
            return NotFound(new { message = "Skill not found." });

        return Ok(skill);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSkill(Skill skill)
    {
        await _unitOfWork.Skills.AddAsync(skill);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetSkillById),
            new { id = skill.Id },
            skill);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkill(Guid id, Skill skill)
    {
        if (id != skill.Id)
            return BadRequest(new { message = "Skill ID mismatch." });

        var existingSkill = await _unitOfWork.Skills.GetByIdAsync(id);

        if (existingSkill == null)
            return NotFound(new { message = "Skill not found." });

        _unitOfWork.Skills.Update(skill);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill updated successfully."
        });
    }

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