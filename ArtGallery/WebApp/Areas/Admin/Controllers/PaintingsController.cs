using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class PaintingsController : Controller
    {
        private readonly AppDbContext _context;

        public PaintingsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Paintings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Paintings.Include(p => p.Artist);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Paintings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings
                .Include(p => p.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // GET: Paintings/Create
        public IActionResult Create()
        {
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "FirstName");
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Price,Title,Size,ArtistId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id,Quantity")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                painting.Id = Guid.NewGuid();
                _context.Add(painting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "FirstName", painting.ArtistId);
            return View(painting);
        }

        // GET: Paintings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Bio", painting.ArtistId);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description,Price,Title,Size,ArtistId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id,Quantity")] Painting painting)
        {
            if (id != painting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(painting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaintingExists(painting.Id))
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
            ViewData["ArtistId"] = new SelectList(_context.Artists, "Id", "Bio", painting.ArtistId);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _context.Paintings
                .Include(p => p.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // POST: Paintings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaintingExists(Guid id)
        {
            return _context.Paintings.Any(e => e.Id == id);
        }
    }
}
