using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class InvoiceStatusCodesController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceStatusCodesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceStatusCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoiceStatusCodes.ToListAsync());
        }

        // GET: InvoiceStatusCodes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceStatusCode = await _context.InvoiceStatusCodes
                .FirstOrDefaultAsync(m => m.Id == id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceStatusDescription,Code,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] InvoiceStatusCode invoiceStatusCode)
        {
            if (ModelState.IsValid)
            {
                invoiceStatusCode.Id = Guid.NewGuid();
                _context.Add(invoiceStatusCode);
                await _context.SaveChangesAsync();
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

            var invoiceStatusCode = await _context.InvoiceStatusCodes.FindAsync(id);
            if (invoiceStatusCode == null)
            {
                return NotFound();
            }
            return View(invoiceStatusCode);
        }

        // POST: InvoiceStatusCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InvoiceStatusDescription,Code,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] InvoiceStatusCode invoiceStatusCode)
        {
            if (id != invoiceStatusCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceStatusCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceStatusCodeExists(invoiceStatusCode.Id))
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

            var invoiceStatusCode = await _context.InvoiceStatusCodes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var invoiceStatusCode = await _context.InvoiceStatusCodes.FindAsync(id);
            _context.InvoiceStatusCodes.Remove(invoiceStatusCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceStatusCodeExists(Guid id)
        {
            return _context.InvoiceStatusCodes.Any(e => e.Id == id);
        }
    }
}
