using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArtistsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            return await _context.Artists.Select(a => new ArtistDTO()
            {
                Id = a.Id, FirstName = a.FirstName, LastName = a.LastName, Country = a.Country,
                PaintingCount = a.Paintings.Count
            }).ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(Guid id)
        {
            var artist = await _context.Artists.Select(a => new ArtistDTO()
            {
                Id = a.Id, Country = a.Country, FirstName = a.FirstName, LastName = a.LastName, PaintingCount = a.Paintings.Count
            }).Where(a => a.Id == id).FirstOrDefaultAsync();
            
            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(Guid id, ArtistEditDTO artistEditDTO)
        {
            if (id != artistEditDTO.Id)
            {
                return BadRequest();
            }
            
            var artist = await _context.Artists.FindAsync(artistEditDTO.Id);

            if (artist == null)
            {
                return BadRequest();
            }

            artist.Country = artistEditDTO.Country;
            artist.FirstName = artistEditDTO.FirstName;
            artist.LastName = artistEditDTO.LastName;

            _context.Artists.Update(artist);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(ArtistCreateDTO artistCreateDTO)
        {
            var artist = new Artist()
            {
                FirstName = artistCreateDTO.FirstName,
                LastName = artistCreateDTO.LastName,
                Country = artistCreateDTO.Country,
                PlaceOfBirth = artistCreateDTO.PlaceOfBirth,
                Bio = artistCreateDTO.Bio,
                DateOfBirth = artistCreateDTO.DateOfBirth
            };
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(Guid id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return artist;
        }

        private bool ArtistExists(Guid id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
