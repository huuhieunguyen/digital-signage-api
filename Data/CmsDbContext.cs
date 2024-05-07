using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class CmsDbContext : DbContext
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options)
        : base(options)
        {
        }

        public DbSet<ContentItem> ContentItems { get; set; }
    }
}