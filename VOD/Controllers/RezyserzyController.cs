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

namespace VODProjekt.Controllers
{
    [Authorize(Roles = "Admin,Pracownik")]
    public class RezyserzyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RezyserzyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rezyserzy
        public async Task<IActionResult> Index()
        {
            var projektdb2Context = _context.Rezyserzy.Include(r => r.Daneosobowe);
            return View(await projektdb2Context.ToListAsync());
        }

        // GET: Rezyserzy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezyserzy = await _context.Rezyserzy
                .Include(r => r.Daneosobowe)
                .FirstOrDefaultAsync(m => m.RezyserId == id);
            if (rezyserzy == null)
            {
                return NotFound();
            }

            return View(rezyserzy);
        }

        // GET: Rezyserzy/Create
        public IActionResult Create()
        {
            var rezyser = new Rezyserzy();
            return View(rezyser);
        }

        // POST: Rezyserzy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rezyserzy rezyserzy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezyserzy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DaneosoboweId"] = new SelectList(_context.Daneosobowe, "DaneosoboweId", "Imie", rezyserzy.DaneosoboweId);
            return View(rezyserzy);
        }

        // GET: Rezyserzy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezyserzy = await _context.Rezyserzy
                .Include(a => a.Daneosobowe)
                .FirstOrDefaultAsync(m => m.RezyserId == id);

            if (rezyserzy == null)
            {
                return NotFound();
            }

            return View(rezyserzy);
        }

        // POST: Rezyserzy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Rezyserzy rezyserzy)
        {
            if (rezyserzy == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezyserzy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezyserzyExists(rezyserzy.RezyserId))
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
            ViewData["DaneosoboweId"] = new SelectList(_context.Daneosobowe, "DaneosoboweId", "Imie", rezyserzy.DaneosoboweId);
            return View(rezyserzy);
        }

        // GET: Rezyserzy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezyserzy = await _context.Rezyserzy
                .Include(r => r.Daneosobowe)
                .FirstOrDefaultAsync(m => m.RezyserId == id);

            if (rezyserzy == null)
            {
                return NotFound();
            }

            return View(rezyserzy);
        }

        // POST: Rezyserzy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezyserzy = await _context.Rezyserzy.FindAsync(id);
            _context.Rezyserzy.Remove(rezyserzy);

            var RezyserzyFilmy = await _context.Rezyserzy
                .Where(r => r.RezyserId == id)
                .SelectMany(rf => rf.RezyserzyFilmy)
                .Where(rf => rf.RezyserId == id)
                .ToListAsync();

            _context.RezyserzyFilmy.RemoveRange(RezyserzyFilmy);

            var dane = await _context.Daneosobowe
                .FindAsync(rezyserzy.DaneosoboweId);

            _context.Daneosobowe.Remove(dane);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezyserzyExists(int id)
        {
            return _context.Rezyserzy.Any(e => e.RezyserId == id);
        }
    }
}
