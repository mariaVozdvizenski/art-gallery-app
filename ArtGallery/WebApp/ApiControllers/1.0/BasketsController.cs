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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BasketsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BasketMapper _basketMapper = new BasketMapper();

        public BasketsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Baskets
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<IEnumerable<BasketView>>> GetBaskets()
        {
            var basketList = await _bll.Baskets.GetAllAsync();
            return Ok(basketList.Select(e => _basketMapper.MapBasketView(e)));
        }

        // GET: api/Baskets/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<BasketView>> GetBasket(Guid id)
        {
            var basket = await _bll.Baskets.FirstOrDefaultAsync(id);
            
            if (basket == null)
            {
                return NotFound();
            }
            
            return _basketMapper.MapBasketView(basket);
        }

        // PUT: api/Baskets/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> PutBasket(Guid id, Basket basket)
        {
            basket.DateCreated = DateTime.Now;
            if (id != basket.Id)
            {
                return BadRequest();
            }

            var bllEntity = _basketMapper.Map(basket);
            await _bll.Baskets.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Baskets
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
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
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Domain.App.Basket>> DeleteBasket(Guid id)
        {
            var basket = await _bll.Baskets.FirstOrDefaultAsync(id);
            
            if (basket == null)
            {
                return NotFound();
            }

            await _bll.Baskets.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(basket);
        }
    }
}
