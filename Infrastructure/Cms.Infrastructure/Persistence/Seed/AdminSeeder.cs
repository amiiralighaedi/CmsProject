

using Cms.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Persistence.Seed;

public static class AdminSeeder
{
    public static async Task SeedAdminAsync(ApplicationDbContext db)
    {
        if (!await db.Users.AnyAsync())
        {
            var admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Email = "admin@cms.local",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!")
            };

            db.Users.Add(admin);
            await db.SaveChangesAsync();

            var adminRole = await db.Roles.FirstAsync(r => r.Name == SystemRoles.Admin);

            db.UserRoles.Add(new UserRole
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            });

            await db.SaveChangesAsync();
        }
    }
}
