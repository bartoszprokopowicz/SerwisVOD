using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VOD.Models;
using VOD.Data;
using Microsoft.AspNetCore.Authorization;

namespace VOD.Controllers
{
    [Authorize(Roles = "Admin,Pracownik")]
    public class AktorzyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AktorzyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aktorzy
        public async Task<IActionResult> Index()
        {
            var projektdb2Context = _context.Aktorzy.Include(a => a.Daneosobowe);
            return View(await projektdb2Context.ToListAsync());
        }

        // GET: Aktorzy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktorzy = await _context.Aktorzy
                .Include(a => a.Daneosobowe)
                .FirstOrDefaultAsync(m => m.AktorId == id);
            
            if (aktorzy == null)
            {
                return NotFound();
            }

            return View(aktorzy);
        }

        // GET: Aktorzy/Create
        public IActionResult Create()
        {
            var aktor = new Aktorzy();

            return View(aktor);
        }

        // POST: Aktorzy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aktorzy aktorzy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aktorzy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DaneosoboweId"] = new SelectList(_context.Daneosobowe, "DaneosoboweId", "Imie", aktorzy.DaneosoboweId);
            return View(aktorzy);
        }

        // GET: Aktorzy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktorzy = await _context.Aktorzy
                .Include(a => a.Daneosobowe)
                .FirstOrDefaultAsync(m => m.AktorId == id);

            if (aktorzy == null)
            {
                return NotFound();
            }

            return View(aktorzy);
        }

        // POST: Aktorzy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Aktorzy aktorzy)
        {
            if (aktorzy == null)
            {
                return NotFound();
            }
   

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aktorzy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AktorzyExists(aktorzy.AktorId))
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
            ViewData["DaneosoboweId"] = new SelectList(_context.Daneosobowe, "DaneosoboweId", "Imie", aktorzy.DaneosoboweId);
            return View(aktorzy);
        }

        // GET: Aktorzy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aktorzy = await _context.Aktorzy
                .Include(a => a.Daneosobowe)
                .FirstOrDefaultAsync(m => m.AktorId == id);

            if (aktorzy == null)
            {
                return NotFound();
            }

            return View(aktorzy);
        }

        // POST: Aktorzy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aktorzy = await _context.Aktorzy.FindAsync(id);

            _context.Aktorzy.Remove(aktorzy);

            var AktorzyFilmy = await _context.Aktorzy
                .Where(r => r.AktorId == id)
                .SelectMany(rf => rf.AktorzyFilmy)
                .Where(rf => rf.AktorId == id)
                .ToListAsync();

            _context.AktorzyFilmy.RemoveRange(AktorzyFilmy);

            var dane = await _context.Daneosobowe
                .FindAsync(aktorzy.DaneosoboweId);

            _context.Daneosobowe.Remove(dane);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AktorzyExists(int id)
        {
            return _context.Aktorzy.Any(e => e.AktorId == id);
        }
    }
}
