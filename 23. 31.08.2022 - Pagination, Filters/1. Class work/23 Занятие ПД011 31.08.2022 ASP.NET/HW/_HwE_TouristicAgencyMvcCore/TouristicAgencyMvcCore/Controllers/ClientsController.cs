using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TouristicAgencyMvcCore.Models;

namespace TouristicAgencyMvcCore.Controllers;

public class ClientsController : Controller
{
    private readonly ApplicationContext _context;

    public ClientsController(ApplicationContext context) {
        _context = context;
    }

    // GET: Clients
    public async Task<IActionResult> Index() {
        return _context.Clients != null ?
                    View(await _context.Clients.ToListAsync()) :
                    Problem("Entity set 'ApplicationContext.Clients'  is null.");
    }


    // GET: Clients/Create
    public IActionResult Create() => View();
    
    // POST: Clients/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Surname,Name,Patronymic,Passport")] Client client) {
        if (ModelState.IsValid) {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(client);
    }

    // GET: Clients/Edit/5
    public async Task<IActionResult> Edit(int? id) {
        if (id == null || _context.Clients == null)
        {
            return NotFound();
        }

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return NotFound();
        }
        return View(client);
    }

    // POST: Clients/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Patronymic,Passport")] Client client) {
        if (id != client.Id) {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(client.Id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(client);
    }

    private bool ClientExists(int id) {
        return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}

