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
    public class ShipmentItemsController : Controller
    {
        private readonly AppDbContext _context;

        public ShipmentItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ShipmentItems
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ShipmentItems.Include(s => s.OrderItem).Include(s => s.Shipment);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ShipmentItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentItem = await _context.ShipmentItems
                .Include(s => s.OrderItem)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shipmentItem == null)
            {
                return NotFound();
            }

            return View(shipmentItem);
        }

        // GET: ShipmentItems/Create
        public IActionResult Create()
        {
            ViewData["OrderItemId"] = new SelectList(_context.OrderItems, "Id", "Id");
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Id");
            return View();
        }

        // POST: ShipmentItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderItemId,ShipmentId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] ShipmentItem shipmentItem)
        {
            if (ModelState.IsValid)
            {
                shipmentItem.Id = Guid.NewGuid();
                _context.Add(shipmentItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderItemId"] = new SelectList(_context.OrderItems, "Id", "Id", shipmentItem.OrderItemId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Id", shipmentItem.ShipmentId);
            return View(shipmentItem);
        }

        // GET: ShipmentItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentItem = await _context.ShipmentItems.FindAsync(id);
            if (shipmentItem == null)
            {
                return NotFound();
            }
            ViewData["OrderItemId"] = new SelectList(_context.OrderItems, "Id", "Id", shipmentItem.OrderItemId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Id", shipmentItem.ShipmentId);
            return View(shipmentItem);
        }

        // POST: ShipmentItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderItemId,ShipmentId,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] ShipmentItem shipmentItem)
        {
            if (id != shipmentItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipmentItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentItemExists(shipmentItem.Id))
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
            ViewData["OrderItemId"] = new SelectList(_context.OrderItems, "Id", "Id", shipmentItem.OrderItemId);
            ViewData["ShipmentId"] = new SelectList(_context.Shipments, "Id", "Id", shipmentItem.ShipmentId);
            return View(shipmentItem);
        }

        // GET: ShipmentItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipmentItem = await _context.ShipmentItems
                .Include(s => s.OrderItem)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var shipmentItem = await _context.ShipmentItems.FindAsync(id);
            _context.ShipmentItems.Remove(shipmentItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentItemExists(Guid id)
        {
            return _context.ShipmentItems.Any(e => e.Id == id);
        }
    }
}