using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvaluations()
    {
        var evaluations = await _unitOfWork.Evaluations.GetAllAsync();
        return Ok(evaluations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvaluationById(Guid id)
    {
        var evaluation = await _unitOfWork.Evaluations.GetByIdAsync(id);

        if (evaluation == null)
            return NotFound(new { message = "Evaluation not found." });

        return Ok(evaluation);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvaluation(Evaluation evaluation)
    {
        await _unitOfWork.Evaluations.AddAsync(evaluation);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetEvaluationById),
            new { id = evaluation.Id },
            evaluation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvaluation(Guid id, Evaluation evaluation)
    {
        if (id != evaluation.Id)
            return BadRequest(new { message = "Evaluation ID mismatch." });

        var existing = await _unitOfWork.Evaluations.GetByIdAsync(id);

        if (existing == null)
            return NotFound(new { message = "Evaluation not found." });

        _unitOfWork.Evaluations.Update(evaluation);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Evaluation updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvaluation(Guid id)
    {
        var evaluation = await _unitOfWork.Evaluations.GetByIdAsync(id);

        if (evaluation == null)
            return NotFound(new { message = "Evaluation not found." });

        _unitOfWork.Evaluations.Delete(evaluation);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Evaluation deleted successfully."
        });
    }
}