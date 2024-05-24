using System.Collections.Generic;
using CMS.Data;
using CMS.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS.Repositories
{
    public class ContentItemRepository : IContentItemRepository
    {
        private readonly CmsDbContext _dbContext;

        public ContentItemRepository(CmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ContentItem>> GetAllContentItemsAsync()
        {
            return await _dbContext.ContentItems
                .Include(ci => ci.PlaylistContentItems)
                .ToListAsync();
        }

        public async Task<ContentItem> GetContentItemByIdAsync(int contentItemId)
        {
            return await _dbContext.ContentItems
                .Include(ci => ci.PlaylistContentItems)
                .FirstOrDefaultAsync(ci => ci.Id == contentItemId);
        }

        public async Task<ContentItem> AddContentItemAsync(ContentItem contentItem)
        {
            _dbContext.ContentItems.Add(contentItem);
            await _dbContext.SaveChangesAsync();
            return contentItem;
        }

        public async Task<ContentItem> UpdateContentItemAsync(ContentItem contentItem)
        {
            _dbContext.Entry(contentItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return contentItem;
        }

        public async Task DeleteContentItemAsync(int contentItemId)
        {
            var contentItem = await _dbContext.ContentItems.FindAsync(contentItemId);
            if (contentItem != null)
            {
                _dbContext.ContentItems.Remove(contentItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> ContentItemExistsAsync(int contentItemId)
        {
            return await _dbContext.ContentItems.AnyAsync(ci => ci.Id == contentItemId);
        }
    }
}
