using EfAspCoreMvciltersPaginations.Models.ViewModels;
using EntityFrameworkInAspNetCoreMvcIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EfAspCoreMvciltersPaginations.Controllers;
public class CompaniesController : Controller
{
    private UsersContext _db;

    // В конструкторе получаем доступ к контекксту базы данных
    public CompaniesController(UsersContext context) {
        _db = context;
    } // CompaniesController

    // Вывод списка компаний постранично
    // /Companies/Index
    public async Task<IActionResult> Index(int page = 1) {
        int pageSize = 5;   // количество элементов на странице

        IQueryable<Company> source = _db.Companies;
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        CompanyIndexViewModel viewModel = new (items, pageViewModel);
        return View(viewModel);
    } // Index
}

