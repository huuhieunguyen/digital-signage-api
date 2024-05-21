using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;

namespace CMS.Controllers
{
    [Route("api/v1/content-items")]
    [ApiController]
    public class ContentItemController : ControllerBase
    {
        private readonly IContentItemService _contentItemService;

        public ContentItemController(IContentItemService contentItemService)
        {
            _contentItemService = contentItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentItem>>> GetContentItems()
        {
            var contentItems = await _contentItemService.GetAllContentItemsAsync();
            return Ok(contentItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContentItem>> GetContentItem(int id)
        {
            var contentItem = await _contentItemService.GetContentItemByIdAsync(id);
            if (contentItem == null)
            {
                return NotFound();
            }
            return Ok(contentItem);
        }

        [HttpPost]
        public async Task<ActionResult<ContentItem>> PostContentItem([FromForm] ContentItem contentItem, IFormFile file)
        {
            if (file == null || (file.ContentType != "image/jpeg" && file.ContentType != "video/mp4"))
            {
                return BadRequest("Only JPEG images and MP4 videos are allowed.");
            }

            var createdContentItem = await _contentItemService.AddContentItemAsync(contentItem, file);
            return CreatedAtAction(nameof(GetContentItem), new { id = createdContentItem.Id }, createdContentItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContentItem(int id, ContentItem contentItem)
        {
            if (id != contentItem.Id)
            {
                return BadRequest();
            }

            await _contentItemService.UpdateContentItemAsync(contentItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContentItem(int id)
        {
            var contentItem = await _contentItemService.GetContentItemByIdAsync(id);
            if (contentItem == null)
            {
                return NotFound();
            }

            await _contentItemService.DeleteContentItemAsync(id);
            return NoContent();
        }
    }
}
