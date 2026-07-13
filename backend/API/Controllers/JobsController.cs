using System.IdentityModel.Tokens.Jwt;
using Application.DTOs.Jobs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoMapper;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IMapper _mapper;
private readonly IUnitOfWork _unitOfWork;

public JobsController(
    IUnitOfWork unitOfWork,
    IMapper mapper)
{
    _unitOfWork = unitOfWork;
    _mapper = mapper;
}

    // GET: api/jobs
    //[Authorize(Roles = "Admin,Recruiter")]
    [Authorize(Roles = "Admin,Recruiter,HiringManager,Candidate")]
[HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        var jobs = await _unitOfWork.Jobs.GetAllAsync();
        var response = _mapper.Map<List<JobResponseDto>>(jobs);

return Ok(response);
    }

    // GET: api/jobs/{id}
  /** [Authorize(Roles = "Admin,Recruiter")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(Guid id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);

        if (job == null)
            return NotFound(new { message = "Job not found." });

        var response = _mapper.Map<JobResponseDto>(job);

return Ok(response);
    }**/

    [Authorize(Roles = "Admin,Recruiter")]
[HttpGet("{id}")]
public async Task<IActionResult> GetJobById(Guid id)
{
    var job = await _unitOfWork.Jobs.GetByIdAsync(id);

    if (job == null)
        return NotFound(new { message = "Job not found." });

    // Admin can view any job
    if (User.IsInRole("Admin"))
    {
        var adminResponse = _mapper.Map<JobResponseDto>(job);
        return Ok(adminResponse);
    }

    // Logged-in Recruiter
    var userIdClaim =
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
        User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

    if (string.IsNullOrWhiteSpace(userIdClaim))
        return Unauthorized(new { message = "Invalid token." });

    var recruiter = await _unitOfWork.Recruiters
        .GetByUserIdAsync(Guid.Parse(userIdClaim));

    if (recruiter == null)
        return BadRequest(new { message = "Recruiter profile not found." });

    // Recruiter can only view own jobs
    if (job.RecruiterId != recruiter.Id)
        return Forbid();

    var response = _mapper.Map<JobResponseDto>(job);

    return Ok(response);
}

    

    // POST: api/jobs
   /** [Authorize(Roles = "Admin,Recruiter")]
    [HttpPost]
    public async Task<IActionResult> CreateJob(CreateJobDto dto)
    {
       var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
           return Unauthorized(new { message = "Invalid token." });

        var recruiter = await _unitOfWork.Recruiters
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (recruiter == null)
            return BadRequest(new { message = "Recruiter profile not found." });

        var job = new Job
        {
            RecruiterId = recruiter.Id,
            OrganizationId = recruiter.OrganizationId,

            Title = dto.Title,
            Description = dto.Description,
            Location = dto.Location,
            EmploymentType = dto.EmploymentType,
            MinimumSalary = dto.MinimumSalary,
            MaximumSalary = dto.MaximumSalary,
            ExpiryDate = dto.ExpiryDate,

            Status = JobStatus.Active
        };

        await _unitOfWork.Jobs.AddAsync(job);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetJobById),
            new { id = job.Id },
            job);
    }**/

    // POST: api/jobs
[Authorize(Roles = "Admin,Recruiter")]
[HttpPost]
public async Task<IActionResult> CreateJob(CreateJobDto dto)
{
    // Get logged-in user id from JWT
    var userIdClaim =
        User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
        User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

    if (string.IsNullOrWhiteSpace(userIdClaim))
    {
        // Debug (remove later)
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type} = {claim.Value}");
        }

        return Unauthorized(new { message = "Invalid token." });
    }

    var recruiter = await _unitOfWork.Recruiters
        .GetByUserIdAsync(Guid.Parse(userIdClaim));

    if (recruiter == null)
        return BadRequest(new { message = "Recruiter profile not found." });

    var job = new Job
    {
        RecruiterId = recruiter.Id,
        OrganizationId = recruiter.OrganizationId,

        Title = dto.Title,
        Description = dto.Description,
        Location = dto.Location,
        EmploymentType = dto.EmploymentType,
        MinimumSalary = dto.MinimumSalary,
        MaximumSalary = dto.MaximumSalary,
        ExpiryDate = dto.ExpiryDate,

        Status = JobStatus.Active
    };

    await _unitOfWork.Jobs.AddAsync(job);
    await _unitOfWork.SaveChangesAsync();

    var response = _mapper.Map<JobResponseDto>(job);

return CreatedAtAction(
    nameof(GetJobById),
    new { id = response.Id },
    response);
}

    // PUT: api/jobs/{id}
    /**[Authorize(Roles = "Admin,Recruiter")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(Guid id, CreateJobDto dto)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);

        if (job == null)
            return NotFound(new { message = "Job not found." });

        job.Title = dto.Title;
        job.Description = dto.Description;
        job.Location = dto.Location;
        job.EmploymentType = dto.EmploymentType;
        job.MinimumSalary = dto.MinimumSalary;
        job.MaximumSalary = dto.MaximumSalary;
        job.ExpiryDate = dto.ExpiryDate;

        _unitOfWork.Jobs.Update(job);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job updated successfully."
        });
    }**/

    [Authorize(Roles = "Admin,Recruiter")]
[HttpPut("{id}")]
public async Task<IActionResult> UpdateJob(Guid id, CreateJobDto dto)
{
    var job = await _unitOfWork.Jobs.GetByIdAsync(id);

    if (job == null)
        return NotFound(new { message = "Job not found." });

    // Admin can update any job
    if (!User.IsInRole("Admin"))
    {
        var userIdClaim =
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        var recruiter = await _unitOfWork.Recruiters
            .GetByUserIdAsync(Guid.Parse(userIdClaim));

        if (recruiter == null)
            return BadRequest(new { message = "Recruiter profile not found." });

        if (job.RecruiterId != recruiter.Id)
            return Forbid();
    }

    job.Title = dto.Title;
    job.Description = dto.Description;
    job.Location = dto.Location;
    job.EmploymentType = dto.EmploymentType;
    job.MinimumSalary = dto.MinimumSalary;
    job.MaximumSalary = dto.MaximumSalary;
    job.ExpiryDate = dto.ExpiryDate;

    _unitOfWork.Jobs.Update(job);
    await _unitOfWork.SaveChangesAsync();

    return Ok(new
    {
        message = "Job updated successfully."
    });
}

    // DELETE: api/jobs/{id}
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(Guid id)
    {
        var job = await _unitOfWork.Jobs.GetByIdAsync(id);

        if (job == null)
            return NotFound(new { message = "Job not found." });

        _unitOfWork.Jobs.Delete(job);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job deleted successfully."
        });
    }
}