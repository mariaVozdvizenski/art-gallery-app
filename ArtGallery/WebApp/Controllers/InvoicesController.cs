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
    public class InvoicesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public InvoicesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Invoices.AllAsync();
            return View(await appDbContext);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices
                .FirstOrDefaultAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            ViewData["InvoiceStatusCodeId"] = new SelectList(await _uow.InvoiceStatusCodes.AllAsync(), "Id", "Code");
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceNumber,InvoiceDate,InvoiceDetails,OrderId,InvoiceStatusCodeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.Id = Guid.NewGuid();
                _uow.Invoices.Add(invoice);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceStatusCodeId"] = new SelectList(await _uow.InvoiceStatusCodes.AllAsync(), "Id", "Code", invoice.InvoiceStatusCodeId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", invoice.OrderId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["InvoiceStatusCodeId"] = new SelectList(await _uow.InvoiceStatusCodes.AllAsync(), "Id", "Code", invoice.InvoiceStatusCodeId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", invoice.OrderId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceNumber,InvoiceDate,InvoiceDetails,OrderId,InvoiceStatusCodeId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Invoices.Update(invoice);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InvoiceExists(invoice.Id))
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
            ViewData["InvoiceStatusCodeId"] = new SelectList(await _uow.InvoiceStatusCodes.AllAsync(), "Id", "Code", invoice.InvoiceStatusCodeId);
            ViewData["OrderId"] = new SelectList(await _uow.Orders.AllAsync(), "Id", "Id", invoice.OrderId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _uow.Invoices
                .FirstOrDefaultAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Invoices.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> InvoiceExists(Guid id)
        {
            return await _uow.Invoices.ExistsAsync(id);
        }
    }
}
