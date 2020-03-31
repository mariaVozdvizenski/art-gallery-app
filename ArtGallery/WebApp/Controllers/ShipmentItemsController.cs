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
    public class ShipmentItemsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ShipmentItemsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ShipmentItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.ShipmentItems.AllAsync();
            return View(await appDbContext);
        }

        // GET: ShipmentItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentItem = await _uow.ShipmentItems
                .FirstOrDefaultAsync(id);
            if (shipmentItem == null)
            {
                return NotFound();
            }

            return View(shipmentItem);
        }

        // GET: ShipmentItems/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderItemId"] = new SelectList(await _uow.OrderItems.AllAsync(), "Id", "Id");
            ViewData["ShipmentId"] = new SelectList(await _uow.Shipments.AllAsync(), "Id", "Id");
            return View();
        }

        // POST: ShipmentItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderItemId,ShipmentId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ShipmentItem shipmentItem)
        {
            if (ModelState.IsValid)
            {
                shipmentItem.Id = Guid.NewGuid();
                _uow.ShipmentItems.Add(shipmentItem);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderItemId"] = new SelectList(await _uow.OrderItems.AllAsync(), "Id", "Id", shipmentItem.OrderItemId);
            ViewData["ShipmentId"] = new SelectList(await _uow.Shipments.AllAsync(), "Id", "Id", shipmentItem.ShipmentId);
            return View(shipmentItem);
        }

        // GET: ShipmentItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentItem = await _uow.ShipmentItems.FindAsync(id);
            if (shipmentItem == null)
            {
                return NotFound();
            }
            ViewData["OrderItemId"] = new SelectList(await _uow.OrderItems.AllAsync(), "Id", "Id", shipmentItem.OrderItemId);
            ViewData["ShipmentId"] = new SelectList(await _uow.Shipments.AllAsync(), "Id", "Id", shipmentItem.ShipmentId);
            return View(shipmentItem);
        }

        // POST: ShipmentItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderItemId,ShipmentId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] ShipmentItem shipmentItem)
        {
            if (id != shipmentItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ShipmentItems.Update(shipmentItem);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ShipmentItemExists(shipmentItem.Id))
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
            ViewData["OrderItemId"] = new SelectList(await _uow.OrderItems.AllAsync(), "Id", "Id", shipmentItem.OrderItemId);
            ViewData["ShipmentId"] = new SelectList(await _uow.Shipments.AllAsync(), "Id", "Id", shipmentItem.ShipmentId);
            return View(shipmentItem);
        }

        // GET: ShipmentItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentItem = await _uow.ShipmentItems
                .FirstOrDefaultAsync(id);
            if (shipmentItem == null)
            {
                return NotFound();
            }

            return View(shipmentItem);
        }

        // POST: ShipmentItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shipmentItem = await _uow.ShipmentItems.FindAsync(id);
            _uow.ShipmentItems.Remove(shipmentItem);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool>  ShipmentItemExists(Guid id)
        {
            return await _uow.ShipmentItems.ExistsAsync(id);
        }
    }
}
