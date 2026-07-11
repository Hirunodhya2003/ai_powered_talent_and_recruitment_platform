using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HiringManagersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public HiringManagersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/hiringmanagers
    [HttpGet]
    public async Task<IActionResult> GetAllHiringManagers()
    {
        var hiringManagers = await _unitOfWork.HiringManagers.GetAllAsync();
        return Ok(hiringManagers);
    }

    // GET: api/hiringmanagers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetHiringManagerById(Guid id)
    {
        var hiringManager = await _unitOfWork.HiringManagers.GetByIdAsync(id);

        if (hiringManager == null)
            return NotFound(new { message = "Hiring Manager not found." });

        return Ok(hiringManager);
    }

    // POST: api/hiringmanagers
    [HttpPost]
    public async Task<IActionResult> CreateHiringManager(HiringManager hiringManager)
    {
        await _unitOfWork.HiringManagers.AddAsync(hiringManager);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHiringManagerById),
            new { id = hiringManager.Id }, hiringManager);
    }

    // PUT: api/hiringmanagers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHiringManager(Guid id, HiringManager hiringManager)
    {
        if (id != hiringManager.Id)
            return BadRequest();

        var existing = await _unitOfWork.HiringManagers.GetByIdAsync(id);

        if (existing == null)
            return NotFound();

        _unitOfWork.HiringManagers.Update(hiringManager);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Hiring Manager updated successfully."
        });
    }

    // DELETE: api/hiringmanagers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHiringManager(Guid id)
    {
        var hiringManager = await _unitOfWork.HiringManagers.GetByIdAsync(id);

        if (hiringManager == null)
            return NotFound();

        _unitOfWork.HiringManagers.Delete(hiringManager);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Hiring Manager deleted successfully."
        });
    }
}