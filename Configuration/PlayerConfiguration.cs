using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("players");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Location)
                .HasMaxLength(50);

            builder.Property(p => p.Status)
                .HasConversion<string>();

            builder.Property(p => p.LastActiveDateTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.IPAddress)
                .HasMaxLength(15);

            builder.Property(p => p.VirtualUrl)
                .HasMaxLength(255);

            builder.Property(p => p.Resolution)
                .HasMaxLength(10);

            builder.Property(p => p.Orientation)
                .HasMaxLength(10);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            // a player can have one or many playlists
            // and a playlist can be associated with one or many players
            // so we need a join table, which is PlayerPlaylist.
            builder.HasMany(p => p.PlayerPlaylists)
                .WithOne(pp => pp.Player)
                .HasForeignKey(pp => pp.PlayerId);

            builder.HasMany(p => p.Labels)
                .WithMany(l => l.Players);

        }
    }
}
