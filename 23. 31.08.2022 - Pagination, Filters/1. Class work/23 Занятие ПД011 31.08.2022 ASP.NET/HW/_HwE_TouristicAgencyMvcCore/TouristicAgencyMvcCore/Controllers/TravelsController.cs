using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkInAspNetCoreMvcIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TouristicAgencyMvcCore.Models;

namespace TouristicAgencyMvcCore.Controllers;

public class TravelsController : Controller
{
    private readonly ApplicationContext _context;

    public TravelsController(ApplicationContext context) {
        _context = context;
    }

    // GET: Travels
    public async Task<IActionResult> Index() {
        var applicationContext = _context
            .Travels.Include(t => t.Client)
            .Include(t => t.Route);
        return View(await applicationContext.ToListAsync());
    }


    // GET: Travels/Create
    public IActionResult Create() {
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
        ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id");
        return View();
    }

    // POST: Travels/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,ClientId,RouteId,Start,Duration")] Travel travel) {
        if (ModelState.IsValid)
        {
            _context.Add(travel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", travel.ClientId);
        ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id", travel.RouteId);
        return View(travel);
    }

    // GET: Travels/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Travels == null)
        {
            return NotFound();
        }

        var travel = await _context.Travels.FindAsync(id);
        if (travel == null)
        {
            return NotFound();
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", travel.ClientId);
        ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id", travel.RouteId);
        return View(travel);
    }

    // POST: Travels/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,RouteId,Start,Duration")] Travel travel) {
        if (id != travel.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(travel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelExists(travel.Id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", travel.ClientId);
        ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Id", travel.RouteId);
        return View(travel);
    }

    // GET: Travels/Delete/5
    public async Task<IActionResult> Delete(int? id) {
        if (id == null || _context.Travels == null)
        {
            return NotFound();
        }

        var travel = await _context.Travels
            .Include(t => t.Client)
            .Include(t => t.Route)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (travel == null)
        {
            return NotFound();
        }

        return View(travel);
    }

    // POST: Travels/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        if (_context.Travels == null)
        {
            return Problem("Entity set 'ApplicationContext.Travels'  is null.");
        }
        var travel = await _context.Travels.FindAsync(id);
        if (travel != null)
        {
            _context.Travels.Remove(travel);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TravelExists(int id) {
        return (_context.Travels?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    // сортировка по заданию
    public async Task<IActionResult> OrderBy(SortState sortOrder = SortState.StartAsc) {
        IQueryable<Travel>? travels = _context.Travels
            .Include(x => x.Client)
            .Include(x => x.Route)
            .ThenInclude(r => r.Country);

        ViewBag.StartSort = sortOrder == SortState.StartAsc ? SortState.StartDesc : SortState.StartAsc;
        ViewBag.DurationSort = sortOrder == SortState.DurationAsc ? SortState.DurationDesc : SortState.DurationAsc;
        ViewBag.CountrySort = sortOrder == SortState.CountryAsc ? SortState.CountryDesc : SortState.CountryAsc;

        travels = sortOrder switch {
            SortState.StartDesc => travels.OrderByDescending(s => s.Start),
            SortState.DurationAsc => travels.OrderBy(s => s.Duration),
            SortState.DurationDesc => travels.OrderByDescending(s => s.Duration),
            SortState.CountryAsc =>   travels.OrderBy(s => s.Route.Country.Name),
            SortState.CountryDesc =>  travels.OrderByDescending(s => s.Route.Country.Name),
            _ => travels.OrderBy(s => s.Id),
        };
        return View("Index", await travels.AsNoTracking().ToListAsync());
    }
}

