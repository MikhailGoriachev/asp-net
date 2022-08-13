using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TouristicAgency.Models;

namespace TouristicAgency.Pages
{
    // работа с таблицей КЛИЕНТЫ
    public class ClientsModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Client>? Clients { get; private set; }

        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public ClientsModel(ApplicationContext db)
        {
            _context = db;
        } // ClientsModel

        public void OnGet()
        {
            Clients = _context.Clients
                .AsNoTracking()
                .ToList();
        }
    }
}
