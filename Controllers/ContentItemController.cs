using Microsoft.AspNetCore.Mvc;
using CMS.Data;
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
        public ActionResult GetContentItems()
        {
            var contents = _contentItemService.GetAllContentItems();
            return Ok(contents);
        }

        [HttpGet("{id}", Name = "GetContentItemById")]
        public ActionResult GetContentItem(int id)
        {
            var content = _contentItemService.GetContentItem(id);
            if (content == null)
                return NotFound();

            return Ok(content);
        }

        [HttpPost]
        public ActionResult CreateContentItem(ContentItem contentItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var content = _contentItemService.CreateContentItem(contentItem);
            return CreatedAtRoute("GetContentItemById", new { id = contentItem.Id }, content);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateContentItem(int id, ContentItem contentItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var content = _contentItemService.UpdateContentItem(id, contentItem);
            if (content == null)
                return NotFound();

            return Ok(content);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteContentItem(int id)
        {
            var deletedContent = _contentItemService.DeleteContentItem(id);
            if (deletedContent == null)
                return NotFound();

            return Ok(deletedContent);
        }
    }
}
