using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
    public class PaintingCategoriesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaintingCategoriesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PaintingCategories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.PaintingCategories.AllAsync();
            return View(await appDbContext);
        }

        // GET: PaintingCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paintingCategory = await _uow.PaintingCategories
                .FirstOrDefaultAsync(id);
            if (paintingCategory == null)
            {
                return NotFound();
            }

            return View(paintingCategory);
        }

        // GET: PaintingCategories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "Id", "CategoryName");
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description");
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
                _uow.PaintingCategories.Add(paintingCategory);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "Id", "CategoryName", paintingCategory.CategoryId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", paintingCategory.PaintingId);
            return View(paintingCategory);
        }

        // GET: PaintingCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paintingCategory = await _uow.PaintingCategories.FindAsync(id);
            if (paintingCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "Id", "CategoryName", paintingCategory.CategoryId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", paintingCategory.PaintingId);
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
                    _uow.PaintingCategories.Update(paintingCategory);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PaintingCategoryExists(paintingCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(await _uow.Categories.AllAsync(), "Id", "CategoryName", paintingCategory.CategoryId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", paintingCategory.PaintingId);
            return View(paintingCategory);
        }

        // GET: PaintingCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paintingCategory = await _uow.PaintingCategories.FirstOrDefaultAsync(id);
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
            var paintingCategory = await _uow.PaintingCategories.FindAsync(id);
            _uow.PaintingCategories.Remove(paintingCategory);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PaintingCategoryExists(Guid id)
        {
            return await _uow.PaintingCategories.ExistsAsync(id);
        }
    }
}
