using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Resume;

public class UploadResumeDto
{
    [Required(ErrorMessage = "Resume file is required.")]
    public IFormFile File { get; set; } = null!;
}