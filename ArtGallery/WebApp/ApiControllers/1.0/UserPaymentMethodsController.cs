using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.EF;
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

    
    public class UserPaymentMethodsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public UserPaymentMethodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/UserPaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPaymentMethodDTO>>> GetUserPaymentMethods()
        {
            return Ok(await _uow.UserPaymentMethods.DTOAllAsync(User.UserGuidId()));
        }

        // GET: api/UserPaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPaymentMethodDTO>> GetUserPaymentMethod(Guid id)
        {
            var userPaymentMethod = await _uow.UserPaymentMethods.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (userPaymentMethod == null)
            {
                return NotFound();
            }

            return userPaymentMethod;
        }

        // PUT: api/UserPaymentMethods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPaymentMethod(Guid id, UserPaymentMethod userPaymentMethod)
        {
            if (id != userPaymentMethod.Id)
            {
                return BadRequest();
            }

            _uow.UserPaymentMethods.Update(userPaymentMethod);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.UserPaymentMethods.ExistsAsync(userPaymentMethod.Id, User.UserGuidId()))
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

        // POST: api/UserPaymentMethods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserPaymentMethod>> PostUserPaymentMethod(UserPaymentMethod userPaymentMethod)
        {
            _uow.UserPaymentMethods.Add(userPaymentMethod);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetUserPaymentMethod", new { id = userPaymentMethod.Id }, userPaymentMethod);
        }

        // DELETE: api/UserPaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPaymentMethod>> DeleteUserPaymentMethod(Guid id)
        {
            await _uow.UserPaymentMethods.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
    }
}
