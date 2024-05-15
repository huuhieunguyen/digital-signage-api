using CMS.Models;

namespace CMS.Services
{
    public interface IContentItemService
    {
        IEnumerable<ContentItem> GetAllContentItems();
        ContentItem GetContentItem(int id);
        ContentItem CreateContentItem(ContentItem requestModel);
        ContentItem UpdateContentItem(int id, ContentItem requestModel);
        ContentItem DeleteContentItem(int id);
    }
}
