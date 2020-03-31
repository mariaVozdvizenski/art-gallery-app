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
    public class PaintingsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaintingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Paintings
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Paintings.AllAsync();
            return View(await appDbContext);
        }

        // GET: Paintings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _uow.Paintings.FirstOrDefaultAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            return View(painting);
        }

        // GET: Paintings/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ArtistId"] = new SelectList(await _uow.Artists.AllAsync(), "Id", "Bio");
            return View();
        }

        // POST: Paintings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Price,Title,Size,ArtistId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Painting painting)
        {
            if (ModelState.IsValid)
            {
                painting.Id = Guid.NewGuid();
                _uow.Paintings.Add(painting);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistId"] = new SelectList(await _uow.Artists.AllAsync(), "Id", "Bio", painting.ArtistId);
            return View(painting);
        }

        // GET: Paintings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _uow.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }
            ViewData["ArtistId"] = new SelectList(await _uow.Artists.AllAsync(), "Id", "Bio", painting.ArtistId);
            return View(painting);
        }

        // POST: Paintings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description,Price,Title,Size,ArtistId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Painting painting)
        {
            if (id != painting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Paintings.Update(painting);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PaintingExists(painting.Id))
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
            ViewData["ArtistId"] = new SelectList(await _uow.Artists.AllAsync(), "Id", "Bio", painting.ArtistId);
            return View(painting);
        }

        // GET: Paintings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var painting = await _uow.Paintings.FirstOrDefaultAsync(id);
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
            var painting = await _uow.Paintings.FindAsync(id);
            _uow.Paintings.Remove(painting);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PaintingExists(Guid id)
        {
            return await _uow.Paintings.ExistsAsync(id);
        }
    }
}
