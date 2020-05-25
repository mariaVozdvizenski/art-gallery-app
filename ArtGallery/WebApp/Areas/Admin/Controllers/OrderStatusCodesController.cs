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
    public class OrderStatusCodesController : Controller
    {
        private readonly AppDbContext _context;

        public OrderStatusCodesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderStatusCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatusCodes.ToListAsync());
        }

        // GET: OrderStatusCodes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusCode = await _context.OrderStatusCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatusCode == null)
            {
                return NotFound();
            }

            return View(orderStatusCode);
        }

        // GET: OrderStatusCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatusCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatusDescription,Code,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] OrderStatusCode orderStatusCode)
        {
            if (ModelState.IsValid)
            {
                orderStatusCode.Id = Guid.NewGuid();
                _context.Add(orderStatusCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatusCode);
        }

        // GET: OrderStatusCodes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusCode = await _context.OrderStatusCodes.FindAsync(id);
            if (orderStatusCode == null)
            {
                return NotFound();
            }
            return View(orderStatusCode);
        }

        // POST: OrderStatusCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderStatusDescription,Code,CreatedBy,CreatedAt,ChangedBy,ChangedAt,Id")] OrderStatusCode orderStatusCode)
        {
            if (id != orderStatusCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatusCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusCodeExists(orderStatusCode.Id))
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
            return View(orderStatusCode);
        }

        // GET: OrderStatusCodes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusCode = await _context.OrderStatusCodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatusCode == null)
            {
                return NotFound();
            }

            return View(orderStatusCode);
        }

        // POST: OrderStatusCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderStatusCode = await _context.OrderStatusCodes.FindAsync(id);
            _context.OrderStatusCodes.Remove(orderStatusCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusCodeExists(Guid id)
        {
            return _context.OrderStatusCodes.Any(e => e.Id == id);
        }
    }
}
