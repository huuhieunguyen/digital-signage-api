using CMS.Models;
using CMS.Repositories;

namespace CMS.Services
{
    public class ContentItemService : IContentItemService
    {
        private readonly IContentItemRepository _contentItemRepository;

        public ContentItemService(IContentItemRepository contentItemRepository)
        {
            _contentItemRepository = contentItemRepository;
        }

        public IEnumerable<ContentItem> GetAllContentItems()
        {
            return _contentItemRepository.GetAllContentItems();
        }

        public ContentItem GetContentItem(int id)
        {
            return _contentItemRepository.GetContentItem(id);
        }

        public ContentItem CreateContentItem(ContentItem contentItem)
        {
            var content = new ContentItem
            {
                Title = contentItem.Title,
                Description = contentItem.Description,
                FilePath = contentItem.FilePath,
                ResourceType = contentItem.ResourceType,
                Duration = contentItem.Duration,
                Dimensions = contentItem.Dimensions,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return _contentItemRepository.CreateContentItem(content);
        }

        public ContentItem UpdateContentItem(int id, ContentItem contentItem)
        {
            var existingContent = _contentItemRepository.GetContentItem(id);
            if (existingContent == null)
                return null;

            existingContent.Title = contentItem.Title;
            existingContent.Description = contentItem.Description;
            existingContent.FilePath = contentItem.FilePath;
            existingContent.ResourceType = contentItem.ResourceType;
            existingContent.Duration = contentItem.Duration;
            existingContent.Dimensions = contentItem.Dimensions;
            existingContent.UpdatedAt = DateTime.UtcNow;

            return _contentItemRepository.UpdateContentItem(existingContent);
        }

        public ContentItem DeleteContentItem(int id)
        {
            var existingContent = _contentItemRepository.GetContentItem(id);
            if (existingContent == null)
                return null;

            return _contentItemRepository.DeleteContentItem(existingContent);
        }
    }
}
