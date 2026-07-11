using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HiringDecisionsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public HiringDecisionsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHiringDecisions()
    {
        var decisions = await _unitOfWork.HiringDecisions.GetAllAsync();
        return Ok(decisions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHiringDecisionById(Guid id)
    {
        var decision = await _unitOfWork.HiringDecisions.GetByIdAsync(id);

        if (decision == null)
            return NotFound(new { message = "Hiring Decision not found." });

        return Ok(decision);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHiringDecision(HiringDecision decision)
    {
        await _unitOfWork.HiringDecisions.AddAsync(decision);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetHiringDecisionById),
            new { id = decision.Id },
            decision);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHiringDecision(Guid id, HiringDecision decision)
    {
        if (id != decision.Id)
            return BadRequest(new { message = "Hiring Decision ID mismatch." });

        var existing = await _unitOfWork.HiringDecisions.GetByIdAsync(id);

        if (existing == null)
            return NotFound(new { message = "Hiring Decision not found." });

        _unitOfWork.HiringDecisions.Update(decision);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Hiring Decision updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHiringDecision(Guid id)
    {
        var decision = await _unitOfWork.HiringDecisions.GetByIdAsync(id);

        if (decision == null)
            return NotFound(new { message = "Hiring Decision not found." });

        _unitOfWork.HiringDecisions.Delete(decision);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Hiring Decision deleted successfully."
        });
    }
}