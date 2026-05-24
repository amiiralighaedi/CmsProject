using Cms.Domain.Content.ContentItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Infrastructure.Persistence.Configurations;

public class ContentVersionConfiguration : IEntityTypeConfiguration<ContentVersion>
{
    public void Configure(EntityTypeBuilder<ContentVersion> builder)
    {
        builder.ToTable("ContentVersions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.VersionNumber)
            .IsRequired();

        builder.Property(x => x.CreateAt)
            .IsRequired();

        builder.OwnsMany(x => x.Values, values =>
        {
            values.WithOwner()
                .HasForeignKey("ContentVersionId");

            values.Property<Guid>("Id");

            values.HasKey("Id");

            values.Property(v => v.FieldName)
                .HasMaxLength(200);

            values.Property(v => v.Value);
        });

        builder.HasOne(v => v.ContentItem)
            .WithMany(i => i.Versions)
            .HasForeignKey(v => v.ContentItemId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}