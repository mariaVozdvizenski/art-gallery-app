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
    /// Order Status Codes
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class OrderStatusCodesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderStatusCodeMapper _orderStatusCodeMapper = new OrderStatusCodeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public OrderStatusCodesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/OrderStatusCodes
        /// <summary>
        /// Get all OrderStatusCodes
        /// </summary>
        /// <returns>Collection of OrderStatusCodes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderStatusCode>))]
        public async Task<ActionResult<IEnumerable<OrderStatusCode>>> GetOrderStatusCodes()
        {
            var query = await _bll.OrderStatusCodes.GetAllAsync();
            return Ok(query.Select(e => _orderStatusCodeMapper.Map(e)));
        }

        // GET: api/OrderStatusCodes/5
        /// <summary>
        /// Get a single OrderStatusCode
        /// </summary>
        /// <param name="id">OrderStatusCode Id</param>
        /// <returns>OrderStatusCode object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderStatusCode))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<OrderStatusCode>> GetOrderStatusCode(Guid id)
        {
            var orderStatusCode = await _bll.OrderStatusCodes.FirstOrDefaultAsync(id);

            if (orderStatusCode == null)
            {
                return NotFound(new MessageDTO("OrderStatusCode not found"));
            }

            return _orderStatusCodeMapper.Map(orderStatusCode);
        }

        // PUT: api/OrderStatusCodes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update an OrderStatusCode
        /// </summary>
        /// <param name="id">OrderStatusCode Id</param>
        /// <param name="orderStatusCode">OrderStatusCode object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutOrderStatusCode(Guid id, OrderStatusCode orderStatusCode)
        {
            if (id != orderStatusCode.Id)
            {
                return BadRequest(new MessageDTO("Id and orderStatusCode.Id do not match"));
            }

            if (!await _bll.OrderStatusCodes.ExistsAsync(orderStatusCode.Id))
            {
                return NotFound(new MessageDTO($"OrderStatusCode does not exist"));
            }

            var bllEntity = _orderStatusCodeMapper.Map(orderStatusCode);
            await _bll.OrderStatusCodes.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/OrderStatusCodes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create an OrderStatusCode
        /// </summary>
        /// <param name="orderStatusCode">OrderStatusCode object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderStatusCode))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderStatusCode>> PostOrderStatusCode(OrderStatusCode orderStatusCode)
        {
            var bllEntity = _orderStatusCodeMapper.Map(orderStatusCode);
            _bll.OrderStatusCodes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            orderStatusCode.Id = bllEntity.Id;

            return CreatedAtAction("GetOrderStatusCode", new { id = orderStatusCode.Id }, orderStatusCode);
        }

        // DELETE: api/OrderStatusCodes/5
        /// <summary>
        /// Delete OrderStatusCode
        /// </summary>
        /// <param name="id">OrderStatusCode Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderStatusCode))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderStatusCode>> DeleteOrderStatusCode(Guid id)
        {
            var orderStatusCode = await _bll.OrderStatusCodes.FirstOrDefaultAsync(id);
            
            if (orderStatusCode == null)
            {
                return NotFound(new MessageDTO("OrderStatusCode not found"));
            }

            await _bll.OrderStatusCodes.RemoveAsync(orderStatusCode);
            await _bll.SaveChangesAsync();

            return Ok(_orderStatusCodeMapper.Map(orderStatusCode));
        }
    }
}
