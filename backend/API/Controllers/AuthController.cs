using Application.DTOs.Auth;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
//using Application.DTOs.Auth;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    private readonly IAuditService _auditService;

    public AuthController(
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IAuditService auditService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
         _auditService = auditService;
    }

// ========================= REGISTER =========================

[HttpPost("register")]
public async Task<IActionResult> Register(RegisterDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    if (await _unitOfWork.Users.IsEmailExistsAsync(dto.Email))
    {
        return BadRequest(new
        {
            message = "Email already exists."
        });
    }

    await _unitOfWork.BeginTransactionAsync();

    try
    {
        var user = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role,
            IsActive = true
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Recruiter Profile
        if (dto.Role == Domain.Enums.UserRole.Recruiter)
        {
            if (dto.OrganizationId == null)
            {
                await _unitOfWork.RollbackAsync();

                return BadRequest(new
                {
                    message = "OrganizationId is required for Recruiters."
                });
            }

            var recruiter = new Recruiter
            {
                UserId = user.Id,
                OrganizationId = dto.OrganizationId.Value,
                Department = dto.Department,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber
            };

            await _unitOfWork.Recruiters.AddAsync(recruiter);
        }

        // Candidate Profile
        else if (dto.Role == Domain.Enums.UserRole.Candidate)
        {
            var candidate = new Candidate
            {
                UserId = user.Id,
                PhoneNumber = dto.PhoneNumber,
                IsOpenToWork = true,
                ProfileCompleteness = 0
            };

            await _unitOfWork.Candidates.AddAsync(candidate);
        }

        await _unitOfWork.SaveChangesAsync();

        await _auditService.LogAsync(
            user.Id,
            "REGISTER",
            "User",
            user.Id.ToString(),
            "User registered successfully.",
            HttpContext.Connection.RemoteIpAddress?.ToString()
        );

        await _unitOfWork.CommitAsync();

        return Ok(new
        {
            message = "Registration successful."
        });
    }
    catch
    {
        await _unitOfWork.RollbackAsync();
        throw;
    }
}

    // ========================= LOGIN =========================

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

        if (user == null)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        //Console.WriteLine("Email: " + dto.Email);
        //Console.WriteLine("Password: " + dto.Password);
        //Console.WriteLine("Hash: " + user.PasswordHash);

        bool validPassword = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash);

        if (!validPassword)
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        var accessToken = _jwtService.GenerateToken(user);

        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);
        await _unitOfWork.SaveChangesAsync();

        await _auditService.LogAsync(
    user.Id,
    "LOGIN",
    "User",
    user.Id.ToString(),
    "User logged in successfully.",
    HttpContext.Connection.RemoteIpAddress?.ToString()
);

        return Ok(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Email = user.Email,
            Role = user.Role.ToString()
        });
    }

    // ========================= REFRESH =========================

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequestDto dto)
    {
        var storedToken = await _unitOfWork.RefreshTokens
            .GetByTokenAsync(dto.RefreshToken);

        if (storedToken == null)
        {
            return Unauthorized(new
            {
                message = "Invalid refresh token."
            });
        }

        if (storedToken.Revoked != null)
        {
            return Unauthorized(new
            {
                message = "Refresh token has been revoked."
            });
        }

        if (storedToken.Expires < DateTime.UtcNow)
        {
            return Unauthorized(new
            {
                message = "Refresh token has expired."
            });
        }

        var user = storedToken.User;

        var newAccessToken = _jwtService.GenerateToken(user);

        var newRefreshToken = _jwtService.GenerateRefreshToken();

        // Revoke old refresh token
        storedToken.Revoked = DateTime.UtcNow;
        _unitOfWork.RefreshTokens.Update(storedToken);

        // Save new refresh token
        var refreshTokenEntity = new RefreshToken
        {
            Token = newRefreshToken,
            UserId = user.Id,
            Created = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

        await _unitOfWork.SaveChangesAsync();

        await _auditService.LogAsync(
    user.Id,
    "REFRESH_TOKEN",
    "User",
    user.Id.ToString(),
    "Access token refreshed.",
    HttpContext.Connection.RemoteIpAddress?.ToString()
);

        return Ok(new RefreshTokenResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    // ========================= LOGOUT =========================

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequestDto dto)
    {
        var storedToken = await _unitOfWork.RefreshTokens
            .GetByTokenAsync(dto.RefreshToken);

        if (storedToken == null)
        {
            return Unauthorized(new
            {
                message = "Invalid refresh token."
            });
        }

        if (storedToken.Revoked != null)
        {
            return BadRequest(new
            {
                message = "Already logged out."
            });
        }

        storedToken.Revoked = DateTime.UtcNow;

        _unitOfWork.RefreshTokens.Update(storedToken);

        await _unitOfWork.SaveChangesAsync();

        await _auditService.LogAsync(
    storedToken.UserId,
    "LOGOUT",
    "User",
    storedToken.UserId.ToString(),
    "User logged out successfully.",
    HttpContext.Connection.RemoteIpAddress?.ToString()
);

        return Ok(new
        {
            message = "Logout successful."
        });
    }
}