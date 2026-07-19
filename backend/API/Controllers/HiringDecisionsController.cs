using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Application.DTOs.HiringDecisions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HiringDecisionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HiringDecisionsController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // ==========================================================
    // GET ALL HIRING DECISIONS
    // Admin
    // Recruiter
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter")]
    [HttpGet]
    public async Task<IActionResult> GetAllHiringDecisions()
    {
        var decisions =
            await _unitOfWork.HiringDecisions.GetAllAsync();

        var response =
            _mapper.Map<List<HiringDecisionResponseDto>>(decisions);

        return Ok(response);
    }

    // ==========================================================
    // GET HIRING DECISION BY ID
    // Admin
    // Recruiter
    // Hiring Manager (Owner)
    // Candidate (Own)
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHiringDecisionById(Guid id)
    {
        var decision =
            await _unitOfWork.HiringDecisions.GetByIdAsync(id);

        if (decision == null)
        {
            return NotFound(new
            {
                message = "Hiring decision not found."
            });
        }

        if (User.IsInRole("Admin") || User.IsInRole("Recruiter"))
        {
            var adminResponse =
                _mapper.Map<HiringDecisionResponseDto>(decision);

            return Ok(adminResponse);
        }

        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            return Unauthorized(new
            {
                message = "Invalid token."
            });
        }

        var userId = Guid.Parse(userIdClaim);

        if (User.IsInRole("HiringManager"))
        {
            var manager =
                await _unitOfWork.HiringManagers
                    .GetByUserIdAsync(userId);

            if (manager == null)
            {
                return BadRequest(new
                {
                    message = "Hiring Manager profile not found."
                });
            }

            if (decision.HiringManagerId != manager.Id)
            {
                return Forbid();
            }
        }

        if (User.IsInRole("Candidate"))
        {
            var candidate =
                await _unitOfWork.Candidates
                    .GetByUserIdAsync(userId);

            if (candidate == null)
            {
                return BadRequest(new
                {
                    message = "Candidate profile not found."
                });
            }

            var application =
                await _unitOfWork.Applications
                    .GetByIdAsync(decision.ApplicationId);

            if (application == null ||
                application.CandidateId != candidate.Id)
            {
                return Forbid();
            }
        }

        var response =
            _mapper.Map<HiringDecisionResponseDto>(decision);

        return Ok(response);
    }

    // ==========================================================
    // CREATE HIRING DECISION
    // Admin
    // Hiring Manager
    // ==========================================================

    [Authorize(Roles = "HiringManager")]
    [HttpPost]
    public async Task<IActionResult> CreateHiringDecision(
        CreateHiringDecisionDto dto)
    {
        var application =
            await _unitOfWork.Applications
                .GetByIdAsync(dto.ApplicationId);

        if (application == null)
        {
            return NotFound(new
            {
                message = "Job application not found."
            });
        }

        Guid hiringManagerId;

              /**  if (User.IsInRole("Admin"))
        {
            hiringManagerId = dto.HiringManagerId;
        }
        else
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                return Unauthorized(new
                {
                    message = "Invalid token."
                });
            }

            var manager =
                await _unitOfWork.HiringManagers
                    .GetByUserIdAsync(Guid.Parse(userIdClaim));

            if (manager == null)
            {
                return BadRequest(new
                {
                    message = "Hiring Manager profile not found."
                });
            }

            hiringManagerId = manager.Id;
        }**/

        var userIdClaim =
    User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
    User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

if (string.IsNullOrWhiteSpace(userIdClaim))
{
    return Unauthorized(new
    {
        message = "Invalid token."
    });
}

var manager =
    await _unitOfWork.HiringManagers
        .GetByUserIdAsync(Guid.Parse(userIdClaim));

if (manager == null)
{
    return BadRequest(new
    {
        message = "Hiring Manager profile not found."
    });
}

hiringManagerId = manager.Id;

        var decision = new HiringDecision
        {
            ApplicationId = dto.ApplicationId,
            HiringManagerId = hiringManagerId,
            AIMatchScore = dto.AIMatchScore,
            Notes = dto.Notes,
            DecisionDate = dto.DecisionDate ?? DateTime.UtcNow
        };

        await _unitOfWork.HiringDecisions.AddAsync(decision);
        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<HiringDecisionResponseDto>(decision);

        return CreatedAtAction(
            nameof(GetHiringDecisionById),
            new { id = response.Id },
            response);
    }

    // ==========================================================
    // GET MY HIRING DECISIONS
    // Hiring Manager
    // ==========================================================

    [Authorize(Roles = "HiringManager")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyHiringDecisions()
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            return Unauthorized(new
            {
                message = "Invalid token."
            });
        }

        var manager =
            await _unitOfWork.HiringManagers
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (manager == null)
        {
            return BadRequest(new
            {
                message = "Hiring Manager profile not found."
            });
        }

        var decisions =
            (await _unitOfWork.HiringDecisions.GetAllAsync())
            .Where(x => x.HiringManagerId == manager.Id)
            .ToList();

        var response =
            _mapper.Map<List<HiringDecisionResponseDto>>(decisions);

        return Ok(response);
    }

        // ==========================================================
    // UPDATE HIRING DECISION
    // Admin
    // Hiring Manager (Owner)
    // ==========================================================

    [Authorize(Roles = "Admin,HiringManager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHiringDecision(
        Guid id,
        UpdateHiringDecisionDto dto)
    {
        var decision =
            await _unitOfWork.HiringDecisions.GetByIdAsync(id);

        if (decision == null)
        {
            return NotFound(new
            {
                message = "Hiring decision not found."
            });
        }

        if (User.IsInRole("HiringManager"))
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
            {
                return Unauthorized(new
                {
                    message = "Invalid token."
                });
            }

            var manager =
                await _unitOfWork.HiringManagers
                    .GetByUserIdAsync(Guid.Parse(userIdClaim));

            if (manager == null)
            {
                return BadRequest(new
                {
                    message = "Hiring Manager profile not found."
                });
            }

            if (decision.HiringManagerId != manager.Id)
            {
                return Forbid();
            }
        }

        decision.AIMatchScore = dto.AIMatchScore;
        decision.Notes = dto.Notes;
        decision.DecisionDate = dto.DecisionDate;

        _unitOfWork.HiringDecisions.Update(decision);
        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<HiringDecisionResponseDto>(decision);

        return Ok(new
        {
            message = "Hiring decision updated successfully.",
            hiringDecision = response
        });
    }

    // ==========================================================
    // DELETE HIRING DECISION
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHiringDecision(Guid id)
    {
        var decision =
            await _unitOfWork.HiringDecisions.GetByIdAsync(id);

        if (decision == null)
        {
            return NotFound(new
            {
                message = "Hiring decision not found."
            });
        }

        _unitOfWork.HiringDecisions.Delete(decision);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Hiring decision deleted successfully."
        });
    }
}