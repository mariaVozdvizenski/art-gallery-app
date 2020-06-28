using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Paintings
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    
    public class PaintingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private PaintingMapper _paintingMapper = new PaintingMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PaintingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Paintings
        /// <summary>
        /// Get all Paintings
        /// </summary>
        /// <param name="condition">Filters Paintings by price, string</param>
        /// <param name="inStock">Filters Paintings by availability, boolean</param>
        /// <param name="categories">Filters Paintings by categories, string</param>
        /// <returns>A filtered collection of Paintings</returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaintingView>))]
        public async Task<ActionResult<IEnumerable<PaintingView>>> GetPaintings(string? condition, bool? inStock, 
            string? categories)
        {
            var query = await _bll.Paintings.GetAllForViewAsync();
            query = await _bll.Paintings.ApplyFilters(query, condition, categories, inStock);
            
            return Ok(query.Select(e => _paintingMapper.MapPaintingView(e)).ToList());
        }

        // GET: api/Paintings/5
        /// <summary>
        /// Get a single Painting
        /// </summary>
        /// <param name="id">Painting Id</param>
        /// <returns>Painting object</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaintingView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaintingView>> GetPainting(Guid id)
        {
            var painting = await _bll.Paintings.GetFirstOrDefaultForViewAsync(id);

            if (painting == null)
            {
                return NotFound(new MessageDTO("Painting was not found"));
            }
            return Ok(_paintingMapper.MapPaintingView(painting));
        }

        // PUT: api/Paintings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Update a Painting
        /// </summary>
        /// <param name="id">Painting Id</param>
        /// <param name="paintingDTO">Painting object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutPainting(Guid id, Painting paintingDTO)
        {
            if (id != paintingDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and painting.Id do not match"));
            }

            if (!await _bll.Paintings.ExistsAsync(paintingDTO.Id))
            {
                return NotFound(new MessageDTO($"PaintingCategory does not exist"));
            }

            var bllEntity = _paintingMapper.Map(paintingDTO);
            await _bll.Paintings.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Paintings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create a new Painting
        /// </summary>
        /// <param name="paintingCreateDTO">Painting object</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Painting))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Painting>> PostPainting(Painting paintingCreateDTO)
        {
            var bllEntity = _paintingMapper.Map(paintingCreateDTO);
            _bll.Paintings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            paintingCreateDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetPainting", new { id = paintingCreateDTO.Id }, paintingCreateDTO);
        }

        // DELETE: api/Paintings/5
        /// <summary>
        /// Delete a Painting
        /// </summary>
        /// <param name="id">Painting Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Painting))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Painting>> DeletePainting(Guid id)
        {
            var painting = await _bll.Paintings.FirstOrDefaultAsync(id, User.UserGuidId());
            
            if (painting == null)
            {
                return NotFound(new MessageDTO("Painting was not found"));
            }
            
            await _bll.Paintings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_paintingMapper.Map(painting));
        }
    }
}
