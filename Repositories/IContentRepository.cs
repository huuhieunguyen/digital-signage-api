using System.Collections.Generic;
using CMS.Models;

namespace CMS.Repositories
{
    public interface IContentItemRepository
    {
        IEnumerable<ContentItem> GetAllContentItems();
        ContentItem GetContentItem(int id);
        ContentItem CreateContentItem(ContentItem contentItem);
        ContentItem UpdateContentItem(ContentItem contentItem);
        ContentItem DeleteContentItem(ContentItem contentItem);
    }
}
