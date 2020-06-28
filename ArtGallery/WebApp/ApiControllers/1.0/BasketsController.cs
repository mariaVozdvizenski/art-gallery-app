using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Extensions;
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
    /// Baskets
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class BasketsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BasketMapper _basketMapper = new BasketMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public BasketsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Baskets
        /// <summary>
        /// Get all Baskets for current User
        /// </summary>
        /// <returns>A collection of Baskets</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasketView>))]
        public async Task<ActionResult<IEnumerable<BasketView>>> GetBaskets()
        {
            var basketList = await _bll.Baskets.GetAllAsync(User.UserGuidId());
            return Ok(basketList.Select(e => _basketMapper.MapBasketView(e)));
        }

        // GET: api/Baskets/5
        /// <summary>
        /// Get a single Basket
        /// </summary>
        /// <param name="id">Basket Id</param>
        /// <returns>Basket object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<BasketView>> GetBasket(Guid id)
        {
            var basket = await _bll.Baskets.FirstOrDefaultAsync(id, User.UserGuidId());

            if (basket == null)
            {
                return NotFound(new MessageDTO("BasketItem not found"));
            }
            
            return _basketMapper.MapBasketView(basket);
        }

        // PUT: api/Baskets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update a Basket
        /// </summary>
        /// <param name="id">Basket Id</param>
        /// <param name="basket">Basket object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutBasket(Guid id, Basket basket)
        {
            if (id != basket.Id)
            {
                return BadRequest(new MessageDTO("Id and basket.Id do not match"));
            }

            if (!await _bll.Baskets.ExistsAsync(basket.Id))
            {
                return NotFound(new MessageDTO($"Basket does not exist"));
            }
            
            basket.DateCreated = DateTime.Now;
            var bllEntity = _basketMapper.Map(basket);
            await _bll.Baskets.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Baskets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new Basket
        /// </summary>
        /// <param name="basket">Basket object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Basket))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Basket>> PostBasket(Basket basket)
        {
            basket.DateCreated = DateTime.Now;
            
            var bllEntity = _basketMapper.Map(basket);
            
            _bll.Baskets.Add(bllEntity);
            await _bll.SaveChangesAsync();

            basket.Id = bllEntity.Id;

            return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
        }

        // DELETE: api/Baskets/5
        /// <summary>
        /// Delete a Basket
        /// </summary>
        /// <param name="id">Basket Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Basket))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Basket>> DeleteBasket(Guid id)
        {
            var basket = await _bll.Baskets.FirstOrDefaultAsync(id);
            
            if (basket == null)
            {
                return NotFound(new MessageDTO("Basket not found"));
            }

            await _bll.Baskets.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_basketMapper.Map(basket));
        }
    }
}
