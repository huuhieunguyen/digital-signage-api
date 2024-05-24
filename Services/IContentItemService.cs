using CMS.Models;

namespace CMS.Services
{
    public interface IContentItemService
    {
        Task<IEnumerable<ContentItem>> GetAllContentItemsAsync();
        Task<ContentItem> GetContentItemByIdAsync(int contentItemId);
        Task<IEnumerable<ContentItem>> AddContentItemsAsync(List<IFormFile> files, string storageOption);
        Task<ContentItem> UpdateContentItemAsync(ContentItem contentItem);
        Task DeleteContentItemAsync(int contentItemId);
    }
}
