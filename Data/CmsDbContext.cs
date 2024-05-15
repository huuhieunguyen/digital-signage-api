using Microsoft.EntityFrameworkCore;
using CMS.Models;

namespace CMS.Data
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options)
        : base(options)
        {
        }

        public DbSet<ContentItem> ContentItems { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        // other DbSets...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContentItem>()
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ContentItem>()
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            // modelBuilder.Entity<Playlist>()
            //                 .Property(e => e.CreatedAt)
            //                 .HasDefaultValueSql("CURRENT_TIMESTAMP")
            //                 .ValueGeneratedOnAdd();

            // modelBuilder.Entity<Playlist>()
            //     .Property(e => e.UpdatedAt)
            //     .HasDefaultValueSql("CURRENT_TIMESTAMP")
            //     .ValueGeneratedOnAddOrUpdate();
        }
    }
}