using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._1
{
    [ApiController]
    [ApiVersion( "1.1" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ArtistsController : ControllerBase
    {
        //private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        

        public ArtistsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetArtists()
        {
            return Ok(await _bll.Artists.DTOAllAsync());
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(Guid id)
        {
            var artist = await _bll.Artists.DTOFirstOrDefaultAsync(id);
            
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
            
            var artist = await _bll.Artists.FirstOrDefaultAsync(artistEditDTO.Id);

            if (artist == null)
            {
                return BadRequest();
            }

            artist.Country = artistEditDTO.Country;
            artist.FirstName = artistEditDTO.FirstName;
            artist.LastName = artistEditDTO.LastName;

            _bll.Artists.Update(artist);
            
            try
            {
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Artists.ExistsAsync(id))
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
            _bll.Artists.Add(artist);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.Id }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(Guid id)
        {
            await _bll.Artists.DeleteAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
