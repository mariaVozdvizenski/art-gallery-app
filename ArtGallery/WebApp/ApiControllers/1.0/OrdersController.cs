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
using Order = PublicApi.DTO.v1.Order;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Orders
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderMapper _orderMapper = new OrderMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Orders
        /// <summary>
        /// Get all Orders
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="statusCodes"></param>
        /// <returns>A collection of Orders</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AdminOrderView>))]
        public async Task<ActionResult<IEnumerable<AdminOrderView>>> GetOrders(string? condition, 
            string? statusCodes)
        {
            if (User.IsInRole("admin"))
            {
                var adminQuery = await _bll.Orders.GetAllAsync();
                adminQuery = await _bll.Orders.ApplyAllFiltersAsync(adminQuery, condition, statusCodes);
                return Ok(adminQuery.Select(e => _orderMapper.MapForAdminViewAsync(e)));
            } 
            var query = await _bll.Orders.GetAllAsync(User.UserGuidId());
            query = await _bll.Orders.ApplyAllFiltersAsync(query, condition, statusCodes);
            return Ok(query.Select(e => _orderMapper.MapForAdminViewAsync(e)));
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get a single Order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order object</returns>
        [HttpGet("{id}")]        
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AdminOrderView))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<AdminOrderView>> GetOrder(Guid id)
        {
            if (User.IsInRole("admin"))
            {
                var adminOrder = await _bll.Orders.FirstOrDefaultAsync(id);
                if (adminOrder == null)
                {
                    return NotFound("Order not found");
                }
                return _orderMapper.MapForAdminViewAsync(adminOrder);
            }
            var order = await _bll.Orders.FirstOrDefaultAsync(id, User.UserGuidId());
            if (order == null)
            {
                return NotFound("Order not found");
            }
            return _orderMapper.MapForAdminViewAsync(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update an Order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="order">Order object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(new MessageDTO("Id and order.Id do not match"));
            }
            
            if (User.IsInRole("admin"))
            {
                if (!await _bll.Orders.ExistsAsync(order.Id))
                {
                    return NotFound(new MessageDTO($"Order does not exist"));
                }
            }
            else
            {
                if (!await _bll.Orders.ExistsAsync(order.Id, User.UserGuidId()))
                {
                    return NotFound(new MessageDTO($"Order does not exist"));
                }
            }

            var newOrder = _orderMapper.Map(order);
            var oldOrder = await _bll.Orders.FirstOrDefaultAsync(newOrder.Id);
            await _bll.Orders.PaintingQuantityOnCodeChange(oldOrder, newOrder.OrderStatusCodeId, newOrder);
            await _bll.Orders.UpdateAsync(newOrder);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create an Order
        /// </summary>
        /// <param name="order">Order object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            order.OrderDate = DateTime.Now;
            var bllEntity = _orderMapper.Map(order);
            _bll.Orders.Add(bllEntity);
            await _bll.SaveChangesAsync();

            order.Id = bllEntity.Id;

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete an Order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            
            if (order == null)
            {
                return NotFound(new MessageDTO("Order not found"));
            }

            await _bll.Orders.RemoveAsync(order);
            await _bll.SaveChangesAsync();

            return Ok(_orderMapper.Map(order));
        }
    }
}
