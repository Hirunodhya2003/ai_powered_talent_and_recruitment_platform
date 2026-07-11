using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
//using Application.Common;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsersController(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/users
    [Authorize(Roles = "Admin,Recruiter")]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _unitOfWork.Users.GetAllAsync();

        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        return Ok(new ApiResponse<IEnumerable<UserDto>>
{
    Success = true,
    Message = "Users retrieved successfully.",
    Data = userDtos
});
    }

    // GET: api/users/{id}
    [Authorize(Roles = "Admin,Recruiter")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);

        if (user == null)
            return NotFound(new { message = "User not found." });

        var userDto = _mapper.Map<UserDto>(user);

        return Ok(new ApiResponse<UserDto>
{
    Success = true,
    Message = "User retrieved successfully.",
    Data = userDto
});
    }

    // POST: api/users
[Authorize(Roles = "Admin")]
[HttpPost]
public async Task<IActionResult> CreateUser(CreateUserDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var existingUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

    if (existingUser != null)
    {
        return BadRequest(new
        {
            message = "Email already exists."
        });
    }

    var user = _mapper.Map<User>(dto);

    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);

    await _unitOfWork.Users.AddAsync(user);
    await _unitOfWork.SaveChangesAsync();

    var userDto = _mapper.Map<UserDto>(user);

    return CreatedAtAction(
    nameof(GetUserById),
    new { id = user.Id },
    new ApiResponse<UserDto>
    {
        Success = true,
        Message = "User created successfully.",
        Data = userDto
    });
}

    // PUT: api/users/{id}
[Authorize(Roles = "Admin")]
[HttpPut("{id}")]
public async Task<IActionResult> UpdateUser(Guid id, UpdateUserDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var existingUser = await _unitOfWork.Users.GetByIdAsync(id);

    if (existingUser == null)
        return NotFound(new { message = "User not found." });

    var emailUser = await _unitOfWork.Users.GetByEmailAsync(dto.Email);

    if (emailUser != null && emailUser.Id != id)
    {
        return BadRequest(new
        {
            message = "Email already exists."
        });
    }

    _mapper.Map(dto, existingUser);

    if (!string.IsNullOrWhiteSpace(dto.PasswordHash))
    {
        existingUser.PasswordHash =
            BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);
    }

    _unitOfWork.Users.Update(existingUser);
    await _unitOfWork.SaveChangesAsync();

    return Ok(new ApiResponse<object>
{
    Success = true,
    Message = "User updated successfully.",
    Data = null
});
}

   // DELETE: api/users/{id}
[Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteUser(Guid id)
{
    var user = await _unitOfWork.Users.GetByIdAsync(id);

    if (user == null)
        return NotFound(new
        {
            message = "User not found."
        });

    _unitOfWork.Users.Delete(user);
    await _unitOfWork.SaveChangesAsync();

    
    return Ok(new ApiResponse<object>
{
    Success = true,
    Message = "User deleted successfully.",
    Data = null
});
}
}