using CMS.Models;

namespace CMS.Services
{
    public interface IContentItemService
    {
        Task<IEnumerable<ContentItem>> GetAllContentItemsAsync();
        Task<ContentItem> GetContentItemByIdAsync(int contentItemId);
        Task<ContentItem> AddContentItemAsync(ContentItem contentItem, IFormFile file);
        Task<ContentItem> UpdateContentItemAsync(ContentItem contentItem);
        Task DeleteContentItemAsync(int contentItemId);
    }
}
