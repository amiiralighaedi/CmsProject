

using Cms.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(50);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

        builder.Property(x => x.PasswordHash).IsRequired();

        builder.HasMany(x => x.Roles)
            .WithOne(ur => ur.User)
            .HasForeignKey(x => x.UserId);
    }
}
