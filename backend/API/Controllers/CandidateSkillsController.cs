using Application.DTOs.CandidateSkill;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CandidateSkillsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CandidateSkillsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ==========================================
    // Recruiter / HiringManager / Admin
    // Get All Candidate Skills
    // ==========================================
    [Authorize(Roles = "Recruiter,HiringManager,Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllCandidateSkills()
    {
        var skills = await _unitOfWork.CandidateSkills.GetAllAsync();
        return Ok(skills);
    }

    // ==========================================
    // Candidate
    // Get My Skills
    // ==========================================
    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMySkills()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return NotFound(new { message = "Candidate not found." });

        var skills = await _unitOfWork.CandidateSkills
            .GetSkillsByCandidateIdAsync(candidate.Id);

        return Ok(skills);
    }

    // ==========================================
    // Recruiter / HiringManager / Admin
    // Get Candidate Skill By Id
    // ==========================================
    [Authorize(Roles = "Recruiter,HiringManager,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCandidateSkillById(Guid id)
    {
        var candidateSkill =
            await _unitOfWork.CandidateSkills.GetByIdAsync(id);

        if (candidateSkill == null)
            return NotFound(new { message = "Candidate skill not found." });

        return Ok(candidateSkill);
    }

    // ==========================================
    // Candidate
    // Add Skill
    // ==========================================
    [Authorize(Roles = "Candidate")]
    [HttpPost]
    public async Task<IActionResult> AddSkill(CreateCandidateSkillDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return NotFound(new { message = "Candidate not found." });

        var skill = await _unitOfWork.Skills.GetByIdAsync(dto.SkillId);

        if (skill == null)
            return NotFound(new { message = "Skill not found." });

        var alreadyExists =
            await _unitOfWork.CandidateSkills
                .ExistsAsync(candidate.Id, dto.SkillId);

        if (alreadyExists)
            return BadRequest(new { message = "Skill already added." });

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
    // Candidate
    // Update My Skill
    // ==========================================
    [Authorize(Roles = "Candidate")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSkill(Guid id, UpdateCandidateSkillDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return NotFound(new { message = "Candidate not found." });

        var candidateSkill =
            await _unitOfWork.CandidateSkills.GetByIdAsync(id);

        if (candidateSkill == null)
            return NotFound(new { message = "Candidate skill not found." });

        if (candidateSkill.CandidateId != candidate.Id)
            return Forbid();

        candidateSkill.ProficiencyLevel = dto.ProficiencyLevel;

        _unitOfWork.CandidateSkills.Update(candidateSkill);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill updated successfully."
        });
    }

    // ==========================================
    // Candidate
    // Delete My Skill
    // ==========================================
    [Authorize(Roles = "Candidate")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveSkill(Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized();

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return NotFound(new { message = "Candidate not found." });

        var candidateSkill =
            await _unitOfWork.CandidateSkills.GetByIdAsync(id);

        if (candidateSkill == null)
            return NotFound(new { message = "Candidate skill not found." });

        if (candidateSkill.CandidateId != candidate.Id)
            return Forbid();

        _unitOfWork.CandidateSkills.Delete(candidateSkill);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Skill removed successfully."
        });
    }
}