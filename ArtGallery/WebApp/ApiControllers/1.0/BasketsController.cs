using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BasketsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public BasketsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Baskets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketDTO>>> GetBaskets()
        {
            var basketList = _uow.Baskets.DTOAllAsync(User.UserGuidId());
            
            return Ok(await basketList);
        }

        // GET: api/Baskets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(Guid id)
        {
            var basket = _uow.Baskets.DTOFirstOrDefaultAsync(id, User.UserGuidId());
            
            if (basket == null)
            {
                return NotFound();
            }
            
            return Ok(await basket);
        }

        // PUT: api/Baskets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasket(Guid id, Basket basket)
        {
            if (id != basket.Id)
            {
                return BadRequest();
            }

            var basketUpdate = await _uow.Baskets.FirstOrDefaultAsync(basket.Id, User.UserGuidId());
            
            if (basketUpdate == null)
            {
                return BadRequest();
            }

            basketUpdate.DateCreated = basket.DateCreated;

            _uow.Baskets.Update(basketUpdate);
                
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _uow.Baskets.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Baskets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Basket>> PostBasket(Basket basket)
        {
            _uow.Baskets.Add(basket);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
        }

        // DELETE: api/Baskets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Basket>> DeleteBasket(Guid id)
        {
            await _uow.Baskets.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
