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
    public class UserPaymentMethodsController : Controller
    {
        private readonly AppDbContext _context;

        public UserPaymentMethodsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserPaymentMethods
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserPaymentMethods.Include(u => u.AppUser).Include(u => u.PaymentMethod);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserPaymentMethods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPaymentMethod = await _context.UserPaymentMethods
                .Include(u => u.AppUser)
                .Include(u => u.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userPaymentMethod == null)
            {
                return NotFound();
            }

            return View(userPaymentMethod);
        }

        // GET: UserPaymentMethods/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodCode");
            return View();
        }

        // POST: UserPaymentMethods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,PaymentMethodId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserPaymentMethod userPaymentMethod)
        {
            if (ModelState.IsValid)
            {
                userPaymentMethod.Id = Guid.NewGuid();
                _context.Add(userPaymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userPaymentMethod.AppUserId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodCode", userPaymentMethod.PaymentMethodId);
            return View(userPaymentMethod);
        }

        // GET: UserPaymentMethods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPaymentMethod = await _context.UserPaymentMethods.FindAsync(id);
            if (userPaymentMethod == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userPaymentMethod.AppUserId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodCode", userPaymentMethod.PaymentMethodId);
            return View(userPaymentMethod);
        }

        // POST: UserPaymentMethods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,PaymentMethodId,Id,CreatedBy,CreatedAt,ChangedBy,ChangedAt")] UserPaymentMethod userPaymentMethod)
        {
            if (id != userPaymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userPaymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserPaymentMethodExists(userPaymentMethod.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", userPaymentMethod.AppUserId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "Id", "PaymentMethodCode", userPaymentMethod.PaymentMethodId);
            return View(userPaymentMethod);
        }

        // GET: UserPaymentMethods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userPaymentMethod = await _context.UserPaymentMethods
                .Include(u => u.AppUser)
                .Include(u => u.PaymentMethod)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var userPaymentMethod = await _context.UserPaymentMethods.FindAsync(id);
            _context.UserPaymentMethods.Remove(userPaymentMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserPaymentMethodExists(Guid id)
        {
            return _context.UserPaymentMethods.Any(e => e.Id == id);
        }
    }
}
