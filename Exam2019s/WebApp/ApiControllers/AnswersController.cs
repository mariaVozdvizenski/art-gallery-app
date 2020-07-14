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
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AnswerMapper _answerMapper = new AnswerMapper();

        public AnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.Answer>>> GetAnswers()
        {
            var query = _context.Answers.AsQueryable();
            var domainEntities = await query.ToListAsync();
            return Ok(domainEntities.Select(e => _answerMapper.Map(e)));
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PublicApi.DTO.Answer>> GetAnswer(Guid id)
        {
            var query = _context.Answers.AsQueryable();
            
            var answer = await query
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (answer == null)
            {
                return NotFound();
            }

            return Ok(_answerMapper.Map(answer));
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(Guid id, PublicApi.DTO.Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            var domainEntity = _answerMapper.Map(answer);
            _context.Answers.Update(domainEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Answers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.Answer>> PostAnswer(PublicApi.DTO.Answer answer)
        {
            var domainEntity = _answerMapper.Map(answer);
            await _context.Answers.AddAsync(domainEntity);
            await _context.SaveChangesAsync();

            answer.Id = domainEntity.Id;

            return CreatedAtAction("GetAnswer", new { id = answer.Id }, answer);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.DTO.Answer>> DeleteAnswer(Guid id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return _answerMapper.Map(answer);
        }
        
    }
}
