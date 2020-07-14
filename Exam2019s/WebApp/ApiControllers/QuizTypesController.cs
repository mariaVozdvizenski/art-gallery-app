using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO.Mappers;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    [ApiController]
    public class QuizTypesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly QuizTypeMapper _quizTypeMapper = new QuizTypeMapper();

        public QuizTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuizTypes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.QuizType>>> GetQuizTypes()
        {
            var query = _context.QuizTypes.AsQueryable();
            return Ok(query.Select(e => _quizTypeMapper.Map(e)));
        }

        // GET: api/QuizTypes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PublicApi.DTO.QuizType>> GetQuizType(Guid id)
        {
            var query = _context.QuizTypes.AsQueryable();
            
            var quizType = await query
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (quizType == null)
            {
                return NotFound();
            }

            return Ok(_quizTypeMapper.Map(quizType));
        }

        // PUT: api/QuizTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizType(Guid id, PublicApi.DTO.QuizType quizType)
        {
            if (id != quizType.Id)
            {
                return BadRequest();
            }

            var domainEntity = _quizTypeMapper.Map(quizType);
            _context.QuizTypes.Update(domainEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/QuizTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.QuizType>> PostQuizType(PublicApi.DTO.QuizType quizType)
        {
            var domainEntity = _quizTypeMapper.Map(quizType);
            await _context.QuizTypes.AddAsync(domainEntity);
            await _context.SaveChangesAsync();

            quizType.Id = domainEntity.Id;
            
            return CreatedAtAction("GetQuizType", new { id = quizType.Id }, quizType);
        }

        // DELETE: api/QuizTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.DTO.QuizType>> DeleteQuizType(Guid id)
        {
            var quizType = await _context.QuizTypes.FindAsync(id);
            if (quizType == null)
            {
                return NotFound();
            }

            _context.QuizTypes.Remove(quizType);
            await _context.SaveChangesAsync();

            return Ok(_quizTypeMapper.Map(quizType));
        }
    }
}
