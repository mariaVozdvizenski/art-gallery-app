using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class BasketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BasketsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Baskets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketDTO>>> GetBaskets()
        {
            var basketList =  _context.Baskets
                .Where(b => b.AppUserId == User.UserGuidId())
                .Select(b => new BasketDTO()
                {
                    Id = b.Id,
                    DateCreated = b.DateCreated,
                    ItemCount = b.BasketItems.Count
                });
            
            return await basketList.ToListAsync();
        }

        // GET: api/Baskets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetBasket(Guid id)
        {
            var basket = _context.Baskets
                .Where(o => o.Id == id && o.AppUserId == User.UserGuidId())
                .Select(b => new BasketDTO()
                {
                    Id = b.Id,
                    DateCreated = b.DateCreated,
                    ItemCount = b.BasketItems.Count
                })
                .FirstOrDefaultAsync();
            
            if (basket == null)
            {
                return NotFound();
            }
            return await basket;
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

            _context.Entry(basket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketExists(id))
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
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
        }

        // DELETE: api/Baskets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Basket>> DeleteBasket(Guid id)
        {
            var basket = await _context.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();

            return basket;
        }

        private bool BasketExists(Guid id)
        {
            return _context.Baskets.Any(e => e.Id == id);
        }
    }
}
