using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using TouristicAgencyMvcCore.Models;

namespace TouristicAgencyMvcCore.Controllers;

// Запросы по заданию на выборку из таблиц базы данных
public class QueriesController : Controller
{
    // ссылка на базу данных
    private ApplicationContext _context;


    // получение доступа к БД при помощи конструктора
    // с внедрением зависимости
    public QueriesController(ApplicationContext db) {
        _context = db;
    } // QueriesController


    // Запрос 1. Запрос на выборку
    // Выбирает информацию о маршрутах с заданной целью поездки
    public IActionResult Query01(string purpose = "обучение") {
        ViewBag.Task = new HtmlString($"Выбирает информацию о маршрутах с целью поездки <u>{purpose}</u>");
        return View();
    } // Query01


    // Запрос 2. Запрос на выборку
    // Выбирает информацию о маршрутах с заданной целью поездки и стоимостью
    // транспортных услуг менее заданного значения
    public IActionResult Query02(string purpose, int price) {
        ViewBag.Task = new HtmlString(
            $"Выбирает информацию о маршрутах с целью поездки <u>{purpose}</u> " +
            $"и стоимостью транспортных услуг менее <u>{price:n2}</u>");
        return View();
    } // Query02


    // Запрос 3. Запрос на выборку
    // Выбирает информацию о клиентах, совершивших поездки с заданным
    // количеством дней пребывания в стране
    public IActionResult Query03(int duration) {
        ViewBag.Task = new HtmlString(
            "Выбирает информацию о  клиентах, совершивших поездки с количеством " +
            $"дней пребывания <u>{duration}</u>");
        return View();
    } // Query03


    // Запрос 4. Запрос на выборку
    // Выбирает информацию о маршрутах в заданную страну
    public IActionResult Query04(string country) {
        ViewBag.Task = new HtmlString(
            $"Выбирает информацию о маршрутах в странцу <u>{country}</u>");
        return View();
    } // Query04


    // Запрос 5. Запрос на выборку
    // Выбирает информацию о странах, для которых стоимость оформления визы есть
    // значение из некоторого диапазона
    public IActionResult Query05(int lo, int hi) {
        ViewBag.Task = new HtmlString(
            $"Выбирает информацию о странах со стоимостью формления визы от <u>{lo:n2}</u> " +
            $"до <u>{hi:n2}</u>"
        );
        return View();
    } // Query05


    // Запрос 6. Запрос на выборку
    // Вычисляет для каждой поездки ее полную стоимость с НДС. Включает поля
    // Страна назначения, Цель поездки, Дата начала поездки, Количество дней
    // пребывания, Полная стоимость поездки.
    // Сортировка по полю Страна назначения
    public IActionResult Query06() {
        ViewBag.Task = new HtmlString(
            "Вычисляет для каждой поездки ее полную стоимость с НДС. " +
            "Сортировка по полю <u>Страна назначения</u>."
        );
        return View();
    } // Query06
} // class QueriesController

