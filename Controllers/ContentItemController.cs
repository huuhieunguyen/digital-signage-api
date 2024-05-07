using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


[Route("API/[controller]")]
[ApiController]
public class ContentItemController : ControllerBase
{
    private readonly CmsDbContext _context;

    public ContentItemController(CmsDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ContentItem>>> GetContentItems()
    {
        return await _context.ContentItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ContentItem>> GetContentItem(int id)
    {
        var contentItem = await _context.ContentItems.FindAsync(id);

        if (contentItem == null)
        {
            return NotFound();
        }

        return contentItem;
    }

    [HttpPost]
    public async Task<ActionResult<ContentItem>> PostContentItem(ContentItem contentItem)
    {
        contentItem.CreatedAt = DateTime.Now;
        _context.ContentItems.Add(contentItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetContentItem", new { id = contentItem.Id }, contentItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutContentItem(int id, ContentItem contentItem)
    {
        if (id != contentItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(contentItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ContentItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContentItem(int id)
    {
        var contentItem = await _context.ContentItems.FindAsync(id);

        if (contentItem == null)
        {
            return NotFound();
        }

        _context.ContentItems.Remove(contentItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ContentItemExists(int id)
    {
        return _context.ContentItems.Any(e => e.Id == id);
    }
}