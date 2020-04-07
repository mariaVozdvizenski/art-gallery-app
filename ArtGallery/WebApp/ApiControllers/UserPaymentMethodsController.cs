using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPaymentMethodsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserPaymentMethodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserPaymentMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPaymentMethod>>> GetUserPaymentMethods()
        {
            return await _context.UserPaymentMethods.ToListAsync();
        }

        // GET: api/UserPaymentMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPaymentMethod>> GetUserPaymentMethod(Guid id)
        {
            var userPaymentMethod = await _context.UserPaymentMethods.FindAsync(id);

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

            _context.Entry(userPaymentMethod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPaymentMethodExists(id))
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
            _context.UserPaymentMethods.Add(userPaymentMethod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPaymentMethod", new { id = userPaymentMethod.Id }, userPaymentMethod);
        }

        // DELETE: api/UserPaymentMethods/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPaymentMethod>> DeleteUserPaymentMethod(Guid id)
        {
            var userPaymentMethod = await _context.UserPaymentMethods.FindAsync(id);
            if (userPaymentMethod == null)
            {
                return NotFound();
            }

            _context.UserPaymentMethods.Remove(userPaymentMethod);
            await _context.SaveChangesAsync();

            return userPaymentMethod;
        }

        private bool UserPaymentMethodExists(Guid id)
        {
            return _context.UserPaymentMethods.Any(e => e.Id == id);
        }
    }
}
