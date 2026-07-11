using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public OrganizationsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // GET: api/organizations
    // All authenticated users
    [HttpGet]
    public async Task<IActionResult> GetAllOrganizations()
    {
        var organizations = await _unitOfWork.Organizations.GetAllAsync();

        return Ok(organizations);
    }

    // GET: api/organizations/{id}
    // All authenticated users
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizationById(Guid id)
    {
        var organization = await _unitOfWork.Organizations.GetByIdAsync(id);

        if (organization == null)
        {
            return NotFound(new
            {
                message = "Organization not found."
            });
        }

        return Ok(organization);
    }

    // POST: api/organizations
    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOrganization(Organization organization)
    {
        await _unitOfWork.Organizations.AddAsync(organization);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetOrganizationById),
            new { id = organization.Id },
            organization);
    }

    // PUT: api/organizations/{id}
    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrganization(Guid id, Organization organization)
    {
        if (id != organization.Id)
        {
            return BadRequest(new
            {
                message = "Organization ID mismatch."
            });
        }

        var existingOrganization =
            await _unitOfWork.Organizations.GetByIdAsync(id);

        if (existingOrganization == null)
        {
            return NotFound(new
            {
                message = "Organization not found."
            });
        }

        _unitOfWork.Organizations.Update(organization);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Organization updated successfully."
        });
    }

    // DELETE: api/organizations/{id}
    // Admin only
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        var organization =
            await _unitOfWork.Organizations.GetByIdAsync(id);

        if (organization == null)
        {
            return NotFound(new
            {
                message = "Organization not found."
            });
        }

        _unitOfWork.Organizations.Delete(organization);

        await _unitOfWork.SaveChangesAsync();

        return Ok(new
        {
            message = "Organization deleted successfully."
        });
    }
}