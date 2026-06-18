
using Cms.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Seed;

public static class RoleSeeder
{
    public static async Task SeedRolesAsync(ApplicationDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Domain.Auth.Role { Id = Guid.NewGuid(), Name = SystemRoles.Admin },
                new Domain.Auth.Role { Id = Guid.NewGuid(), Name = SystemRoles.Editor },
                new Domain.Auth.Role { Id = Guid.NewGuid(), Name = SystemRoles.Viewer }
                );

            await context.SaveChangesAsync();
        }
    }
}
