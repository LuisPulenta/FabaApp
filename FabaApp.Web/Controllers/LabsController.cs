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
    //[Authorize(Roles = "Admin")]

    public class LabsController : Controller
    {
        private readonly DataContext _context;

        public LabsController(DataContext context)
        {
            _context = context;
        }

        // GET: Labs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Labs.ToListAsync());
        }

        // GET: Labs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labEntity = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labEntity == null)
            {
                return NotFound();
            }

            return View(labEntity);
        }

        // GET: Labs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Labs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabEntity labEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labEntity);

                try
                {
                    labEntity.DischargeDate = DateTime.Now;
                    //labEntity.User
                    labEntity.Active = true;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Este Laboratorio ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }
            return View(labEntity);
        }

        // GET: Labs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labEntity = await _context.Labs.FindAsync(id);
            if (labEntity == null)
            {
                return NotFound();
            }
            return View(labEntity);
        }

        // POST: Labs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LabEntity labEntity)
        {
            if (id != labEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(labEntity);
                try
                {
                    labEntity.DischargeDate = DateTime.Now;
                    //labEntity.User
                    _context.Update(labEntity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Este Laboratorio ya existe");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(labEntity);
        }

        // GET: Labs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labEntity = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labEntity == null)
            {
                return NotFound();
            }

            _context.Labs.Remove(labEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}