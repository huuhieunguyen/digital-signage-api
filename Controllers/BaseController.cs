using CMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public abstract class BaseController<TResponse, TCreateRequest> : ControllerBase
    {
        private readonly IBaseService<TResponse, TCreateRequest> _service;

        protected BaseController(IBaseService<TResponse, TCreateRequest> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TResponse>>> GetAll()
        {
            var responses = await _service.GetAllAsync();
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TResponse>> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<TResponse>> Create(TCreateRequest request)
        {
            var response = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.GetHashCode() }, response); // Adjust as needed for identifier
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TResponse>> Update(int id, TCreateRequest request)
        {
            var response = await _service.UpdateAsync(id, request);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
