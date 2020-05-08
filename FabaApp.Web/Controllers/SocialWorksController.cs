using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FabaApp.Web.Data;
using FabaApp.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FabaApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SocialWorksController : Controller
    {
        private readonly DataContext _context;

        public SocialWorksController(DataContext context)
        {
            _context = context;
        }

        // GET: SocialWorks
        public async Task<IActionResult> Index()
        {
            return View(await _context.SocialWorks.ToListAsync());
        }

        // GET: SocialWorks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialWorkEntity = await _context.SocialWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialWorkEntity == null)
            {
                return NotFound();
            }

            return View(socialWorkEntity);
        }

        // GET: SocialWorks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SocialWorks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialWorkEntity socialWorkEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialWorkEntity);
                try
                {
                    socialWorkEntity.DischargeDate = DateTime.Now;
                    //socialWorkEntity.User
                    socialWorkEntity.Active = true;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Esta Obra Social ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(socialWorkEntity);
        }

        // GET: SocialWorks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialWorkEntity = await _context.SocialWorks.FindAsync(id);
            if (socialWorkEntity == null)
            {
                return NotFound();
            }
            return View(socialWorkEntity);
        }

        // POST: SocialWorks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialWorkEntity socialWorkEntity)
        {
            if (id != socialWorkEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    socialWorkEntity.DischargeDate = DateTime.Now;
                    //socialWorkEntity.User
                    _context.Update(socialWorkEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Esta Obra Social ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(socialWorkEntity);
        }

        // GET: SocialWorks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var socialWorkEntity = await _context.SocialWorks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialWorkEntity == null)
            {
                return NotFound();
            }

            _context.SocialWorks.Remove(socialWorkEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}