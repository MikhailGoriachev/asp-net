using EfAspCoreMvciltersPaginations.Models.ViewModels;
using EntityFrameworkInAspNetCoreMvcIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EfAspCoreMvciltersPaginations.Controllers;

public class UsersController : Controller
{
    // контекст базы данных
    private UsersContext _db;

    // В конструкторе получаем доступ к контексту базы данных
    public UsersController(UsersContext context) {
        _db = context;
    } // HomeController


    // Вывод списка пользователей
    public async Task<IActionResult> Index() =>
        View(await _db.Users.AsNoTracking().ToListAsync());


    // Создание записи пользователя
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(User user) {
        user.CompanyId = 1;   // чисто временное решение, для скорости изложения
        _db.Users.Add(user);

        await _db.SaveChangesAsync();
        return RedirectToAction("Index");
    } // Create


    // Удаление записи пользователя
    [HttpPost]
    public async Task<IActionResult> Delete(int? id) {

        // оптимизированный по скорости способ удаления из таблицы БД
        // удаление требует только одногообращения к БД
        // (обычно 2: для поиск и для удаления)
        if (id != null)
        {
            // создать в памяти переременную с требуемым идентфикатором
            User user = new User { Id = id.Value };

            // устанвить признак удаления записи с этим идентификатром
            _db.Entry(user).State = EntityState.Deleted;

            // единственное обращение к БД - собственно для удаления из таблицы
            await _db.SaveChangesAsync();

            // показзать коллекцию из таблицы
            return RedirectToAction("Index");
        } // if

        // запись не найдена - сообщить об этом
        return NotFound();
    } // Delete


    // Редактирование записи пользователя
    // GET /Users/Edit/id - вывод страницы с формой редактирования
    public async Task<IActionResult> Edit(int? id) {
        if (id != null)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user != null)
                return View(user);
        }
        return NotFound();
    } // Edit

    // POST оработчик данных из формы редактирования
    [HttpPost]
    public async Task<IActionResult> Edit(User user) {
        // запись в коллекцию и в талицу
        _db.Users.Update(user);
        await _db.SaveChangesAsync();

        // вывод коллекции из таблицы
        return RedirectToAction("Index");
    } // Edit


    // Реализация сортировки
    public async Task<IActionResult> OrderBy(SortState sortOrder = SortState.NameAsc) {
        IQueryable<User>? users = _db.Users.Include(x => x.Company);

        ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
        ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
        ViewData["CompSort"] = sortOrder == SortState.CompanyAsc ? SortState.CompanyDesc : SortState.CompanyAsc;

        users = sortOrder switch {
            SortState.NameDesc => users.OrderByDescending(s => s.Name),
            SortState.AgeAsc => users.OrderBy(s => s.Age),
            SortState.AgeDesc => users.OrderByDescending(s => s.Age),
            SortState.CompanyAsc => users.OrderBy(s => s.Company!.Name),
            SortState.CompanyDesc => users.OrderByDescending(s => s.Company!.Name),
            _ => users.OrderBy(s => s.Name),
        };
        return View(await users.AsNoTracking().ToListAsync());
    } // OrderBy


    // фильтрация данных пользователя по заданноиу критерию
    // GET /Users/FilterBy
    public ActionResult FilterBy(int? companyId, string? name) {
        // получить коллекцию пользователей из таблицы - все данные, без фильтра
        IQueryable<User> users = _db.Users.Include(p => p.Company);

        // если в фильтре задана компания - отбор по компании
        if (companyId != null && companyId != 0) {
            users = users.Where(p => p.CompanyId == companyId);
        } // if 

        // Если в фильтре задана часть имени пользователя - отбор по этому критерию
        // Обратите внимание, что если был отбор по компании, фильтруем именно этот
        // сокращенный набор данных
        if (!string.IsNullOrEmpty(name)) {
            users = users.Where(p => p.Name!.Contains(name));
        } // if

        // подготовка ViewModel для отображения на странице
        List<Company> companies = _db.Companies.ToList();
        
        // добавляем начальный элемент, который позволит выбрать всех пользователей
        companies.Insert(0, new Company { Name = "Все", Id = 0 });

        UserListViewModel viewModel = new UserListViewModel {
            // передатча инфорамции о пользователях
            Users = users.ToList(),

            // передача параметров в форму
            Name = name,
            Companies = new SelectList(companies, "Id", "Name", companyId),
        };

        // запуск рендеринга представления
        return View(viewModel);
    } // FilterBy


    // реализация постраничного вывода - пагинация
    public async Task<IActionResult> PageBy(int page = 1) {
        int pageSize = 3;   // количество элементов на странице

        IQueryable<User> source = _db.Users.Include(x => x.Company);
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        PageByViewModel viewModel = new PageByViewModel(items, pageViewModel);
        return View(viewModel);
    } // PageBy

} // class HomeController

