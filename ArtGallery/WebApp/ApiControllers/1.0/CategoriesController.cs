using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Categories
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CategoryMapper _categoryMapper = new CategoryMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Categories
        /// <summary>
        /// Get all Categories
        /// </summary>
        /// <returns>A collection of Categories</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var bllCategories = await _bll.Categories.GetAllAsync();
            return Ok(bllCategories.Select(e => _categoryMapper.Map(e)));
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get a Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);

            if (category == null)
            {
                return NotFound(new MessageDTO("Category not found"));
            }
            return Ok(_categoryMapper.Map(category));
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Update a Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="category">Category object</param>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest(new MessageDTO("Id and category.Id do not match"));
            }

            if (!await _bll.Categories.ExistsAsync(category.Id))
            {
                return NotFound(new MessageDTO($"Category does not exist"));
            }

            var bllEntity = _categoryMapper.Map(category);
            await _bll.Categories.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create a new Category
        /// </summary>
        /// <param name="category">Category object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var bllEntity = _categoryMapper.Map(category);
            _bll.Categories.Add(bllEntity);
            await _bll.SaveChangesAsync();
            category.Id = bllEntity.Id;

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete a Category
        /// </summary>
        /// <param name="id">Category Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var category = await _bll.Categories.FirstOrDefaultAsync(id);
            
            if (category == null)
            {
                return NotFound(new MessageDTO("Category not found"));
            }

            await _bll.Categories.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_categoryMapper.Map(category));
        }
        
    }
}
