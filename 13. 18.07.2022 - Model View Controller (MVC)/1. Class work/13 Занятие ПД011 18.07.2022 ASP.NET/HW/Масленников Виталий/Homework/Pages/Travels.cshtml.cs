using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Travels : PageModel
{
    private TravelAgencyContext _dbContext;

    // Коллекция данных для отображения
    public IQueryable<Travel>? DisplayTravels { get; private set; }
    
    // Список стран
    public SelectList CountriesList;
    // Список целей поездки
    public SelectList PurposesList;
    
    [BindProperty] public int SelectedCountryId { get; set; }
    [BindProperty] public int SelectedPurposeId { get; set; }
    [BindProperty] public Travel Travel { get; set; } = new() { StartDate = DateTime.Today, Duration = 1 };
    [BindProperty] public Client Client { get; set; } = new();
    
    // Строка для сообщений об ошибках
    public string? ErrMsg;

    public Travels(TravelAgencyContext agencyContext)
    {
        _dbContext = agencyContext;
        CountriesList = new SelectList(_dbContext.Countries.AsNoTracking(), "Id", "Name");
        PurposesList = new SelectList(_dbContext.Purposes.AsNoTracking(), "Id", "Name");
    }
    
    public void OnGet(string? err)
    {
        if (err is not null)
            ErrMsg = err;
        
        DisplayTravels = LoadTravels();
    }

    public void OnPost() => DisplayTravels = LoadTravels();

    public async Task<IActionResult> OnPostAddAsync()
    {
        try
        {
            AssembleTravel();
            Travel.Id = 0;
            _dbContext.Travels.Add(Travel);
            await _dbContext.SaveChangesAsync();

            DisplayTravels = LoadTravels();
            return Page();
        }catch (Exception ex)
        {
            return RedirectToPage(nameof(Travels), new {err = ex.Message});
        }
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var travel = await _dbContext.Travels.FindAsync(id);

        if (travel != null)
        {
            _dbContext.Travels.Remove(travel);
            await _dbContext.SaveChangesAsync();
        }

        return RedirectToPage();
    }
    
    // Инициация редактирования
    public void OnGetEdit(int id)
    {
        DisplayTravels = LoadTravels();
        Travel = DisplayTravels.FirstOrDefault(travel => travel.Id == id)!;
        SelectedCountryId = Travel.Route!.CountryId;
        SelectedPurposeId = Travel.Route!.PurposeId;
        Client = Travel.Client!;
    }

    // Обновление отредактированных данных
    public async Task<IActionResult> OnPostEditAsync()
    {
        try
        {
            AssembleTravel();
            _dbContext.Travels.Update(Travel);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }catch (Exception ex)
        {
            return RedirectToPage(nameof(Travels), new {err = ex.Message});
        }
    }

    // Собрать объект поездки
    private void AssembleTravel()
    {
        var client = _dbContext.Clients.FirstOrDefault(c => c.Passport == Client.Passport);

        if (client is not null && (client.Surname != Client.Surname
                                   || client.Name != Client.Name
                                   || client.Patronymic != Client.Patronymic))
        {
            throw new Exception(
                "Введенные Ф.И.О. не соответствуют Ф.И.О. клиента с указанным номером паспорта");
        }
        
        client ??= _dbContext.Add(Client).Entity;
        
        /*Client = _dbContext.Clients.FirstOrDefault(c => c.Passport == Client.Passport) 
                 ?? _dbContext.Add(Client).Entity;*/
        
        Travel.Client = client;
        Travel.Route = _dbContext.Routes.FirstOrDefault(r =>
            r.CountryId == SelectedCountryId && r.PurposeId == SelectedPurposeId);
    }
    
    // Выгрузка коллекции поездок с вложенными связями
    private IQueryable<Travel> LoadTravels() => _dbContext.Travels
        .Include(tr => tr.Client)
        .Include(tr => tr.Route)
            .ThenInclude(r => r!.Purpose)
        .Include(tr => tr.Route)
            .ThenInclude(r => r!.Country)
        .AsNoTracking();
}