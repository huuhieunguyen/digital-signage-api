using System.Collections.Generic;
using CMS.Data;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public interface IContentItemRepository : IBaseRepository<ContentItem>
    {
        Task CreateRangeAsync(IEnumerable<ContentItem> entities);  // New method for batch inserts
    }

    public class ContentItemRepository : BaseRepository<ContentItem>, IContentItemRepository
    {
        public ContentItemRepository(CmsDbContext context) : base(context)
        {
        }

        public async Task CreateRangeAsync(IEnumerable<ContentItem> entities)  // New method for batch inserts
        {
            await _context.Set<ContentItem>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
