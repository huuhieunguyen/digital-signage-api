// using Microsoft.AspNetCore.Mvc;
// using CMS.Models;
// using CMS.Services;
// using CMS.Models.RequestModels;

// namespace CMS.Controllers
// {
//     [Route("api/v1/content-items")]
//     [ApiController]
//     public class ContentItemController : ControllerBase
//     {
//         private readonly IContentItemService _contentItemService;

//         public ContentItemController(IContentItemService contentItemService)
//         {
//             _contentItemService = contentItemService;
//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<ContentItem>>> GetContentItems()
//         {
//             var contentItems = await _contentItemService.GetAllContentItemsAsync();
//             return Ok(contentItems);
//         }

//         [HttpGet("{id}")]
//         public async Task<ActionResult<ContentItem>> GetContentItem(int id)
//         {
//             var contentItem = await _contentItemService.GetContentItemByIdAsync(id);
//             if (contentItem == null)
//             {
//                 return NotFound();
//             }
//             return Ok(contentItem);
//         }

//         [HttpPost]
//         public async Task<IActionResult> PostContentItems([FromForm] List<IFormFile> files, [FromQuery] string storageOption = "cloudinary")
//         {
//             if (files == null || files.Count == 0)
//             {
//                 return BadRequest("No files uploaded.");
//             }

//             try
//             {
//                 var contentItems = await _contentItemService.AddContentItemsAsync(files, storageOption);
//                 return Ok(contentItems);
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"Internal server error: {ex.Message}");
//             }
//         }

//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutContentItem(int id, [FromBody] ContentItemUpdateRequestModel updateModel)
//         {
//             var contentItem = await _contentItemService.GetContentItemByIdAsync(id);
//             if (contentItem == null)
//             {
//                 return NotFound();
//             }

//             contentItem.Title = updateModel.Title;
//             contentItem.Description = updateModel.Description;

//             await _contentItemService.UpdateContentItemAsync(contentItem);
//             return Ok(contentItem);
//         }

//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteContentItem(int id)
//         {
//             var contentItem = await _contentItemService.GetContentItemByIdAsync(id);
//             if (contentItem == null)
//             {
//                 return NotFound();
//             }

//             await _contentItemService.DeleteContentItemAsync(id);
//             return Ok(new { message = "Content item deleted successfully." });
//         }
//     }
// }

using CMS.DTOs.ContentItemDtos;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/v1/content-items")]
    [ApiController]
    public class ContentItemController : ControllerBase
    {
        private readonly IContentItemService _contentItemService;

        public ContentItemController(IContentItemService service)
        {
            _contentItemService = service;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ContentItemResponseDto>>> Create([FromForm] List<IFormFile> files)
        {
            var result = await _contentItemService.UploadContentItemsAsync(files);
            return Ok(result);
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadContentItem(int id)
        {
            var result = await _contentItemService.DownloadContentItemAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return File(result.Stream, result.ContentType, result.FileName);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentItemResponseDto>>> GetAll()
        {
            var responses = await _contentItemService.GetAllAsync();
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContentItemResponseDto>> GetById(int id)
        {
            var response = await _contentItemService.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ContentItemResponseDto>> Update(int id, [FromBody] ContentItemUpdateRequestDto request)
        {
            var response = await _contentItemService.UpdateContentItemAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _contentItemService.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
