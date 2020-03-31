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
    public class BasketsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public BasketsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Baskets
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Baskets.AllAsync();
            return View(await appDbContext);
        }

        // GET: Baskets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _uow.Baskets.FirstOrDefaultAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // GET: Baskets/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AppUserId"] = new SelectList(await _uow.Baskets.GetUsers(), "Id", "Id");
            return View();
        }

        // POST: Baskets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateCreated,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Basket basket)
        {
            if (ModelState.IsValid)
            {
                basket.Id = Guid.NewGuid();
                _uow.Baskets.Add(basket);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(await _uow.Baskets.GetUsers(), "Id", "Id", basket.AppUserId);
            return View(basket);
        }

        // GET: Baskets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _uow.Baskets.FindAsync(id);
            if (basket == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(await _uow.Baskets.GetUsers(), "Id", "Id", basket.AppUserId);
            return View(basket);
        }

        // POST: Baskets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DateCreated,AppUserId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Basket basket)
        {
            if (id != basket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Baskets.Update(basket);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BasketExists(basket.Id))
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
            ViewData["AppUserId"] = new SelectList(await _uow.Baskets.GetUsers(), "Id", "Id", basket.AppUserId);
            return View(basket);
        }

        // GET: Baskets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basket = await _uow.Baskets
                .FirstOrDefaultAsync(id);
            if (basket == null)
            {
                return NotFound();
            }

            return View(basket);
        }

        // POST: Baskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var basket = await _uow.Baskets.FindAsync(id);
            _uow.Baskets.Remove(basket);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BasketExists(Guid id)
        {
            return await _uow.Baskets.ExistsAsync(id);
        }
    }
}
