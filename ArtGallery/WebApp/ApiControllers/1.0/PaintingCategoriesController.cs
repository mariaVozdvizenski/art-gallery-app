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
    /// Painting Categories
    /// </summary>
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class PaintingCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private PaintingCategoryMapper _paintingCategoryMapper = new PaintingCategoryMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PaintingCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaintingCategories
        /// <summary>
        /// Get all PaintingCategories
        /// </summary>
        /// <returns>A collection of PaintingCategories</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaintingCategory>))]
        public async Task<ActionResult<IEnumerable<PaintingCategoryView>>> GetPaintingCategories()
        {
            var query = await _bll.PaintingCategories.GetAllAsync();
            return  Ok(query.Select(e => _paintingCategoryMapper.MapPaintingCategoryView(e)));
        }

        // GET: api/PaintingCategories/5
        /// <summary>
        /// Get a single PaintingCategory
        /// </summary>
        /// <param name="id">PaintingCategory Id</param>
        /// <returns>PaintingCategory object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaintingCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaintingCategory>> GetPaintingCategory(Guid id)
        {
            var paintingCategory = await _bll.PaintingCategories.FirstOrDefaultAsync(id);

            if (paintingCategory == null)
            {
                return NotFound(new MessageDTO("PaintingCategory not found"));
            }

            return _paintingCategoryMapper.Map(paintingCategory);
        }

        // PUT: api/PaintingCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update a PaintingCategory
        /// </summary>
        /// <param name="id">PaintingCategory Id</param>
        /// <param name="paintingCategory">PaintingCategory object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutPaintingCategory(Guid id, PaintingCategory paintingCategory)
        {
            if (id != paintingCategory.Id)
            {
                return BadRequest(new MessageDTO("Id and paintingCategory.Id do not match"));
            }

            if (!await _bll.PaintingCategories.ExistsAsync(paintingCategory.Id))
            {
                return NotFound(new MessageDTO($"PaintingCategory does not exist"));
            }

            var bllEntity = _paintingCategoryMapper.Map(paintingCategory);
            await _bll.PaintingCategories.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PaintingCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new PaintingCategory
        /// </summary>
        /// <param name="paintingCategory">PaintingCategory object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaintingCategory))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaintingCategory>> PostPaintingCategory(PaintingCategory paintingCategory)
        {
            var bllEntity = _paintingCategoryMapper.Map(paintingCategory);
            
            _bll.PaintingCategories.Add(bllEntity);
            await _bll.SaveChangesAsync();

            paintingCategory.Id = bllEntity.Id;

            return CreatedAtAction("GetPaintingCategory", new { id = paintingCategory.Id }, paintingCategory);
        }

        // DELETE: api/PaintingCategories/5
        /// <summary>
        /// Delete a PaintingCategory
        /// </summary>
        /// <param name="id">PaintingCategory Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaintingCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaintingCategory>> DeletePaintingCategory(Guid id)
        {
            var paintingCategory = await _bll.PaintingCategories.FirstOrDefaultAsync(id);
            
            if (paintingCategory == null)
            {
                return NotFound(new MessageDTO("PaintingCategory not found"));
            }

            await _bll.PaintingCategories.RemoveAsync(paintingCategory);
            await _bll.SaveChangesAsync();

            return _paintingCategoryMapper.Map(paintingCategory);
        }
    }
}
