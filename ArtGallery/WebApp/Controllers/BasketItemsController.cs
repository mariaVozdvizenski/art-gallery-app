using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain;

namespace WebApp.Controllers
{
    public class BasketItemsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public BasketItemsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: BasketItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.BasketItems.AllAsync();
            return View(await appDbContext);
        }

        // GET: BasketItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var basketItem = await _uow.BasketItems.FirstOrDefaultAsync(id);
            if (basketItem == null)
            {
                return NotFound();
            }
            
            return View(basketItem);
        }

        // GET: BasketItems/Create
        public async Task<IActionResult> Create()
        {
            ViewData["BasketId"] = new SelectList( await _uow.Baskets.AllAsync(), nameof(Basket.Id), nameof(Basket.Id));
            ViewData["PaintingId"] = new SelectList( await _uow.Paintings.AllAsync(), nameof(Painting.Title), nameof(Painting.Title));
            return View();
        }

        // POST: BasketItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Quantity,DateCreated,BasketId,PaintingId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] BasketItem basketItem)
        {
            if (ModelState.IsValid)
            {
                basketItem.Id = Guid.NewGuid();
                _uow.BasketItems.Add(basketItem);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BasketId"] = new SelectList(await _uow.Baskets.AllAsync(), "Id", "Id", basketItem.BasketId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Id", basketItem.PaintingId);
            return View(basketItem);
        }

        // GET: BasketItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _uow.BasketItems.FindAsync(id);
            if (basketItem == null)
            {
                return NotFound();
            }

            ViewData["BasketId"] = new SelectList(await _uow.Baskets.AllAsync(), "Id", "Id", basketItem.BasketId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Id", basketItem.PaintingId);
            return View(basketItem);
        }

        // POST: BasketItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Quantity,DateCreated,BasketId,PaintingId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] BasketItem basketItem)
        {
            if (id != basketItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.BasketItems.Update(basketItem);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await(BasketItemExists(basketItem.Id)))
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
            
            ViewData["BasketId"] = new SelectList(await _uow.Baskets.AllAsync(), "Id", "Id", basketItem.BasketId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", basketItem.PaintingId);
            return View(basketItem);
        }

        // GET: BasketItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basketItem = await _uow.BasketItems.FirstOrDefaultAsync(id);
            
            if (basketItem == null)
            {
                return NotFound();
            }

            return View(basketItem);
        }

        // POST: BasketItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var basketItem = await _uow.BasketItems.FindAsync(id);
            _uow.BasketItems.Remove(basketItem);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BasketItemExists(Guid id)
        {
            return await _uow.BasketItems.ExsistsAsync(id);
        }
    }
}
