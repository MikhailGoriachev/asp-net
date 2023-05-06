using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homework.Controllers;

public class QueriesController : Controller
{
    private readonly WholeSaleContext _context;

    public QueriesController(WholeSaleContext context) => _context = context;
    
    public IActionResult Query04() => 
        View(_context.Sellers.AsNoTracking());

    [HttpPost]
    public IActionResult Query04(int interest) => 
        View(_context.Sellers.Where(i => Math.Abs(i.Interest - interest) < 1E-6).AsNoTracking());

    public IActionResult Query05()
    {
        throw new NotImplementedException();
    }

    public IActionResult Query06()
    {
        throw new NotImplementedException();
    }

    public IActionResult Query09()
    {
        throw new NotImplementedException();
    }

    public IActionResult Query10()
    {
        throw new NotImplementedException();
    }
}