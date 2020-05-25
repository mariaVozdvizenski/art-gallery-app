using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

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
        private readonly ArtistMapper _artistMapper = new ArtistMapper();

        public ArtistsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Artists
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistView>>> GetArtists()
        {
            var query = await _bll.Artists.GetAllAsync();
            return Ok(query.Select(e => _artistMapper.MapForViewAsync(e)).ToList());
        }

        /// <summary>
        /// Find and return Artist from data source
        /// </summary>
        /// <param name="id">artist id - guid</param>
        /// <returns>ArtistDTO object based on id</returns>
        /// <response code="200">The artist was successfully retrieved.</response>
        /// <response code="404">The artist does not exist.</response>

        // GET: api/Artists/5
        [AllowAnonymous]
        [ProducesResponseType( typeof( Domain.App.Artist ), 200 )]
        [ProducesResponseType( 404 )]
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistView>> GetArtist(Guid id)
        {
            var artist = await _bll.Artists.FirstOrDefaultAsync(id);
            
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(_artistMapper.MapForViewAsync(artist));
        }

        // PUT: api/Artists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> PutArtist(Guid id, Artist artistDTO)
        {
            if (id != artistDTO.Id)
            {
                return BadRequest();
            }
            
            var bllEntity = _artistMapper.Map(artistDTO);
            await _bll.Artists.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Artists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]

        public async Task<ActionResult<Domain.App.Artist>> PostArtist(Artist artistCreateDTO)
        {
            var bllEntity = _artistMapper.Map(artistCreateDTO);
            
            _bll.Artists.Add(bllEntity);
            await _bll.SaveChangesAsync();

            artistCreateDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetArtist", new { id = artistCreateDTO.Id }, artistCreateDTO);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]

        public async Task<ActionResult<Domain.App.Artist>> DeleteArtist(Guid id)
        {
            var artist = await _bll.Artists.FirstOrDefaultAsync(id);
            
            if (artist == null)
            {
                return NotFound();
            }

            await _bll.Artists.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(artist);
        }
    }
}
