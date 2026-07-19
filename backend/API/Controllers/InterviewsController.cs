using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Application.DTOs.Interviews;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public partial class InterviewsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InterviewsController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // ==========================================================
    // GET ALL INTERVIEWS
    // Admin
    // Recruiter
    // Hiring Manager
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager")]
    [HttpGet]
    public async Task<IActionResult> GetAllInterviews()
    {
        var interviews = await _unitOfWork.Interviews.GetAllAsync();

        var response =
            _mapper.Map<List<InterviewResponseDto>>(interviews);

        return Ok(response);
    }

    // ==========================================================
    // GET INTERVIEW BY ID
    // Admin
    // Recruiter (Own)
    // Hiring Manager
    // Candidate (Own)
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInterviewById(Guid id)
    {
        var interview =
            await _unitOfWork.Interviews.GetByIdAsync(id);

        if (interview == null)
            return NotFound(new
            {
                message = "Interview not found."
            });

        // Admin & HiringManager
        if (User.IsInRole("Admin") ||
            User.IsInRole("HiringManager"))
        {
            var response =
                _mapper.Map<InterviewResponseDto>(interview);

            return Ok(response);
        }

        // Recruiter
        if (User.IsInRole("Recruiter"))
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            var recruiter =
                await _unitOfWork.Recruiters
                    .GetByUserIdAsync(Guid.Parse(userIdClaim));

            if (recruiter == null)
                return BadRequest(new
                {
                    message = "Recruiter profile not found."
                });

            if (interview.RecruiterId != recruiter.Id)
                return Forbid();

            return Ok(
                _mapper.Map<InterviewResponseDto>(interview));
        }

        // Candidate
        var candidateUserId =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(candidateUserId))
            return Unauthorized();

        var candidate =
            await _unitOfWork.Candidates
                .GetByUserIdAsync(Guid.Parse(candidateUserId));

        if (candidate == null)
            return BadRequest(new
            {
                message = "Candidate profile not found."
            });

        if (interview.CandidateId != candidate.Id)
            return Forbid();

        return Ok(
            _mapper.Map<InterviewResponseDto>(interview));
    }

        // ==========================================================
    // CREATE INTERVIEW
    // Admin
    // Recruiter
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter")]
    [HttpPost]
    public async Task<IActionResult> CreateInterview(CreateInterviewDto dto)
    {
        var application = await _unitOfWork.Applications
            .GetByIdAsync(dto.ApplicationId);

        if (application == null)
        {
            return NotFound(new
            {
                message = "Job application not found."
            });
        }

        // Load Job
        var job = await _unitOfWork.Jobs
            .GetByIdAsync(application.JobId);

        if (job == null)
        {
            return NotFound(new
            {
                message = "Job not found."
            });
        }

        Guid recruiterId;

        if (User.IsInRole("Admin"))
        {
            recruiterId = job.RecruiterId;
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

            var recruiter = await _unitOfWork.Recruiters
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

            if (recruiter == null)
            {
                return BadRequest(new
                {
                    message = "Recruiter profile not found."
                });
            }

            // Recruiter can schedule interviews only for own jobs
            if (job.RecruiterId != recruiter.Id)
            {
                return Forbid();
            }

            recruiterId = recruiter.Id;
        }

        var interview = new Interview
        {
            ApplicationId = application.Id,
            CandidateId = application.CandidateId,
            RecruiterId = recruiterId,
            InterviewDate = dto.InterviewDate,
            InterviewTime = dto.InterviewTime,
            Location = dto.Location,
            MeetingLink = dto.MeetingLink
        };

        await _unitOfWork.Interviews.AddAsync(interview);
        await _unitOfWork.SaveChangesAsync();

        var response = _mapper.Map<InterviewResponseDto>(interview);

        return CreatedAtAction(
            nameof(GetInterviewById),
            new { id = response.Id },
            response);
    }

    // ==========================================================
    // GET MY INTERVIEWS
    // Candidate
    // ==========================================================

    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyInterviews()
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

        var candidate = await _unitOfWork.Candidates
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
        {
            return BadRequest(new
            {
                message = "Candidate profile not found."
            });
        }

        var interviews = await _unitOfWork.Interviews
            .GetByCandidateIdAsync(candidate.Id);

        var response = _mapper.Map<List<InterviewResponseDto>>(interviews);

        return Ok(response);
    }

        // ==========================================================
    // UPDATE INTERVIEW
    // Admin
    // Recruiter (Own Interviews Only)
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInterview(
        Guid id,
        UpdateInterviewDto dto)
    {
        var interview = await _unitOfWork.Interviews
            .GetByIdAsync(id);

        if (interview == null)
        {
            return NotFound(new
            {
                message = "Interview not found."
            });
        }

        if (!User.IsInRole("Admin"))
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

            var recruiter = await _unitOfWork.Recruiters
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

            if (recruiter == null)
            {
                return BadRequest(new
                {
                    message = "Recruiter profile not found."
                });
            }

            if (interview.RecruiterId != recruiter.Id)
            {
                return Forbid();
            }
        }

        interview.InterviewDate = dto.InterviewDate;
        interview.InterviewTime = dto.InterviewTime;
        interview.Location = dto.Location;
        interview.MeetingLink = dto.MeetingLink;

        _unitOfWork.Interviews.Update(interview);
        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<InterviewResponseDto>(interview);

        return Ok(new
        {
            message = "Interview updated successfully.",
            interview = response
        });
    }

    // ==========================================================
    // DELETE INTERVIEW
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInterview(Guid id)
    {
        var interview = await _unitOfWork.Interviews
            .GetByIdAsync(id);

        if (interview == null)
        {
            return NotFound(new
            {
                message = "Interview not found."
            });
        }

        _unitOfWork.Interviews.Delete(interview);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Interview deleted successfully."
        });
    }
}


