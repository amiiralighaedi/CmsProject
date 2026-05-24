using Cms.Domain.Content.ContentItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.Infrastructure.Persistence.Configurations;

public class ContentItemConfiguration : IEntityTypeConfiguration<ContentItem>
{
    public void Configure(EntityTypeBuilder<ContentItem> builder)
    {
        builder.ToTable("ContentItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ContentTypeId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Navigation(x => x.Versions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(x => x.Values, values =>
        {
            values.WithOwner()
                .HasForeignKey("ContentItemId");

            values.Property<Guid>("Id");

            values.HasKey("Id");

            values.Property(v => v.FieldName)
                .HasMaxLength(200);

            values.Property(v => v.Value);
        });
    }
}