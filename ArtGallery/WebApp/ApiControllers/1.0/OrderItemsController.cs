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
    /// Order Items
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrderItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderItemMapper _orderItemMapper = new OrderItemMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderItemsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderItems
        /// <summary>
        /// Get all OrderItems
        /// </summary>
        /// <returns>A collection of OrderItems</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderItemView>))]
        public async Task<ActionResult<IEnumerable<OrderItemView>>> GetOrderItems()
        {
            var query = await _bll.OrderItems.GetAllAsync();
            return Ok(query.Select(e => _orderItemMapper.MapForOrderItemView(e)));
        }

        // GET: api/OrderItems/5
        /// <summary>
        /// Get a single OrderItem
        /// </summary>
        /// <param name="id">OrderItem Id</param>
        /// <returns>OrderItem object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItemView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<OrderItemView>> GetOrderItem(Guid id)
        {
            var orderItem = await _bll.OrderItems.FirstOrDefaultAsync(id);

            if (orderItem == null)
            {
                return NotFound(new MessageDTO("OrderItem not found"));
            }

            return Ok(_orderItemMapper.MapForOrderItemView(orderItem));
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update an OrderItem
        /// </summary>
        /// <param name="id">OrderItem Id</param>
        /// <param name="orderItem">OrderItem object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutOrderItem(Guid id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest(new MessageDTO("Id and orderItem.Id do not match"));
            }

            if (!await _bll.OrderItems.ExistsAsync(orderItem.Id))
            {
                return NotFound(new MessageDTO($"OrderItem does not exist"));
            }

            var bllEntity = _orderItemMapper.Map(orderItem);
            await _bll.OrderItems.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/OrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create an OrderItem
        /// </summary>
        /// <param name="orderItem">OrderItem object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderItem))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
        {
            var bllEntity = _orderItemMapper.Map(orderItem);
            _bll.OrderItems.Add(bllEntity);
            await _bll.OrderItems.ReducePaintingQuantityAsync(bllEntity);
            await _bll.SaveChangesAsync();

            orderItem.Id = bllEntity.Id;

            return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
        }

        // DELETE: api/OrderItems/5
        /// <summary>
        /// Delete an OrderItem
        /// </summary>
        /// <param name="id">OrderItem Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(Guid id)
        {
            var orderItem = await _bll.OrderItems.FirstOrDefaultAsync(id);
            
            if (orderItem == null)
            {
                return NotFound(new MessageDTO("OrderItem does not exist"));
            }

            await _bll.OrderItems.RemoveAsync(orderItem);
            await _bll.SaveChangesAsync();

            return Ok(_orderItemMapper.Map(orderItem));
        }
    }
}
