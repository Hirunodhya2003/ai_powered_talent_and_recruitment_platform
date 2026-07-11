using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobApplicationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public JobApplicationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/jobapplications
    [HttpGet]
    public async Task<IActionResult> GetAllJobApplications()
    {
        var applications = await _unitOfWork.Applications.GetAllAsync();
        return Ok(applications);
    }

    // GET: api/jobapplications/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobApplicationById(Guid id)
    {
        var application = await _unitOfWork.Applications.GetByIdAsync(id);

        if (application == null)
            return NotFound(new { message = "Job application not found." });

        return Ok(application);
    }

    // POST: api/jobapplications
    [HttpPost]
    public async Task<IActionResult> CreateJobApplication(JobApplication application)
    {
        await _unitOfWork.Applications.AddAsync(application);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetJobApplicationById),
            new { id = application.Id },
            application);
    }

    // PUT: api/jobapplications/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobApplication(Guid id, JobApplication application)
    {
        if (id != application.Id)
            return BadRequest(new { message = "Job Application ID mismatch." });

        var existingApplication = await _unitOfWork.Applications.GetByIdAsync(id);

        if (existingApplication == null)
            return NotFound(new { message = "Job application not found." });

        _unitOfWork.Applications.Update(application);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job application updated successfully."
        });
    }

    // DELETE: api/jobapplications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobApplication(Guid id)
    {
        var application = await _unitOfWork.Applications.GetByIdAsync(id);

        if (application == null)
            return NotFound(new { message = "Job application not found." });

        _unitOfWork.Applications.Delete(application);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job application deleted successfully."
        });
    }
}