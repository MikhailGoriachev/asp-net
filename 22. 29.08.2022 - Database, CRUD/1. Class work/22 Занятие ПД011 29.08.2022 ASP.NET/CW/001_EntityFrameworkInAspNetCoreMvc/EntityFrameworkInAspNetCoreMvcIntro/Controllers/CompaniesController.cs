using EntityFrameworkInAspNetCoreMvcIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkInAspNetCoreMvcIntro.Controllers;
public class CompaniesController : Controller
{
    private UsersContext _db;

    // В конструкторе получаем доступ к контекксту базы данных
    public CompaniesController(UsersContext context) {
        _db = context;
    } // CompaniesController

    // Вывод списка компаний
    public async Task<IActionResult> Index() {
        return View(await _db.Companies.ToListAsync());
    }
}

