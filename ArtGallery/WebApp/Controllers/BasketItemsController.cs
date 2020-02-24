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
    public class BasketItemsController : Controller
    {
        private readonly AppDbContext _context;

        public BasketItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BasketItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.BasketItems.Include(b => b.Basket).Include(b => b.Painting);
            return View(await appDbContext.ToListAsync());
        }

        // GET: BasketItems/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _context.BasketItems
                .Include(b => b.Basket)
                .Include(b => b.Painting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketItem == null)
            {
                return NotFound();
            }

            return View(basketItem);
        }

        // GET: BasketItems/Create
        public IActionResult Create()
        {
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id");
            ViewData["PaintingId"] = new SelectList(_context.Paintings, "Id", "Id");
            return View();
        }

        // POST: BasketItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,DateCreated,BasketId,PaintingId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(basketItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id", basketItem.BasketId);
            ViewData["PaintingId"] = new SelectList(_context.Paintings, "Id", "Id", basketItem.PaintingId);
            return View(basketItem);
        }

        // GET: BasketItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _context.BasketItems.FindAsync(id);
            if (basketItem == null)
            {
                return NotFound();
            }
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id", basketItem.BasketId);
            ViewData["PaintingId"] = new SelectList(_context.Paintings, "Id", "Id", basketItem.PaintingId);
            return View(basketItem);
        }

        // POST: BasketItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Quantity,DateCreated,BasketId,PaintingId,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] BasketItem basketItem)
        {
            if (id != basketItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basketItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasketItemExists(basketItem.Id))
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
            ViewData["BasketId"] = new SelectList(_context.Baskets, "Id", "Id", basketItem.BasketId);
            ViewData["PaintingId"] = new SelectList(_context.Paintings, "Id", "Id", basketItem.PaintingId);
            return View(basketItem);
        }

        // GET: BasketItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _context.BasketItems
                .Include(b => b.Basket)
                .Include(b => b.Painting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basketItem == null)
            {
                return NotFound();
            }

            return View(basketItem);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var basketItem = await _context.BasketItems.FindAsync(id);
            _context.BasketItems.Remove(basketItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasketItemExists(string id)
        {
            return _context.BasketItems.Any(e => e.Id == id);
        }
    }
}
