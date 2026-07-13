using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Application.DTOs.JobRequisitions;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class JobRequisitionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public JobRequisitionsController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // ==========================================================
    // GET ALL REQUISITIONS
    // Admin
    // Recruiter
    // Hiring Manager
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager")]
    [HttpGet]
    public async Task<IActionResult> GetAllJobRequisitions()
    {
        var requisitions = await _unitOfWork.JobRequisitions.GetAllAsync();

        var response =
            _mapper.Map<List<JobRequisitionResponseDto>>(requisitions);

        return Ok(response);
    }

    // ==========================================================
    // GET REQUISITION BY ID
    // Admin
    // Recruiter
    // Hiring Manager (Own Only)
    // ==========================================================

    [Authorize(Roles = "Admin,Recruiter,HiringManager")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobRequisitionById(Guid id)
    {
        var requisition =
            await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
        {
            return NotFound(new
            {
                message = "Job Requisition not found."
            });
        }

        // Hiring Manager can view only own requisitions
        if (User.IsInRole("HiringManager"))
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim))
                return Unauthorized();

            var hiringManager =
                await _unitOfWork.HiringManagers
                    .GetByUserIdAsync(Guid.Parse(userIdClaim));

            if (hiringManager == null)
            {
                return BadRequest(new
                {
                    message = "Hiring Manager profile not found."
                });
            }

            if (requisition.HiringManagerId != hiringManager.Id)
            {
                return Forbid();
            }
        }

        var response =
            _mapper.Map<JobRequisitionResponseDto>(requisition);

        return Ok(response);
    }

    // ==========================================================
    // CREATE REQUISITION
    // Hiring Manager Only
    // ==========================================================

    [Authorize(Roles = "HiringManager")]
    [HttpPost]
    public async Task<IActionResult> CreateJobRequisition(
        CreateJobRequisitionDto dto)
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

        var hiringManager =
            await _unitOfWork.HiringManagers
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (hiringManager == null)
        {
            return BadRequest(new
            {
                message = "Hiring Manager profile not found."
            });
        }

        var requisition = new JobRequisition
        {
            HiringManagerId = hiringManager.Id,
            OrganizationId = hiringManager.OrganizationId,

            JobTitle = dto.JobTitle,
            Department = dto.Department,
            Location = dto.Location,
            SalaryRange = dto.SalaryRange,
            EmploymentType = dto.EmploymentType,
            NumberOfPositions = dto.NumberOfPositions,
            JobDescription = dto.JobDescription,

            ApprovalStatus = RequisitionStatus.Draft
        };

        await _unitOfWork.JobRequisitions.AddAsync(requisition);

        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<JobRequisitionResponseDto>(requisition);

        return Ok(response);
    }

        // ==========================================================
    // UPDATE REQUISITION
    // Hiring Manager (Own Only)
    // ==========================================================

    [Authorize(Roles = "HiringManager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobRequisition(
        Guid id,
        UpdateJobRequisitionDto dto)
    {
        var requisition =
            await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
        {
            return NotFound(new
            {
                message = "Job Requisition not found."
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

        var hiringManager =
            await _unitOfWork.HiringManagers
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (hiringManager == null)
        {
            return BadRequest(new
            {
                message = "Hiring Manager profile not found."
            });
        }

        if (requisition.HiringManagerId != hiringManager.Id)
        {
            return Forbid();
        }

        requisition.JobTitle = dto.JobTitle;
        requisition.Department = dto.Department;
        requisition.Location = dto.Location;
        requisition.SalaryRange = dto.SalaryRange;
        requisition.EmploymentType = dto.EmploymentType;
        requisition.NumberOfPositions = dto.NumberOfPositions;
        requisition.JobDescription = dto.JobDescription;

        _unitOfWork.JobRequisitions.Update(requisition);

        await _unitOfWork.SaveChangesAsync();

        var response =
            _mapper.Map<JobRequisitionResponseDto>(requisition);

        return Ok(new
        {
            message = "Job Requisition updated successfully.",
            requisition = response
        });
    }

    // ==========================================================
    // SUBMIT FOR APPROVAL
    // Hiring Manager (Own Only)
    // ==========================================================

    [Authorize(Roles = "HiringManager")]
    [HttpPut("{id}/submit")]
    public async Task<IActionResult> SubmitForApproval(Guid id)
    {
        var requisition =
            await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
        {
            return NotFound(new
            {
                message = "Job Requisition not found."
            });
        }

        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized();

        var hiringManager =
            await _unitOfWork.HiringManagers
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (hiringManager == null)
        {
            return BadRequest(new
            {
                message = "Hiring Manager profile not found."
            });
        }

        if (requisition.HiringManagerId != hiringManager.Id)
        {
            return Forbid();
        }

        requisition.ApprovalStatus = RequisitionStatus.Submitted;

        _unitOfWork.JobRequisitions.Update(requisition);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job Requisition submitted successfully."
        });
    }

    // ==========================================================
    // APPROVE REQUISITION
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> ApproveJobRequisition(Guid id)
    {
        var requisition =
            await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
        {
            return NotFound(new
            {
                message = "Job Requisition not found."
            });
        }

        requisition.ApprovalStatus = RequisitionStatus.Approved;

        _unitOfWork.JobRequisitions.Update(requisition);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job Requisition approved successfully."
        });
    }

        // ==========================================================
    // REJECT REQUISITION
    // Admin Only
    // ==========================================================

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/reject")]
    public async Task<IActionResult> RejectJobRequisition(Guid id)
    {
        var requisition =
            await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
        {
            return NotFound(new
            {
                message = "Job Requisition not found."
            });
        }

        requisition.ApprovalStatus = RequisitionStatus.Rejected;

        _unitOfWork.JobRequisitions.Update(requisition);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job Requisition rejected successfully."
        });
    }

    // ==========================================================
    // DELETE REQUISITION
    // Hiring Manager (Own Only)
    // ==========================================================

    [Authorize(Roles = "HiringManager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobRequisition(Guid id)
    {
        var requisition =
            await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
        {
            return NotFound(new
            {
                message = "Job Requisition not found."
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

        var hiringManager =
            await _unitOfWork.HiringManagers
                .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (hiringManager == null)
        {
            return BadRequest(new
            {
                message = "Hiring Manager profile not found."
            });
        }

        if (requisition.HiringManagerId != hiringManager.Id)
        {
            return Forbid();
        }

        _unitOfWork.JobRequisitions.Delete(requisition);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job Requisition deleted successfully."
        });
    }
}