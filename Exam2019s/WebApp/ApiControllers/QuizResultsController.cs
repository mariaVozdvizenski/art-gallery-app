using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PublicApi.DTO;
using PublicApi.DTO.Mappers;
using QuizResult = Domain.QuizResult;

namespace WebApp.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizResultsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly QuizResultMapper _quizResultMapper = new QuizResultMapper();

        public QuizResultsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuizResults
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<QuizResultView>>> GetQuizResults()
        {
            var query = _context.QuizResults.AsQueryable();
            query = query
                .Include(e => e.Quiz)
                .Include(e => e.Quiz!.QuizQuestions);
            
            var domainEntities = await query.ToListAsync();
            
            return Ok(domainEntities.Select(e => _quizResultMapper.MapForQuizResultView(e)));
        }

        // GET: api/QuizResults/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<QuizResultView>> GetQuizResult(Guid id)
        {
            var query = _context.QuizResults.AsQueryable();
            
            var quizResult = await query
                .Include(e => e.Quiz)
                .Include(e => e.Quiz!.QuizQuestions)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (quizResult == null)
            {
                return NotFound();
            }

            return Ok(_quizResultMapper.MapForQuizResultView(quizResult));
        }

        // PUT: api/QuizResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizResult(Guid id, PublicApi.DTO.QuizResult quizResult)
        {
            if (id != quizResult.Id)
            {
                return BadRequest();
            }

            var domainEntity = _quizResultMapper.Map(quizResult);
            _context.QuizResults.Update(domainEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/QuizResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<PublicApi.DTO.QuizResult>> PostQuizResult(PublicApi.DTO.QuizResult quizResult)
        {
            var domainEntity = _quizResultMapper.Map(quizResult);
            
            await _context.QuizResults.AddAsync(domainEntity);
            await _context.SaveChangesAsync();

            quizResult.Id = domainEntity.Id;

            return CreatedAtAction("GetQuizResult", new { id = quizResult.Id }, quizResult);
        }

        // DELETE: api/QuizResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.DTO.QuizResult>> DeleteQuizResult(Guid id)
        {
            var quizResult = await _context.QuizResults.FindAsync(id);
            if (quizResult == null)
            {
                return NotFound();
            }

            _context.QuizResults.Remove(quizResult);
            await _context.SaveChangesAsync();

            return Ok(_quizResultMapper.Map(quizResult));
        }
    }
}
