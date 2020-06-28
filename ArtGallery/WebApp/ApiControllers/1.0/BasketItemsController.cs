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
    /// Basket Items
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class BasketItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private BasketItemMapper _basketItemMapper = new BasketItemMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public BasketItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/BasketItems
        /// <summary>
        /// Get all BasketItems for current User
        /// </summary>
        /// <returns>A collection of BasketItems</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BasketItemView>))]
        public async Task<ActionResult<IEnumerable<BasketItemView>>>GetBasketItems()
        {
            //Check for Painting Quantity here
            var bllBasketItems = await _bll.BasketItems.GetBasketItemsForUsersBasketAsync(User.UserGuidId());
            return Ok(bllBasketItems.Select(e => _basketItemMapper.MapForViewAsync(e)).ToList());
        }

        // GET: api/BasketItems/5
        /// <summary>
        /// Get a single BasketItem
        /// </summary>
        /// <param name="id">BasketItem Id</param>
        /// <returns>BasketItem object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketItemView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<BasketItemView>> GetBasketItem(Guid id)
        {
            var basketItem = await _bll.BasketItems.GetBasketItemForUserBasketAsync(id, User.UserGuidId());

            if (basketItem == null)
            {
                return NotFound(new MessageDTO("BasketItem not found"));
            }
            return Ok(_basketItemMapper.MapForViewAsync(basketItem));
        }

        // PUT: api/BasketItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update BasketItem
        /// </summary>
        /// <param name="id">BasketItem Id</param>
        /// <param name="basketItem">BasketItem object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutBasketItem(Guid id, BasketItem basketItem)
        {
            if (id != basketItem.Id)
            {
                return BadRequest(new MessageDTO("Id and basketItem.Id do not match"));
            }

            if (!await _bll.BasketItems.ExistsAsync(basketItem.Id))
            {
                return NotFound(new MessageDTO($"BasketItem does not exist"));
            }
            
            var bllEntity = _basketItemMapper.Map(basketItem);
            var painting = await _bll.Paintings.FirstOrDefaultAsync(bllEntity.PaintingId);
            
            if (_bll.BasketItems.CheckForPaintingQuantity(bllEntity, painting) == false)
            {
                return BadRequest(new MessageDTO("Not enough paintings in stock."));
            }

            if (!_bll.BasketItems.CheckThatQuantityIsNotZero(bllEntity))
            {
                return BadRequest(new MessageDTO("Cannot put 0 items into the cart"));
            }
            
            await _bll.BasketItems.UpdateAsync(bllEntity, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/BasketItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new BasketItem
        /// </summary>
        /// <param name="basketItem">BasketItem object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BasketItem))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<BasketItem>> PostBasketItem(BasketItem basketItem)
        {
            basketItem.DateCreated = DateTime.Now;
            var bllEntity = _basketItemMapper.Map(basketItem);
            var painting = await _bll.Paintings.FirstOrDefaultAsync(bllEntity.PaintingId);

            if (await _bll.BasketItems.DuplicatePaintingExistsAsync(painting, User.UserGuidId()))
            {
                return BadRequest(new MessageDTO("Painting is already in the cart."));
            }

            if (!_bll.BasketItems.CheckForPaintingQuantity(bllEntity, painting))
            {
                return BadRequest(new MessageDTO("Not enough paintings in stock."));
            }
            
            if (!_bll.BasketItems.CheckThatQuantityIsNotZero(bllEntity))
            {
                return BadRequest(new MessageDTO("Cannot put 0 items into the cart"));
            }
            
            bllEntity.DateCreated = DateTime.Now;
            _bll.BasketItems.Add(bllEntity);
            await _bll.SaveChangesAsync();
            basketItem.Id = bllEntity.Id;
            return CreatedAtAction("GetBasketItem", new { id = basketItem.Id }, basketItem);
        }

        // DELETE: api/BasketItems/5
        /// <summary>
        /// Delete a BasketItem
        /// </summary>
        /// <param name="id">BasketItem Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<BasketItem>> DeleteBasketItem(Guid id)
        {
            var basketItem = await _bll.BasketItems.FirstOrDefaultAsync(id);
            
            if (basketItem == null)
            {
                return NotFound(new MessageDTO("BasketItem no found"));
            }

            await _bll.BasketItems.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_basketItemMapper.Map(basketItem));
        }
    }
}
