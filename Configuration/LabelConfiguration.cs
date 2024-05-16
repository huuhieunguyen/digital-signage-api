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

            builder.HasMany(l => l.Players)
                .WithMany(p => p.Labels);
        }
    }
}