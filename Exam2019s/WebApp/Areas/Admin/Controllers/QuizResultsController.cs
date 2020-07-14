using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class QuizResultsController : Controller
    {
        private readonly AppDbContext _context;

        public QuizResultsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuizResults
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.QuizResults.Include(q => q.Quiz);
            return View(await appDbContext.ToListAsync());
        }

        // GET: QuizResults/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResult = await _context.QuizResults
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizResult == null)
            {
                return NotFound();
            }

            return View(quizResult);
        }

        // GET: QuizResults/Create
        public IActionResult Create()
        {
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title");
            return View();
        }

        // POST: QuizResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuizId,CorrectAnswers")] QuizResult quizResult)
        {
            if (ModelState.IsValid)
            {
                quizResult.Id = Guid.NewGuid();
                _context.Add(quizResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title", quizResult.QuizId);
            return View(quizResult);
        }

        // GET: QuizResults/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResult = await _context.QuizResults.FindAsync(id);
            if (quizResult == null)
            {
                return NotFound();
            }
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title", quizResult.QuizId);
            return View(quizResult);
        }

        // POST: QuizResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,QuizId,CorrectAnswers")] QuizResult quizResult)
        {
            if (id != quizResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizResultExists(quizResult.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title", quizResult.QuizId);
            return View(quizResult);
        }

        // GET: QuizResults/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizResult = await _context.QuizResults
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizResult == null)
            {
                return NotFound();
            }

            return View(quizResult);
        }

        // POST: QuizResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quizResult = await _context.QuizResults.FindAsync(id);
            _context.QuizResults.Remove(quizResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizResultExists(Guid id)
        {
            return _context.QuizResults.Any(e => e.Id == id);
        }
    }
}
