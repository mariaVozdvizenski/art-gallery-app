using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
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
        private readonly IAppUnitOfWork _uow;

        public ArtistsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            return Ok(await _uow.Artists.DTOAllAsync());
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(Guid id)
        {
            var artist = await _uow.Artists.DTOFirstOrDefaultAsync(id);
            
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
            
            var artist = await _uow.Artists.FirstOrDefaultAsync(artistEditDTO.Id);

            if (artist == null)
            {
                return BadRequest();
            }

            artist.Country = artistEditDTO.Country;
            artist.FirstName = artistEditDTO.FirstName;
            artist.LastName = artistEditDTO.LastName;

            _uow.Artists.Update(artist);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Artists.ExistsAsync(id))
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
                DateOfBirth = artistCreateDTO.DateOfBirth,
            };
            _uow.Artists.Add(artist);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(Guid id)
        {
            await _uow.Artists.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
