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
    public class ShipmentsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ShipmentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Shipments.AllAsync();
            return View(await appDbContext);
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _uow.Shipments
                .FirstOrDefaultAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public async Task<IActionResult>  Create()
        {
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(), "Id", "InvoiceDetails");
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,InvoiceId,ShipmentDate,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.Id = Guid.NewGuid();
                _uow.Shipments.Add(shipment);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(), "Id", "InvoiceDetails", shipment.InvoiceId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", shipment.OrderId);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _uow.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(), "Id", "InvoiceDetails", shipment.InvoiceId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", shipment.OrderId);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,InvoiceId,ShipmentDate,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Shipment shipment)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Shipments.Update(shipment);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await ShipmentExists(shipment.Id))
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
            ViewData["InvoiceId"] = new SelectList(await _uow.Invoices.AllAsync(), "Id", "InvoiceDetails", shipment.InvoiceId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", shipment.OrderId);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _uow.Shipments
                .FirstOrDefaultAsync( id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Shipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shipment = await _uow.Shipments.FindAsync(id);
            _uow.Shipments.Remove(shipment);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ShipmentExists(Guid id)
        {
            return await _uow.Shipments.ExistsAsync(id);
        }
    }
}
