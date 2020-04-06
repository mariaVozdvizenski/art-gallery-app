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
    public class OrderItemsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrderItemsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.OrderItems.AllAsync();
            return View(await appDbContext);
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _uow.OrderItems.FirstOrDefaultAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItems/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id");
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaintingId,OrderId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                orderItem.Id = Guid.NewGuid();
                _uow.OrderItems.Add(orderItem);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", orderItem.OrderId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", orderItem.PaintingId);
            return View(orderItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _uow.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", orderItem.OrderId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", orderItem.PaintingId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PaintingId,OrderId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.OrderItems.Update(orderItem);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderItemExists(orderItem.Id))
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
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", orderItem.OrderId);
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", "Description", orderItem.PaintingId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _uow.OrderItems.FirstOrDefaultAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderItem = await _uow.OrderItems.FindAsync(id);
            _uow.OrderItems.Remove(orderItem);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderItemExists(Guid id)
        {
            return await _uow.OrderItems.ExistsAsync(id);
        }
    }
}
