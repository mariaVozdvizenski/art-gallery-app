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
    public class QuestionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly QuestionMapper _questionMapper = new QuestionMapper();

        public QuestionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicApi.DTO.Question>>> GetQuestions()
        {
            var query = _context.Questions.AsQueryable();
            var domainEntities = await query.ToListAsync();
            return Ok(domainEntities.Select(e => _questionMapper.Map(e)));
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PublicApi.DTO.Question>> GetQuestion(Guid id)
        {
            var query = _context.Questions.AsQueryable();
            var question = await query
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (question == null)
            {
                return NotFound();
            }

            return _questionMapper.Map(question);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(Guid id, PublicApi.DTO.Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            var domainEntity = _questionMapper.Map(question);
            _context.Questions.Update(domainEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.Question>> PostQuestion(PublicApi.DTO.Question question)
        {
            var domainEntity = _questionMapper.Map(question);
            await _context.Questions.AddAsync(domainEntity);
            await _context.SaveChangesAsync();

            question.Id = domainEntity.Id;

            return CreatedAtAction("GetQuestion", new { id = question.Id }, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.DTO.Question>> DeleteQuestion(Guid id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return Ok(_questionMapper.Map(question));
        }
    }
}
