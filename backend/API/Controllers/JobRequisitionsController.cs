using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobRequisitionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public JobRequisitionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllJobRequisitions()
    {
        var requisitions = await _unitOfWork.JobRequisitions.GetAllAsync();
        return Ok(requisitions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobRequisitionById(Guid id)
    {
        var requisition = await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
            return NotFound(new { message = "Job Requisition not found." });

        return Ok(requisition);
    }

    [HttpPost]
    public async Task<IActionResult> CreateJobRequisition(JobRequisition requisition)
    {
        await _unitOfWork.JobRequisitions.AddAsync(requisition);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetJobRequisitionById),
            new { id = requisition.Id },
            requisition);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJobRequisition(Guid id, JobRequisition requisition)
    {
        if (id != requisition.Id)
            return BadRequest(new { message = "Job Requisition ID mismatch." });

        var existing = await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (existing == null)
            return NotFound(new { message = "Job Requisition not found." });

        _unitOfWork.JobRequisitions.Update(requisition);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job Requisition updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJobRequisition(Guid id)
    {
        var requisition = await _unitOfWork.JobRequisitions.GetByIdAsync(id);

        if (requisition == null)
            return NotFound(new { message = "Job Requisition not found." });

        _unitOfWork.JobRequisitions.Delete(requisition);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Job Requisition deleted successfully."
        });
    }
}