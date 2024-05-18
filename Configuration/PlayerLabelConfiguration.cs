using CMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Configuration
{
    public class PlayerLabelConfiguration : IEntityTypeConfiguration<PlayerLabel>
    {
        public void Configure(EntityTypeBuilder<PlayerLabel> builder)
        {
            builder.ToTable("player_labels");

            builder.HasKey(pl => new { pl.PlayerId, pl.LabelId });

            builder.Property(pl => pl.PlayerId)
                .IsRequired();

            builder.Property(pl => pl.LabelId)
                .IsRequired();

            builder.Property(pp => pp.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(pp => pp.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.HasOne(pl => pl.Player)
                .WithMany(p => p.PlayerLabels)
                .HasForeignKey(pl => pl.PlayerId);

            builder.HasOne(pl => pl.Label)
                .WithMany(l => l.PlayerLabels)
                .HasForeignKey(pl => pl.LabelId);
        }
    }
}