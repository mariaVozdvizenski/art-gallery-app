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
    [Authorize]
    public class UserPaymentMethodsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public UserPaymentMethodsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: UserPaymentMethods
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.UserPaymentMethods.AllAsync(User.UserGuidId());
            return View(await appDbContext);
        }

        // GET: UserPaymentMethods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPaymentMethod = await _uow.UserPaymentMethods
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (userPaymentMethod == null)
            {
                return NotFound();
            }

            return View(userPaymentMethod);
        }

        // GET: UserPaymentMethods/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(), "Id", "PaymentMethodCode");
            return View();
        }

        // POST: UserPaymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,PaymentMethodId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserPaymentMethod userPaymentMethod)
        {
            userPaymentMethod.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                _uow.UserPaymentMethods.Add(userPaymentMethod);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(), "Id", "PaymentMethodCode", userPaymentMethod.PaymentMethodId);
            return View(userPaymentMethod);
        }

        // GET: UserPaymentMethods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPaymentMethod = await _uow.UserPaymentMethods.FindAsync(id, User.UserGuidId());
            if (userPaymentMethod == null)
            {
                return NotFound();
            }
            ViewData["PaymentMethodId"] = new SelectList(await _uow.UserPaymentMethods.AllAsync(), "Id", "PaymentMethodCode", userPaymentMethod.PaymentMethodId);
            return View(userPaymentMethod);
        }

        // POST: UserPaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,PaymentMethodId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserPaymentMethod userPaymentMethod)
        {
            userPaymentMethod.AppUserId = User.UserGuidId();
            
            if (id != userPaymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.UserPaymentMethods.Update(userPaymentMethod);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.UserPaymentMethods.ExistsAsync(id, User.UserGuidId()))
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
            ViewData["PaymentMethodId"] = new SelectList(await _uow.PaymentMethods.AllAsync(), "Id", "PaymentMethodCode", userPaymentMethod.PaymentMethodId);
            return View(userPaymentMethod);
        }

        // GET: UserPaymentMethods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPaymentMethod = await _uow.UserPaymentMethods
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (userPaymentMethod == null)
            {
                return NotFound();
            }

            return View(userPaymentMethod);
        }

        // POST: UserPaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        { 
            await _uow.UserPaymentMethods.DeleteAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
    }
}
