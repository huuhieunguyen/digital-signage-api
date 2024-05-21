using CMS.Models;

namespace CMS.Repositories
{
    public interface IContentItemRepository
    {
        Task<IEnumerable<ContentItem>> GetAllContentItemsAsync();
        Task<ContentItem> GetContentItemByIdAsync(int contentItemId);
        Task<ContentItem> AddContentItemAsync(ContentItem contentItem);
        Task<ContentItem> UpdateContentItemAsync(ContentItem contentItem);
        Task DeleteContentItemAsync(int contentItemId);
        Task<bool> ContentItemExistsAsync(int contentItemId);
    }

}
