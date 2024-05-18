using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable("playlists");

            builder.HasKey(pl => pl.Id);

            builder.Property(pl => pl.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(pl => pl.Enabled)
                .HasDefaultValue(true);

            builder.Property(pl => pl.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(pl => pl.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasMany(pl => pl.PlayerPlaylists)
                .WithOne(pp => pp.Playlist)
                .HasForeignKey(pp => pp.PlaylistId);

            builder.HasMany(pl => pl.PlaylistContentItems)
                .WithOne(pci => pci.Playlist)
                .HasForeignKey(pci => pci.PlaylistId);
        }
    }
}