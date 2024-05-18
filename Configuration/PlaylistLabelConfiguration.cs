using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class PlaylistLabelConfiguration : IEntityTypeConfiguration<PlaylistLabel>
    {
        public void Configure(EntityTypeBuilder<PlaylistLabel> builder)
        {
            builder.ToTable("playlist_labels");

            builder.HasKey(pl => new { pl.PlaylistId, pl.LabelId });

            builder.Property(pl => pl.PlaylistId)
                .IsRequired();

            builder.Property(pl => pl.LabelId)
                .IsRequired();

            builder.Property(pl => pl.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(pl => pl.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(pl => pl.Playlist)
                .WithMany(p => p.PlaylistLabels)
                .HasForeignKey(pl => pl.PlaylistId);

            builder.HasOne(pl => pl.Label)
                .WithMany(l => l.PlaylistLabels)
                .HasForeignKey(pl => pl.LabelId);
        }
    }
}