// ContentTypeConfiguration.cs

using Cms.Domain.Content.ContentTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Infrastructure.Persistence.Configurations;

public class ContentTypeConfiguration : IEntityTypeConfiguration<ContentType>
{
    public void Configure(EntityTypeBuilder<ContentType> builder)
    {
        builder.ToTable("ContentTypes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(256);

        builder.OwnsMany(x => x.FieldDefinitions, f =>
        {
            f.WithOwner()
                .HasForeignKey("ContentTypeId");

            f.Property<Guid>("Id");

            f.HasKey("Id");

            f.Property(p => p.Name)
                .HasMaxLength(256);
        });
    }
}