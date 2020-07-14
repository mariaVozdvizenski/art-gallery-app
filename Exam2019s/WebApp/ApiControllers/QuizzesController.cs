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
using Quiz = Domain.Quiz;

namespace WebApp.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly QuizMapper _quizMapper = new QuizMapper();

        public QuizzesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Quizzes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<QuizView>>> GetQuizzes(string? categories)
        {
            var query =  _context.Quizzes.AsQueryable();
            query = query
                .Include(e => e.QuizResults)
                .Include(e => e.QuizType)
                .Include(e => e.QuizQuestions)
                .ThenInclude(e => e.QuestionAnswers);

            if (categories != null)
            {
                var list = categories.Split('_');
                query = query.Where(e => list.Contains(e.QuizType!.Type));
            }
            
            var quizzes = await query.ToListAsync();

            return Ok(quizzes.Select(e => _quizMapper.MapForQuizView(e)));
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<QuizView>> GetQuiz(Guid id)
        {
            var query =  _context.Quizzes.AsQueryable();
            
            var quiz = await query
                .Include(e => e.QuizResults)
                .Include(e => e.QuizType)
                .Include(e => e.QuizQuestions)
                .ThenInclude(e => e.QuestionAnswers)
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if (quiz == null)
            {
                return NotFound();
            }

            var quizView = _quizMapper.MapForQuizView(quiz);

            return Ok(_quizMapper.MapForQuizView(quiz));
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(Guid id, PublicApi.DTO.Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            var domainEntity = _quizMapper.Map(quiz);
            _context.Quizzes.Update(domainEntity);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicApi.DTO.Quiz>> PostQuiz(PublicApi.DTO.Quiz quiz)
        {
            var domainEntity = _quizMapper.Map(quiz);
            await _context.Quizzes.AddAsync(domainEntity);
            await _context.SaveChangesAsync();

            quiz.Id = domainEntity.Id;
            
            return CreatedAtAction("GetQuiz", new { id = quiz.Id }, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicApi.DTO.Quiz>> DeleteQuiz(Guid id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return _quizMapper.Map(quiz);
        }
    }
}
