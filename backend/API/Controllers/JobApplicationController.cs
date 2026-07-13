using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Application.DTOs.JobApplications;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobApplicationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public JobApplicationsController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // ==========================================================
    // GET: api/JobApplications
    // Admin
    // Recruiter
    // Hiring Manager
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager")]
    [HttpGet]
    public async Task<IActionResult> GetAllApplications()
    {
        var applications = await _unitOfWork.Applications.GetAllAsync();

        var response =
            _mapper.Map<List<JobApplicationResponseDto>>(applications);

        return Ok(response);
    }

    // ==========================================================
    // GET: api/JobApplications/my
    // Candidate Only
    // ==========================================================

    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyApplications()
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new
            {
                message = "Invalid token."
            });

        var candidate =
            await _unitOfWork.Candidates.GetByUserIdAsync(
                Guid.Parse(userIdClaim));

        if (candidate == null)
            return BadRequest(new
            {
                message = "Candidate profile not found."
            });

        var applications =
            await _unitOfWork.Applications
                .GetByCandidateIdAsync(candidate.Id);

        var response =
            _mapper.Map<List<JobApplicationResponseDto>>(applications);

        return Ok(response);
    }

    // ==========================================================
    // GET: api/JobApplications/{id}
    // Admin
    // Recruiter
    // Hiring Manager
    // Candidate (Own Application Only)
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplication(Guid id)
    {
        var application =
            await _unitOfWork.Applications
                .GetWithCandidateDetailsAsync(id);

        if (application == null)
            return NotFound(new
            {
                message = "Application not found."
            });

        // Admin & Hiring Manager can view everything
        if (User.IsInRole("Admin") ||
            User.IsInRole("HiringManager"))
        {
            return Ok(
                _mapper.Map<JobApplicationResponseDto>(application));
        }

        // Recruiter can only view applications for own jobs
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

            if (application.Job.RecruiterId != recruiter.Id)
                return Forbid();

            return Ok(
                _mapper.Map<JobApplicationResponseDto>(application));
        }

        // Candidate can only view own application
        var candidateUserId =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(candidateUserId))
            return Unauthorized();

        var candidateProfile =
            await _unitOfWork.Candidates
                .GetByUserIdAsync(Guid.Parse(candidateUserId));

        if (candidateProfile == null)
            return BadRequest(new
            {
                message = "Candidate profile not found."
            });

        if (application.CandidateId != candidateProfile.Id)
            return Forbid();

        return Ok(
            _mapper.Map<JobApplicationResponseDto>(application));
    }

        // ==========================================================
    // POST: api/JobApplications
    // Candidate Only
    // ==========================================================

    [Authorize(Roles = "Candidate")]
    [HttpPost]
    public async Task<IActionResult> ApplyJob(CreateJobApplicationDto dto)
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new
            {
                message = "Invalid token."
            });

        var candidate =
            await _unitOfWork.Candidates
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
            return BadRequest(new
            {
                message = "Candidate profile not found."
            });

        // Check Job
        var job =
            await _unitOfWork.Jobs.GetByIdAsync(dto.JobId);

        if (job == null)
            return NotFound(new
            {
                message = "Job not found."
            });

        // Check Resume
        if (dto.ResumeId.HasValue)
        {
            var resume =
                await _unitOfWork.Resumes
                    .GetByIdAsync(dto.ResumeId.Value);

            if (resume == null)
                return NotFound(new
                {
                    message = "Resume not found."
                });
        }

        // Duplicate Apply Check
        var alreadyApplied =
            await _unitOfWork.Applications
                .IsAlreadyAppliedAsync(candidate.Id, dto.JobId);

        if (alreadyApplied)
            return BadRequest(new
            {
                message = "You have already applied for this job."
            });

        var application = new JobApplication
        {
            CandidateId = candidate.Id,
            JobId = dto.JobId,
            ResumeId = dto.ResumeId,
            CoverLetter = dto.CoverLetter,
            AppliedAt = DateTime.UtcNow,
            Status = ApplicationStatus.Pending
        };

        await _unitOfWork.Applications.AddAsync(application);
        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<JobApplicationResponseDto>(application);

        return CreatedAtAction(
            nameof(GetApplication),
            new { id = response.Id },
            response);
    }

    // ==========================================================
    // PUT: api/JobApplications/{id}
    // Recruiter
    // Admin
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplication(
        Guid id,
        UpdateJobApplicationDto dto)
    {
        var application =
            await _unitOfWork.Applications
                .GetWithCandidateDetailsAsync(id);

        if (application == null)
            return NotFound(new
            {
                message = "Application not found."
            });

        // Recruiter can update only own job applications
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

            if (application.Job.RecruiterId != recruiter.Id)
                return Forbid();
        }

        application.Status = dto.Status;
        application.RecruiterNotes = dto.RecruiterNotes;

        _unitOfWork.Applications.Update(application);

        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<JobApplicationResponseDto>(application);

        return Ok(new
        {
            message = "Application updated successfully.",
            application = response
        });
    }

        // ==========================================================
    // DELETE: api/JobApplications/{id}
    // Candidate Only (Own Application)
    // ==========================================================

    [Authorize(Roles = "Candidate")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(Guid id)
    {
        var application =
            await _unitOfWork.Applications
                .GetByIdAsync(id);

        if (application == null)
        {
            return NotFound(new
            {
                message = "Application not found."
            });
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

        var candidate =
            await _unitOfWork.Candidates
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (candidate == null)
        {
            return BadRequest(new
            {
                message = "Candidate profile not found."
            });
        }

        // Candidate can delete only own application
        if (application.CandidateId != candidate.Id)
        {
            return Forbid();
        }

        _unitOfWork.Applications.Delete(application);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Application deleted successfully."
        });
    }
}