using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class ContentItemConfiguration : IEntityTypeConfiguration<ContentItem>
    {
        public void Configure(EntityTypeBuilder<ContentItem> builder)
        {
            builder.ToTable("content_items");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(ci => ci.Description)
                .HasMaxLength(255);

            builder.Property(ci => ci.FilePath)
                .HasMaxLength(255);

            builder.Property(ci => ci.ResourceType)
                .HasConversion<string>();

            builder.Property(ci => ci.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(ci => ci.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(ci => ci.PlaylistContentItems)
                .WithOne(pci => pci.ContentItem)
                .HasForeignKey(pci => pci.ContentItemId);
        }
    }
}