using System.Collections.Generic;
using CMS.Data;
using CMS.Models;

namespace CMS.Repositories
{
    public class ContentItemRepository : IContentItemRepository
    {
        private readonly CmsDbContext _dbContext;

        public ContentItemRepository(CmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ContentItem> GetAllContentItems()
        {
            return _dbContext.ContentItems.ToList();
        }

        public ContentItem GetContentItem(int id)
        {
            return _dbContext.ContentItems.FirstOrDefault(c => c.Id == id);
        }

        public ContentItem CreateContentItem(ContentItem contentItem)
        {
            _dbContext.ContentItems.Add(contentItem);
            _dbContext.SaveChanges();
            return contentItem;
        }

        public ContentItem UpdateContentItem(ContentItem contentItem)
        {
            _dbContext.ContentItems.Update(contentItem);
            _dbContext.SaveChanges();
            return contentItem;
        }

        public ContentItem DeleteContentItem(ContentItem contentItem)
        {
            _dbContext.ContentItems.Remove(contentItem);
            _dbContext.SaveChanges();
            return contentItem;
        }
    }
}
