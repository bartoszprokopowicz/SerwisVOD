using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VOD.Models;
using VOD.ViewModels.FilmyViewModels;
using VOD.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace VODProjekt.Controllers
{
    [Authorize(Roles = "Admin,Pracownik,Uzytkownik")]
    public class FilmyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Uzytkownicy> _userManager;

        public FilmyController(ApplicationDbContext context, UserManager<Uzytkownicy> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Filmy
        public async Task<IActionResult> Index(string byGenre)
        {
            var model = new FilmyInfo();

            model.filmy = _context.Filmy
                .Include(f => f.Gatunek)
                .Include(f => f.Pracownik)
                .Include(f => f.OcenaFilm)
                .ToList();

            IQueryable<string> gatunkiQuery = from m in _context.Gatunki
                                              select m.Gatunek;

            model.gatunki = new SelectList(await gatunkiQuery.Distinct().ToListAsync());

            if(!String.IsNullOrEmpty(byGenre))
            {
                model.filmy = model.filmy.Where(f => f.Gatunek.Gatunek == byGenre);
            }
            return View(model);
        }

        // GET: Filmy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmy = await _context.Filmy
                .Include(f => f.Gatunek)
                .Include(f => f.Pracownik)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (filmy == null)
            {
                return NotFound();
            }

            filmy.KrajeFilmy = _context.Filmy
                .Where(f => f.FilmId == id)
                .SelectMany(fk => fk.KrajeFilmy)
                .Where(fk => fk.FilmId == id)
                .Include(fk => fk.Kraj)
                .ToList();

            filmy.RezyserzyFilmy = _context.Filmy
                .Where(f => f.FilmId == id)
                .SelectMany(rf => rf.RezyserzyFilmy)
                .Where(rf => rf.FilmId == id)
                .Include(rf => rf.Rezyser)
                .Include(rf => rf.Rezyser.Daneosobowe)
                .ToList();

            filmy.AktorzyFilmy = _context.Filmy
                .Where(f => f.FilmId == id)
                .SelectMany(rf => rf.AktorzyFilmy)
                .Where(rf => rf.FilmId == id)
                .Include(rf => rf.Aktor)
                .Include(rf => rf.Aktor.Daneosobowe)
                .ToList();

            return View(filmy);
        }

        [Authorize(Roles = "Admin,Pracownik")]
        // GET: Filmy/Create
        public IActionResult Create()
        {
            var model = new FilmyEdit();


            var rezyserzy = from m in _context.Rezyserzy
                            select new pelneImie
                            {
                                Id = m.RezyserId,
                                Imie = m.Daneosobowe.Imie,
                                Nazwisko = m.Daneosobowe.Nazwisko
                            };

            var aktorzy = from m in _context.Aktorzy
                          select new pelneImie
                          {
                              Id = m.AktorId,
                              Imie = m.Daneosobowe.Imie,
                              Nazwisko = m.Daneosobowe.Nazwisko
                          };

            var kraje = from m in _context.Kraje
                        select new Kraje
                        {
                            KrajId = m.KrajId,
                            KrajNazwa = m.KrajNazwa
                        };

            model.rezyserzy = new SelectList(rezyserzy, "Id", "PelneImie");
            model.aktorzy = new SelectList(aktorzy, "Id", "PelneImie");
            model.kraje = new SelectList(kraje, "KrajId", "KrajNazwa");

            ViewData["GatunekId"] = new SelectList(_context.Gatunki, "GatunekId", "Gatunek");

            return View(model);
        }

        // POST: Filmy/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Pracownik")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmyEdit model)
        {
            if (ModelState.IsValid)
            {
                model.filmy.PracownikId = System.Convert.ToInt16(_userManager.GetUserId(HttpContext.User));

                _context.Add(model.filmy);

                foreach (var it in model.wybranyRezyser)
                {
                    var RezyserzyFilmy = new RezyserzyFilmy();
                    RezyserzyFilmy.FilmId = model.filmy.FilmId;
                    RezyserzyFilmy.RezyserId = System.Convert.ToInt32(it);
                    _context.RezyserzyFilmy.Add(RezyserzyFilmy);
                }

                foreach (var it in model.wybranyAktor)
                {
                    var AktorzyFilmy = new AktorzyFilmy();
                    AktorzyFilmy.FilmId = model.filmy.FilmId;
                    AktorzyFilmy.AktorId = System.Convert.ToInt32(it);
                    _context.AktorzyFilmy.Add(AktorzyFilmy);
                }

                foreach (var it in model.wybranyKraj)
                {
                    var KrajeFilmy = new KrajeFilmy();
                    KrajeFilmy.FilmId = model.filmy.FilmId;
                    KrajeFilmy.KrajId = System.Convert.ToInt32(it);
                    _context.KrajeFilmy.Add(KrajeFilmy);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GatunekId"] = new SelectList(_context.Gatunki, "GatunekId", "Gatunek", model.filmy.GatunekId);

            return View(model);
        }

        // GET: Filmy/Edit/5
        [Authorize(Roles = "Admin,Pracownik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = new FilmyEdit();

            model.filmy = await _context.Filmy
                .Include(f => f.Gatunek)
                .Include(f => f.Pracownik)
                .Include(rf => rf.RezyserzyFilmy)
                .Include(af => af.AktorzyFilmy)
                .Include(kf => kf.KrajeFilmy)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (model.filmy == null)
            {
                return NotFound();
            }
            
            var rezyserzy = from m in _context.Rezyserzy
                      select new pelneImie
                      {
                          Id = m.RezyserId,
                          Imie = m.Daneosobowe.Imie,
                          Nazwisko = m.Daneosobowe.Nazwisko
                      };

            var aktorzy = from m in _context.Aktorzy
                      select new pelneImie
                      {
                          Id = m.AktorId,
                          Imie = m.Daneosobowe.Imie,
                          Nazwisko = m.Daneosobowe.Nazwisko
                      };

            var kraje = from m in _context.Kraje
                        select new Kraje
                        {
                            KrajId = m.KrajId,
                            KrajNazwa = m.KrajNazwa
                        };

            model.rezyserzy = new SelectList(rezyserzy, "Id", "PelneImie", model.filmy.RezyserzyFilmy);
            foreach (var it in model.rezyserzy)
            {
                foreach (var i in model.filmy.RezyserzyFilmy.ToList())
                    if (it.Value.Equals(i.RezyserId.ToString()))
                        it.Selected = true;
            }
            model.aktorzy = new SelectList(aktorzy, "Id", "PelneImie", model.filmy.AktorzyFilmy);
            foreach (var it in model.aktorzy)
            {
                foreach (var i in model.filmy.AktorzyFilmy.ToList())
                    if (it.Value.Equals(i.AktorId.ToString()))
                        it.Selected = true;
            }
            model.kraje = new SelectList(kraje, "KrajId", "KrajNazwa", model.filmy.KrajeFilmy);
            foreach (var it in model.kraje)
            {
                foreach (var i in model.filmy.KrajeFilmy.ToList())
                    if (it.Value.Equals(i.KrajId.ToString()))
                        it.Selected = true;
            }
            ViewData["GatunekId"] = new SelectList(_context.Gatunki, "GatunekId", "Gatunek", model.filmy.GatunekId);

            return View(model);
        }

        // POST: Filmy/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Pracownik")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FilmyEdit model)
        {
            if (model == null)
            {
                return NotFound();
            }

            model.filmy.PracownikId = System.Convert.ToInt16(_userManager.GetUserId(HttpContext.User));

            model.filmy.KrajeFilmy = _context.Filmy
                .Where(f => f.FilmId == model.filmy.FilmId)
                .SelectMany(fk => fk.KrajeFilmy)
                .Where(fk => fk.FilmId == model.filmy.FilmId)
                .Include(fk => fk.Kraj)
                .ToList();

            model.filmy.RezyserzyFilmy = _context.Filmy
                .Where(f => f.FilmId == model.filmy.FilmId)
                .SelectMany(rf => rf.RezyserzyFilmy)
                .Where(rf => rf.FilmId == model.filmy.FilmId)
                .ToList();

            model.filmy.AktorzyFilmy = _context.Filmy
                .Where(f => f.FilmId == model.filmy.FilmId)
                .SelectMany(rf => rf.AktorzyFilmy)
                .Where(rf => rf.FilmId == model.filmy.FilmId)
                .ToList();
 
            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(model.filmy);

                    _context.RemoveRange(model.filmy.RezyserzyFilmy);
                    _context.RemoveRange(model.filmy.AktorzyFilmy);
                    _context.RemoveRange(model.filmy.KrajeFilmy);

                    foreach (var it in model.wybranyRezyser)
                    {
                        var RezyserzyFilmy = new RezyserzyFilmy();
                        RezyserzyFilmy.FilmId = model.filmy.FilmId;
                        RezyserzyFilmy.RezyserId = System.Convert.ToInt32(it);
                        _context.RezyserzyFilmy.Add(RezyserzyFilmy);
                    }

                    foreach (var it in model.wybranyAktor)
                    {
                        var AktorzyFilmy = new AktorzyFilmy();
                        AktorzyFilmy.FilmId = model.filmy.FilmId;
                        AktorzyFilmy.AktorId = System.Convert.ToInt32(it);
                        _context.AktorzyFilmy.Add(AktorzyFilmy);
                    }

                    foreach (var it in model.wybranyKraj)
                    {
                        var KrajeFilmy = new KrajeFilmy();
                        KrajeFilmy.FilmId = model.filmy.FilmId;
                        KrajeFilmy.KrajId = System.Convert.ToInt32(it);
                        _context.KrajeFilmy.Add(KrajeFilmy);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmyExists(model.filmy.FilmId))
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
            ViewData["GatunekId"] = new SelectList(_context.Gatunki, "GatunekId", "Gatunek", model.filmy.GatunekId);

            return View(model);
        }

        [Authorize(Roles = "Admin,Pracownik")]
        // GET: Filmy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmy = await _context.Filmy
                .Include(f => f.Gatunek)
                .Include(f => f.Pracownik)
                .FirstOrDefaultAsync(m => m.FilmId == id);
            if (filmy == null)
            {
                return NotFound();
            }

            return View(filmy);
        }

        // POST: Filmy/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Pracownik")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var filmy = await _context.Filmy
                .Include(rf => rf.RezyserzyFilmy)
                .Include(af => af.AktorzyFilmy)
                .Include(kf => kf.KrajeFilmy)
                .Include(of => of.OcenaFilm)
                .Include(fu => fu.UzytkownicyFilmy)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            _context.RezyserzyFilmy.RemoveRange(filmy.RezyserzyFilmy);
            _context.AktorzyFilmy.RemoveRange(filmy.AktorzyFilmy);
            _context.KrajeFilmy.RemoveRange(filmy.KrajeFilmy);
            _context.OcenaFilm.RemoveRange(filmy.OcenaFilm);
            _context.UzytkownicyFilmy.RemoveRange(filmy.UzytkownicyFilmy);

            _context.Filmy.Remove(filmy);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Pracownik,Uzytkownik")]
        // GET: Filmy/Wykup/5
        public async Task<IActionResult> Wykup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filmy = await _context.Filmy
                .FirstOrDefaultAsync(m => m.FilmId == id);

            if (filmy == null)
            {
                return NotFound();
            }

            return View(filmy);
        }

        [HttpPost, ActionName("Wykup")]
        [Authorize(Roles = "Admin,Pracownik,Uzytkownik")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Wykup(int id)
        {
            var filmy = await _context.Filmy
                .Include(rf => rf.RezyserzyFilmy)
                .Include(af => af.AktorzyFilmy)
                .Include(kf => kf.KrajeFilmy)
                .Include(of => of.OcenaFilm)
                .Include(fu => fu.UzytkownicyFilmy)
                .FirstOrDefaultAsync(m => m.FilmId == id);

            var UzytkownicyFilmy = new UzytkownicyFilmy();
            UzytkownicyFilmy.FilmId = id;
            UzytkownicyFilmy.UzytkownikId = System.Convert.ToInt16(_userManager.GetUserId(HttpContext.User));

            _context.UzytkownicyFilmy.Add(UzytkownicyFilmy);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool FilmyExists(int id)
        {
            return _context.Filmy.Any(e => e.FilmId == id);
        }
    }
}
