using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{

    // работа с таблицей КЛИЕНТЫ
    public class ClientsAllModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public List<Client>? Clients { get; private set; }

        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public ClientsAllModel(ApplicationContext db)
        {
            _context = db;
        } //  ClientsAllModel

        // по GET-запросу отдать клиенту разметку с выведенной
        // коллекцией записей из таблицы БД
        public void OnGet()
        {
            Clients = _context.Clients
                .AsNoTracking()
                .ToList();
        } // OnGet
    }
}
