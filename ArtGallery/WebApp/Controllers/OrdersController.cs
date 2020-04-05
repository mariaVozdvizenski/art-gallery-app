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
using Extensions;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "User")]
    public class OrdersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public OrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Orders.AllAsync(User.UserGuidId());
            return View(await appDbContext);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderStatusCodeId"] = new SelectList(await _uow.OrderStatusCodes.AllAsync(), "Id", "Code");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Order order)
        {
            order.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                _uow.Orders.Add(order);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderStatusCodeId"] = new SelectList(await _uow.OrderStatusCodes.AllAsync(), "Id", "Code", order.OrderStatusCodeId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders.FirstOrDefaultAsync(id, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }
            ViewData["OrderStatusCodeId"] = new SelectList(await _uow.OrderStatusCodes.AllAsync(), "Id", "Code", order.OrderStatusCodeId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Order order)
        {
            order.AppUserId = User.UserGuidId();
            
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Orders.Update(order);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Orders.ExistsAsync(id, User.UserGuidId()))
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
            ViewData["OrderStatusCodeId"] = new SelectList(await _uow.OrderStatusCodes.AllAsync(), "Id", "Code", order.OrderStatusCodeId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _uow.Orders
                .FirstOrDefaultAsync( id, User.UserGuidId());
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Orders.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
