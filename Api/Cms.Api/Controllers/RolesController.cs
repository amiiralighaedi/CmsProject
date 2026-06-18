using Cms.Domain.Auth;
using Cms.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RolesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(Guid userId, string role)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound("User not found");

        var roleEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
        if (roleEntity == null) return NotFound("Role not found");

        if (await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleEntity.Id))
            return BadRequest("User already has this role");

        _context.UserRoles.Add(new UserRole
        {
            UserId = userId,
            RoleId = roleEntity.Id
        });

        await _context.SaveChangesAsync();
        return Ok("Role assigned");
    }
}
