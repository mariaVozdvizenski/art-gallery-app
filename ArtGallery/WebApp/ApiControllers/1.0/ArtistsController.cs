using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

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
            return Ok(await _bll.Artists.AllAsync(User.UserGuidId())); // need this to not get a SyntaxError: Unexpected end of JSON
        }

        /// <summary>
        /// Find and return Artist from data source
        /// </summary>
        /// <param name="id">artist id - guid</param>
        /// <returns>ArtistDTO object based on id</returns>
        /// <response code="200">The artist was successfully retrieved.</response>
        /// <response code="404">The artist does not exist.</response>

        // GET: api/Artists/5
        [ProducesResponseType( typeof( Artist ), 200 )]
        [ProducesResponseType( 404 )]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistDTO>> GetArtist(Guid id)
        {
            var artist = await _bll.Artists.FirstOrDefaultAsync(id, User.UserGuidId());
            
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
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
                if (!await _bll.Artists.ExistsAsync(id, User.UserGuidId()))
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
            var artist = new BLL.App.DTO.Artist
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
            var artist = await _bll.Artists.FirstOrDefaultAsync(id, User.UserGuidId());
            if (artist == null)
            {
                return NotFound();
            }

            _bll.Artists.Remove(artist);
            await _bll.SaveChangesAsync();

            return Ok(artist);
        }
    }
}
