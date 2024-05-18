using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class LabelConfiguration : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.ToTable("labels");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(l => l.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            // a label can have one or many playlists
            // and a playlist can be associated with one or many labels
            // so we need a join table, which is PlaylistLabel.
            builder.HasMany(l => l.PlaylistLabels)
                .WithOne(pl => pl.Label)
                .HasForeignKey(pl => pl.LabelId);

            // a label can have one or many players
            // and a player can be associated with one or many labels
            // so we need a join table, which is PlayerLabel.
            builder.HasMany(l => l.PlayerLabels)
                .WithOne(pl => pl.Label)
                .HasForeignKey(pl => pl.LabelId);
        }
    }
}