using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class OrdersController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            return Ok(await _uow.Orders.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
        {
            var order = await _uow.Orders.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, OrderEditDTO orderEditDTO)
        {
            if (id != orderEditDTO.Id)
            {
                return BadRequest();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(orderEditDTO.Id, User.UserGuidId());

            order.OrderDetails = orderEditDTO.OrderDetails;
            order.OrderStatusCodeId = orderEditDTO.OrderStatusCodeId;
            order.OrderStatusCode = await _uow.OrderStatusCodes.FirstOrDefaultAsync(orderEditDTO.OrderStatusCodeId);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await  _uow.Orders.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _uow.Orders.Add(order);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
        {
            var order = await _uow.Orders.FirstOrDefaultAsync(id, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            _uow.Orders.Remove(order);
            await _uow.SaveChangesAsync();

            return order;
        }
    }
}
