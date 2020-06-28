using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Artists
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ArtistsController : ControllerBase
    {
        //private readonly IAppUnitOfWork _uow;
        private readonly IAppBLL _bll;
        private readonly ArtistMapper _artistMapper = new ArtistMapper();

        /// <summary>
        /// Controller
        /// </summary>
        public ArtistsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Artists
        /// <summary>
        /// Get all Artists
        /// </summary>
        /// <returns>A collection of Artists</returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ArtistView>))]
        public async Task<ActionResult<IEnumerable<ArtistView>>> GetArtists()
        {
            var query = await _bll.Artists.GetAllAsync();
            return Ok(query.Select(e => _artistMapper.MapForViewAsync(e)).ToList());
        }

        /// <summary>
        /// Get a single Artist
        /// </summary>
        /// <param name="id">Artist Id</param>
        /// <returns>Artist object</returns>
        /// <response code="200">The artist was successfully retrieved.</response>
        /// <response code="404">The artist does not exist.</response>

        // GET: api/Artists/5
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArtistView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistView>> GetArtist(Guid id)
        {
            var artist = await _bll.Artists.FirstOrDefaultAsync(id);
            
            if (artist == null)
            {
                return NotFound(new MessageDTO($"Artist with {id} not found"));
            }

            return Ok(_artistMapper.MapForViewAsync(artist));
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Update an Artist
        /// </summary>
        /// <param name="id">Artist Id</param>
        /// <param name="artistDTO">Artist object</param>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutArtist(Guid id, Artist artistDTO)
        {
            if (id != artistDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and artist.Id do not match"));
            }

            if (!await _bll.Artists.ExistsAsync(artistDTO.Id))
            {
                return NotFound(new MessageDTO($"Artist does not exist"));
            }
            
            var bllEntity = _artistMapper.Map(artistDTO);
            await _bll.Artists.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Artists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create a new Artist
        /// </summary>
        /// <param name="artistCreateDTO">An Artist object</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Artist))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Artist>> PostArtist(Artist artistCreateDTO)
        {
            var bllEntity = _artistMapper.Map(artistCreateDTO);
            
            _bll.Artists.Add(bllEntity);
            await _bll.SaveChangesAsync();

            artistCreateDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetArtist", new { id = artistCreateDTO.Id }, artistCreateDTO);
        }

        // DELETE: api/Artists/5
        /// <summary>
        /// Delete an Artist
        /// </summary>
        /// <param name="id">Artist Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Artist))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Artist>> DeleteArtist(Guid id)
        {
            var artist = await _bll.Artists.FirstOrDefaultAsync(id);
            
            if (artist == null)
            {
                return NotFound(new MessageDTO("Artist not found"));
            }

            await _bll.Artists.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_artistMapper.Map(artist));
        }
    }
}
