using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class PlaylistContentItemConfiguration : IEntityTypeConfiguration<PlaylistContentItem>
    {
        public void Configure(EntityTypeBuilder<PlaylistContentItem> builder)
        {
            builder.ToTable("playlist_content_items");

            builder.HasKey(pci => new { pci.PlaylistId, pci.ContentItemId });

            builder.Property(pci => pci.PlaylistId)
                .IsRequired();

            builder.Property(pci => pci.ContentItemId)
                .IsRequired();

            builder.Property(pci => pci.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(pci => pci.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(pci => pci.Playlist)
                .WithMany(p => p.PlaylistContentItems)
                .HasForeignKey(pci => pci.PlaylistId);

            builder.HasOne(pci => pci.ContentItem)
                .WithMany(ci => ci.PlaylistContentItems)
                .HasForeignKey(pci => pci.ContentItemId);
        }
    }
}