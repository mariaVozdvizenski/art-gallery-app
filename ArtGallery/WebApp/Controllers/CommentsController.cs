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
    public class CommentsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public CommentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _uow.Comments.AllAsync();
            return View(await appDbContext);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _uow.Comments
                .FirstOrDefaultAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["PaintingId"] = new SelectList(await _uow.Paintings.AllAsync(), "Id", nameof(Painting.Title));
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            comment.AppUserId = User.UserGuidId();
            
            if (ModelState.IsValid)
            {
                _uow.Comments.Add(comment);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        [Authorize]
        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _uow.Comments.FirstOrDefaultAsync(id, User.UserGuidId());
            if (comment == null)
            {
                return NotFound();
            }
          
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Comment comment)
        {
            comment.AppUserId = User.UserGuidId();

            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Comments.Update(comment);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.Comments.ExistsAsync(comment.Id, User.UserGuidId()))
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
            return View(comment);
        }

        [Authorize]
        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _uow.Comments
                .FirstOrDefaultAsync(id, User.UserGuidId());
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [Authorize]
        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Comments.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
