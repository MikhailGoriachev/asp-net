using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Homework.Pages;

public class Clients : PageModel
{
    private TravelAgencyContext _dbContext;

    // Коллекция данных для отображения
    public IQueryable<Client>? DisplayClients { get; private set; }
    
    [BindProperty] public Client? Client { get; set; } = new();
    
    // Строка для сообщений об ошибках
    public string? ErrMsg;
    
    public Clients(TravelAgencyContext agencyContext) => _dbContext = agencyContext;

    public void OnGet(string? err)
    {
        if (err is not null)
            ErrMsg = err;

        DisplayClients = _dbContext.Clients.AsNoTracking();
    }
    
    public void OnPost() => DisplayClients = _dbContext.Clients.AsNoTracking();
    
    public async Task<IActionResult> OnPostAddAsync()
    {
        try
        {
            if (_dbContext.Clients.FirstOrDefault(c => c.Passport == Client!.Passport) is not null)
                throw new Exception("Клиент с указанным номером паспорта уже существует в базе");
            
            _dbContext.Clients.Add(Client!);
            await _dbContext.SaveChangesAsync();

            DisplayClients = _dbContext.Clients.AsNoTracking();
            return Page();
        }catch (Exception ex)
        {
            return RedirectToPage(nameof(Travels), new {err = ex.Message});
        }
    }
    
    // Инициация редактирования
    public async Task<IActionResult> OnGetEdit(int id)
    {
        DisplayClients = _dbContext.Clients.AsNoTracking();
        Client = await _dbContext.Clients.FindAsync(id);

        if (Client == null) {
            return NotFound();
        }

        return Page();
    }

    // Обновление отредактированных данных
    public async Task<IActionResult> OnPostEditAsync()
    {
        try
        {
            _dbContext.Clients.Update(Client!);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }catch (Exception ex)
        {
            return RedirectToPage(nameof(Travels), new {err = ex.Message});
        }
    }
}