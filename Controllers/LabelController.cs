using Microsoft.AspNetCore.Mvc;
using CMS.Models;
using CMS.Services;
using CMS.Models.RequestModels;

namespace CMS.Controllers
{
    [ApiController]
    [Route("api/v1/labels")]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;

        public LabelsController(ILabelService service)
        {
            _labelService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLabels()
        {
            var labels = await _labelService.GetAllLabels();
            return Ok(labels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabel(int id)
        {
            var label = await _labelService.GetLabel(id);
            if (label == null)
            {
                return NotFound();
            }
            return Ok(label);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Label label)
        {
            var createdLabel = await _labelService.CreateLabel(label);
            return CreatedAtAction(nameof(GetLabel), new { id = createdLabel.Id }, createdLabel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Label label)
        {
            if (id != label.Id)
            {
                return BadRequest();
            }

            var existingLabel = await _labelService.GetLabel(id);
            if (existingLabel == null)
            {
                return NotFound();
            }

            var updatedLabel = await _labelService.UpdateLabel(label);
            return Ok(updatedLabel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _labelService.DeleteLabel(id);
            return NoContent();
        }
    }
}