using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;

namespace WebApp.Controllers
{
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
        public async Task<IActionResult> Details(string id)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderStatusDescription,Code,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] OrderStatusCode orderStatusCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatusCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatusCode);
        }

        // GET: OrderStatusCodes/Edit/5
        public async Task<IActionResult> Edit(string id)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderStatusDescription,Code,CreatedBy,CreatedAt,DeletedBy,DeletedAt,Id")] OrderStatusCode orderStatusCode)
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
        public async Task<IActionResult> Delete(string id)
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
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderStatusCode = await _context.OrderStatusCodes.FindAsync(id);
            _context.OrderStatusCodes.Remove(orderStatusCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusCodeExists(string id)
        {
            return _context.OrderStatusCodes.Any(e => e.Id == id);
        }
    }
}
