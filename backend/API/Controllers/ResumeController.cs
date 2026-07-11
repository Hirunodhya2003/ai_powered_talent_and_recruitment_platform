using System.Security.Claims;
using API.DTOs.Resume;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResumeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _environment;

    public ResumeController(
        IUnitOfWork unitOfWork,
        IWebHostEnvironment environment)
    {
        _unitOfWork = unitOfWork;
        _environment = environment;
    }

    // ======================================
    // Upload Resume
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadResume([FromForm] UploadResumeDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest(new { message = "Please select a PDF file." });

        var extension = Path.GetExtension(dto.File.FileName);

        if (!extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
            return BadRequest(new { message = "Only PDF files are allowed." });

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        Guid userId = Guid.Parse(userIdClaim);

        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user == null)
            return NotFound(new { message = "User not found." });

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            "resumes");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var resume = new Resume
        {
            FileName = dto.File.FileName,
            FileUrl = $"/uploads/resumes/{uniqueFileName}",
            UserId = userId,
            IsActive = true
        };

        await _unitOfWork.Resumes.AddAsync(resume);
        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Resume uploaded successfully.",
            resume.Id,
            resume.FileName,
            resume.FileUrl
        });
    }

    // ======================================
    // Candidate - My Resume
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyResume()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        Guid userId = Guid.Parse(userIdClaim);

        var resume = await _unitOfWork.Resumes.GetByUserIdAsync(userId);

        if (resume == null)
            return NotFound(new { message = "Resume not found." });

        return Ok(resume);
    }

    // ======================================
    // Recruiter/Admin - View Resume
    // ======================================

    [Authorize(Roles = "Recruiter,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetResumeById(Guid id)
    {
        var resume = await _unitOfWork.Resumes.GetByIdAsync(id);

        if (resume == null)
            return NotFound(new { message = "Resume not found." });

        return Ok(resume);
    }

    // ======================================
    // Candidate - Replace Resume Details
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpPut]
    public async Task<IActionResult> UpdateResume(Resume updatedResume)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        Guid userId = Guid.Parse(userIdClaim);

        var resume = await _unitOfWork.Resumes.GetByUserIdAsync(userId);

        if (resume == null)
            return NotFound(new { message = "Resume not found." });

        resume.FileName = updatedResume.FileName;
        resume.FileUrl = updatedResume.FileUrl;
        resume.IsActive = updatedResume.IsActive;

        _unitOfWork.Resumes.Update(resume);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Resume updated successfully."
        });
    }

    // ======================================
    // Candidate - Delete Resume
    // ======================================

    [Authorize(Roles = "Candidate")]
    [HttpDelete]
    public async Task<IActionResult> DeleteResume()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
            return Unauthorized(new { message = "Invalid token." });

        Guid userId = Guid.Parse(userIdClaim);

        var resume = await _unitOfWork.Resumes.GetByUserIdAsync(userId);

        if (resume == null)
            return NotFound(new { message = "Resume not found." });

        _unitOfWork.Resumes.Delete(resume);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Resume deleted successfully."
        });
    }
}