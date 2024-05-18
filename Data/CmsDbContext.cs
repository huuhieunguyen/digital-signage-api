using Microsoft.EntityFrameworkCore;
using CMS.Models;
using CMS.Configuration;

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
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerPlaylist> PlayerPlaylists { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<PlaylistLabel> PlaylistLabels { get; set; }
        public DbSet<PlayerLabel> PlayerLabels { get; set; }
        public DbSet<PlaylistContentItem> PlaylistContentItems { get; set; }
        // other DbSets...

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerPlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new LabelConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new ContentItemConfiguration());
            // modelBuilder.ApplyConfiguration(new ScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistLabelConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerLabelConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistContentItemConfiguration());

            // other configurations...
        }
    }
}