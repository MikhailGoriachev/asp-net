using H_WASP_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace H_WASP_NET.Pages
{
    public class FilterQueriesModel : PageModel
    {
        // ссылка на базу данных
        private ApplicationContext _context;

        // ссылка на коллекцию сущностей - таблицу из БД
        public IQueryable<Travel> Travels { get; private set; } = null!;

        // стоимость транспортных услуг
        [BindProperty]
        public string? NamePurpTravel { get; set; }

        // цель поездки
        [BindProperty]
        public int CostTransportServ { get; set; }

        // длительность поездки
        [BindProperty]
        public int Duration { get; set; }

        // страна назначения
        [BindProperty]
        public string NameCountry { get; set; }

        // минимальная стоимость визы
        [BindProperty]
        public int CostVisaMin { get; set; }

        // максимальная стоимость визы
        [BindProperty]
        public int CostVisaMax { get; set; }

        // формулировка запроса
        public string? Task { get; private set; }


        // получение доступа к БД при помощи конструктора
        // с внедрением зависимости
        public FilterQueriesModel(ApplicationContext db)
        {
            _context = db;
        } // FilterQueriesModel

        public void OnGet() { }

        // Выбирает из таблицы МАРШРУТЫ информацию о маршрутах с целью поездки «отдых»
        public void OnPostQuery01()
        {
            Task = $"Выбрана информация о маршрутах с целью поездки {NamePurpTravel}";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Route.PurposeTravel.NamePurpTravel == NamePurpTravel);
        } // OnPostQuery01


        // Выбирает из таблицы МАРШРУТЫ информацию о маршрутах,
        // для которых Цель поездки «лечение» и Стоимость транспортных не превышает 2000 руб.
        public void OnPostQuery02()
        {
            Task = $"Выбрана информация о маршрутах с целью поездки {NamePurpTravel} и стоимость транспортных {CostTransportServ}р.";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Route.PurposeTravel.NamePurpTravel == NamePurpTravel && t.Route.Country.CostTransportServ == CostTransportServ);
        } // OnPostQuery02


        // Выбирает из таблиц КЛИЕНТЫ и ПОЕЗДКИ информацию о клиентах,
        // совершивших поездки с количеством дней пребывания в стране не менее 10
        public void OnPostQuery03(int duration = 10)
        {
            Task = $"Выбрана информация о клиентах, с количеством дней пребывания в стране не менее {duration}";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Duration >= duration);
        } // OnPostQuery03

        // Выбирает из таблицы МАРШРУТЫ информацию о маршрутах в заданную страну.
        // Конкретное название страны вводится при выполнении запроса
        public void OnPostQuery04()
        {
            Task = $"Выбрана информация о маршрутах в заданную страну {NameCountry}";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r.PurposeTravel)
                .AsNoTracking()
                .Where(t => t.Route.Country.NameCountry == NameCountry);
        } // OnPostQuery04

        // Выбирает из таблицы МАРШРУТЫ информацию о странах,
        // для которых стоимость оформления визы есть значение
        // из некоторого диапазона. Нижняя и верхняя границы диапазона
        // задаются при выполнении запроса
        public void OnPostQuery05()
        {
            Task = $"Выбрана информация о станах, стоимость оформления визы от {CostVisaMin}р. до {CostVisaMax}р.";
            Travels = _context.Travels
                .Include(t => t.Client)
                .Include(t => t.Route)
                .ThenInclude(r => r!.Country)
                .Include(t => t.Route)
                .ThenInclude(r => r!.PurposeTravel)
                .AsNoTracking()
                .Where(t => CostVisaMin <= t.Route.Country.CostVisa && t.Route.Country.CostVisa <= CostVisaMax);
        } // OnPostQuery05


    } // class FilterQueriesModel
}
