using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TalentPoolsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TalentPoolsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/talentpools
    [HttpGet]
    public async Task<IActionResult> GetAllTalentPools()
    {
        var talentPools = await _unitOfWork.TalentPools.GetAllAsync();
        return Ok(talentPools);
    }

    // GET: api/talentpools/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTalentPoolById(Guid id)
    {
        var talentPool = await _unitOfWork.TalentPools.GetByIdAsync(id);

        if (talentPool == null)
            return NotFound(new { message = "Talent Pool record not found." });

        return Ok(talentPool);
    }

    // POST: api/talentpools
    [HttpPost]
    public async Task<IActionResult> CreateTalentPool(TalentPool talentPool)
    {
        await _unitOfWork.TalentPools.AddAsync(talentPool);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetTalentPoolById),
            new { id = talentPool.Id },
            talentPool);
    }

    // PUT: api/talentpools/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTalentPool(Guid id, TalentPool talentPool)
    {
        if (id != talentPool.Id)
            return BadRequest(new { message = "Talent Pool ID mismatch." });

        var existingTalentPool = await _unitOfWork.TalentPools.GetByIdAsync(id);

        if (existingTalentPool == null)
            return NotFound(new { message = "Talent Pool record not found." });

        _unitOfWork.TalentPools.Update(talentPool);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Talent Pool updated successfully."
        });
    }

    // DELETE: api/talentpools/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTalentPool(Guid id)
    {
        var talentPool = await _unitOfWork.TalentPools.GetByIdAsync(id);

        if (talentPool == null)
            return NotFound(new { message = "Talent Pool record not found." });

        _unitOfWork.TalentPools.Delete(talentPool);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Talent Pool deleted successfully."
        });
    }
}