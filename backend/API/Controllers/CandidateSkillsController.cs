using Application.DTOs.CandidateSkill;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CandidateSkillsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CandidateSkillsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ==========================================
    // GET Logged-in Candidate Skills
    // ==========================================
    [HttpGet]
    public async Task<IActionResult> GetMySkills()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return NotFound("Candidate not found.");

        var skills = await _unitOfWork.CandidateSkills
            .GetSkillsByCandidateIdAsync(candidate.Id);

        return Ok(skills);
    }

    // ==========================================
    // ADD Skill
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> AddSkill(CreateCandidateSkillDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return NotFound("Candidate not found.");

        var skill = await _unitOfWork.Skills.GetByIdAsync(dto.SkillId);

        if (skill == null)
            return NotFound("Skill not found.");

        var alreadyExists =
            await _unitOfWork.CandidateSkills
                .ExistsAsync(candidate.Id, dto.SkillId);

        if (alreadyExists)
            return BadRequest("Skill already added.");

        var candidateSkill = new CandidateSkill
        {
            CandidateId = candidate.Id,
            SkillId = dto.SkillId,
            ProficiencyLevel = dto.ProficiencyLevel
        };

        await _unitOfWork.CandidateSkills.AddAsync(candidateSkill);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill added successfully."
        });
    }

    // ==========================================
    // UPDATE Skill
    // ==========================================
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkill(
        Guid id,
        UpdateCandidateSkillDto dto)
    {
        var candidateSkill =
            await _unitOfWork.CandidateSkills.GetByIdAsync(id);

        if (candidateSkill == null)
            return NotFound("Skill not found.");

        candidateSkill.ProficiencyLevel = dto.ProficiencyLevel;

        _unitOfWork.CandidateSkills.Update(candidateSkill);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill updated successfully."
        });
    }

    // ==========================================
    // DELETE Skill
    // ==========================================
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveSkill(Guid id)
    {
        var candidateSkill =
            await _unitOfWork.CandidateSkills.GetByIdAsync(id);

        if (candidateSkill == null)
            return NotFound("Skill not found.");

        _unitOfWork.CandidateSkills.Delete(candidateSkill);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill removed successfully."
        });
    }
}