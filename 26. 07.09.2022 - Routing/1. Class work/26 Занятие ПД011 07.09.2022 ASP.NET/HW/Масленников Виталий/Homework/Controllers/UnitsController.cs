using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Homework.Models;
using Homework.Models.ViewModels;

namespace Homework.Controllers
{
    public class UnitsController : Controller
    {
        private readonly WholeSaleContext _context;

        private const int PerPage = 5;
        
        public UnitsController(WholeSaleContext context) => _context = context;

        // GET: Units
        public async Task<IActionResult> Index(int page = 1)
        {
            var items = await _context.Units.AsNoTracking()
                  .Skip((page - 1) * PerPage)
                  .Take(PerPage)
                  .ToListAsync();
            
              return View(new IndexViewModel<Unit>
              {
                  CurrentPage = page,
                  PagesCount = (int)Math.Ceiling((double) await _context.Units.CountAsync() / PerPage),
                  Items = items
              });
        }
        
        // GET: Units/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Short,Long")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Units/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Units == null)
            {
                return NotFound();
            }

            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Short,Long")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitExists(unit.Id))
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
            return View(unit);
        }

        private bool UnitExists(int id)
        {
          return (_context.Units?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
