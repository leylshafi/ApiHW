using ApiHW.Data;
using ApiHW.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiHW.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 2)
        {
            List<Tag> Tags = await _context.Tags.Skip((page - 1) * take).Take(take).ToListAsync();
            return Ok(Tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);
            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);
            tag.Name = name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            Tag tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);
            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
