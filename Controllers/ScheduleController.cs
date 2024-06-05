using CMS.DTOs.ScheduleDtos;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/v1/schedules")]
    [ApiController]
    public class ScheduleController : BaseController<ScheduleResponseDto, ScheduleCreateRequestDto>
    {
        public ScheduleController(IScheduleService service)
            : base(service)
        {
        }
    }
}
