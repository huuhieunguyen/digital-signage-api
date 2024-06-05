using CMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlob.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAttachmentController : ControllerBase
    {
        AzureBlobService _service;
        public TestAttachmentController(AzureBlobService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlobs(List<IFormFile> files)
        {
            var response = await _service.UploadFiles(files);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlobs()
        {
            var response = await _service.GetUploadedBlobs();
            return Ok(response);
        }

    }
}
