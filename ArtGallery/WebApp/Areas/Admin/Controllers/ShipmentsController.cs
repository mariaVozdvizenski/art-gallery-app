using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class ShipmentsController : Controller
    {
        private readonly AppDbContext _context;

        public ShipmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Shipments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Shipments.Include(s => s.Invoice).Include(s => s.Order);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Shipments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Invoice)
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // GET: Shipments/Create
        public IActionResult Create()
        {
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceDetails");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: Shipments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,InvoiceId,ShipmentDate,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                shipment.Id = Guid.NewGuid();
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceDetails", shipment.InvoiceId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shipment.OrderId);
            return View(shipment);
        }

        // GET: Shipments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceDetails", shipment.InvoiceId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shipment.OrderId);
            return View(shipment);
        }

        // POST: Shipments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderId,InvoiceId,ShipmentDate,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] Shipment shipment)
        {
            if (id != shipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.Id))
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
            ViewData["InvoiceId"] = new SelectList(_context.Invoices, "Id", "InvoiceDetails", shipment.InvoiceId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", shipment.OrderId);
            return View(shipment);
        }

        // GET: Shipments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .Include(s => s.Invoice)
                .Include(s => s.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var shipment = await _context.Shipments.FindAsync(id);
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(Guid id)
        {
            return _context.Shipments.Any(e => e.Id == id);
        }
    }
}
