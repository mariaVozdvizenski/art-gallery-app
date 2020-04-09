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
    public class OrderStatusCodesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrderStatusCodesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: OrderStatusCodes
        public async Task<IActionResult> Index()
        {
            return View(await _uow.OrderStatusCodes.AllAsync());
        }

        // GET: OrderStatusCodes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var orderStatusCode = await _uow.OrderStatusCodes.FirstOrDefaultAsync(id);
            
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
        public async Task<IActionResult> Create([Bind("OrderStatusDescription,Code,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderStatusCode orderStatusCode)
        {
            if (ModelState.IsValid)
            {
                orderStatusCode.Id = Guid.NewGuid();
                _uow.OrderStatusCodes.Add(orderStatusCode);
                await _uow.SaveChangesAsync();
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

            var orderStatusCode = await _uow.OrderStatusCodes.FirstOrDefaultAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("OrderStatusDescription,Code,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] OrderStatusCode orderStatusCode)
        {
            if (id != orderStatusCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.OrderStatusCodes.Update(orderStatusCode);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.OrderStatusCodes.ExistsAsync(id))
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

            var orderStatusCode = await _uow.OrderStatusCodes
                .FirstOrDefaultAsync(id);
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
            await _uow.OrderStatusCodes.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
