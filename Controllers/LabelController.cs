using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;
using CMS.DTOs.LabelDtos;

namespace CMS.Controllers
{
    [Route("api/v1/labels")]
    [ApiController]
    public class LabelController : BaseController<LabelResponseDto, LabelCreateRequestDto>
    {
        public LabelController(ILabelService service) : base(service)
        {
        }
    }
}