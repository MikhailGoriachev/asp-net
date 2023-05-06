using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Optional.Controllers;

public class ProductsController : Controller
{
    // !!! единственный объект для всех пользователей приложения
    // !!! проблемы синхронизации при изменении
    private static Dictionary<int, string> _products = new Dictionary<int, string>();

    public ProductsController() {
        // Конструктор будет срабатывать при каждом обращении к контролеру.
        if (_products.Count == 0) {
            _products[1] = "Чашка чайная";
            _products[2] = "Бутылка с дозатором";
            _products[3] = "Ложка чайная";
            _products[4] = "Нож столовый";
            _products[5] = "Пила цепочечная";
            _products[6] = "Котелок алюминиевый";
        } // if
    }

    // Так как key является опциональным параметром используем nullable тип.
    // Если в запросе не будет предоставлено значение для параметра key
    // метод получит значение null
    public IActionResult Details(int? key) {
        if (key == null) {
            // сформируем случайный ключ,если параметр не передан 
            key = new Random().Next(1, _products.Count + 1);

            // Так как модель является значением строкового типа мы
            // явно приводим ее к типу object
            // Метод View имеет перегрузку принимающую тип данных string,
            // которая позволяет вернуть представление с определенным именем.
            // Для того, чтобы передать представлению модель,
            // нужно использовать перегрузку с параметром object
            return View(_products[(int)key] as object);
        } // if

        if (_products.ContainsKey((int)key)) {
            return View(_products[(int)key] as object);
        } // if
        return NotFound();
    } // Details
}
