using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PaintingCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public PaintingCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: PaintingCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaintingCategories.ToListAsync());
        }

        // GET: PaintingCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paintingCategory = await _context.PaintingCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paintingCategory == null)
            {
                return NotFound();
            }

            return View(paintingCategory);
        }

        // GET: PaintingCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaintingCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaintingId,CategoryId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PaintingCategory paintingCategory)
        {
            if (ModelState.IsValid)
            {
                paintingCategory.Id = Guid.NewGuid();
                _context.Add(paintingCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paintingCategory);
        }

        // GET: PaintingCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paintingCategory = await _context.PaintingCategories.FindAsync(id);
            if (paintingCategory == null)
            {
                return NotFound();
            }
            return View(paintingCategory);
        }

        // POST: PaintingCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PaintingId,CategoryId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] PaintingCategory paintingCategory)
        {
            if (id != paintingCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paintingCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaintingCategoryExists(paintingCategory.Id))
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
            return View(paintingCategory);
        }

        // GET: PaintingCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paintingCategory = await _context.PaintingCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paintingCategory == null)
            {
                return NotFound();
            }

            return View(paintingCategory);
        }

        // POST: PaintingCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paintingCategory = await _context.PaintingCategories.FindAsync(id);
            _context.PaintingCategories.Remove(paintingCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaintingCategoryExists(Guid id)
        {
            return _context.PaintingCategories.Any(e => e.Id == id);
        }
    }
}
