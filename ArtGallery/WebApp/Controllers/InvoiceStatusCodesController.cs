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
    public class InvoiceStatusCodesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public InvoiceStatusCodesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: InvoiceStatusCodes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.InvoiceStatusCodes.AllAsync());
        }

        // GET: InvoiceStatusCodes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatusCode = await _uow.InvoiceStatusCodes
                .FindAsync(id);
            if (invoiceStatusCode == null)
            {
                return NotFound();
            }

            return View(invoiceStatusCode);
        }

        // GET: InvoiceStatusCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceStatusCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceStatusDescription,Code,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] InvoiceStatusCode invoiceStatusCode)
        {
            if (ModelState.IsValid)
            {
                invoiceStatusCode.Id = Guid.NewGuid();
                _uow.InvoiceStatusCodes.Add(invoiceStatusCode);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceStatusCode);
        }

        // GET: InvoiceStatusCodes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatusCode = await _uow.InvoiceStatusCodes.FindAsync(id);
            if (invoiceStatusCode == null)
            {
                return NotFound();
            }
            return View(invoiceStatusCode);
        }

        // POST: InvoiceStatusCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceStatusDescription,Code,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] InvoiceStatusCode invoiceStatusCode)
        {
            if (id != invoiceStatusCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.InvoiceStatusCodes.Update(invoiceStatusCode);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await InvoiceStatusCodeExists(invoiceStatusCode.Id))
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
            return View(invoiceStatusCode);
        }

        // GET: InvoiceStatusCodes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatusCode = await _uow.InvoiceStatusCodes.FindAsync();
            if (invoiceStatusCode == null)
            {
                return NotFound();
            }

            return View(invoiceStatusCode);
        }

        // POST: InvoiceStatusCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoiceStatusCode = await _uow.InvoiceStatusCodes.FindAsync(id);
            _uow.InvoiceStatusCodes.Remove(invoiceStatusCode);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> InvoiceStatusCodeExists(Guid id)
        {
            return await _uow.InvoiceStatusCodes.ExistsAsync(id);
        }
    }
}
