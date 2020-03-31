using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaintingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Paintings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Painting>>> GetPaintings()
        {
            return await _context.Paintings.ToListAsync();
        }

        // GET: api/Paintings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Painting>> GetPainting(Guid id)
        {
            var painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            return painting;
        }

        // PUT: api/Paintings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPainting(Guid id, Painting painting)
        {
            if (id != painting.Id)
            {
                return BadRequest();
            }

            _context.Entry(painting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaintingExists(id))
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

        // POST: api/Paintings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Painting>> PostPainting(Painting painting)
        {
            _context.Paintings.Add(painting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPainting", new { id = painting.Id }, painting);
        }

        // DELETE: api/Paintings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Painting>> DeletePainting(Guid id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();

            return painting;
        }

        private bool PaintingExists(Guid id)
        {
            return _context.Paintings.Any(e => e.Id == id);
        }
    }
}
