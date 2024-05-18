using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class PlayerPlaylistConfiguration : IEntityTypeConfiguration<PlayerPlaylist>
    {
        public void Configure(EntityTypeBuilder<PlayerPlaylist> builder)
        {
            builder.ToTable("player_playlists");

            builder.HasKey(pp => new { pp.PlayerId, pp.PlaylistId });

            builder.Property(pp => pp.PlayerId)
                .IsRequired();

            builder.Property(pp => pp.PlaylistId)
                .IsRequired();

            builder.Property(pp => pp.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(pp => pp.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(pp => pp.Player)
                .WithMany(p => p.PlayerPlaylists)
                .HasForeignKey(pp => pp.PlayerId);

            builder.HasOne(pp => pp.Playlist)
                .WithMany(pl => pl.PlayerPlaylists)
                .HasForeignKey(pp => pp.PlaylistId);
        }
    }
}