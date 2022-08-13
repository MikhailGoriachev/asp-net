using HomeWork.Infrastructure;
using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Pages;

public class ShipList : PageModel
{
    // хранилище коллекции кораблей
    public Models.ShipList ShipListData { get; set; } = new Models.ShipList();

    // текущая кооллекция для отображения
    public List<Ship>? CurrentShips { get; set; }

    // список типов кораблей
    public SelectList TypesShip { get; } = new SelectList(Utils.ShipTypes);

    // название обработки
    public string NameHandler { get; set; } = "Исходная последовательность";

    // карабль для добавления
    [BindProperty]
    public Ship? ShipItem { get; set; }

    // файл изображеия
    public IFormFile? FileImage { get; set; }

    // название папки для сохранение файла изображения
    [BindProperty]
    public string DirectoryImages => "wwwroot/images/ships";

    // ссылка на текущее окружение
    public IHostEnvironment Environment { get; private set; }


    #region Конструкторы

    // констркутор инициализирующий
    public ShipList(IHostEnvironment environment)
    {
        Environment = environment;
    }

    #endregion


    #region Методы

    // исходная последовательность
    public void OnGet()
    {
        CurrentShips = ShipListData.Ships;
    }


    // упорядочивание по возрастанию года изготовления
    public void OnGetOrderByAscYear()
    {
        NameHandler = "Упорядочивание по возрастанию года изготовления";
        CurrentShips = ShipListData.OrderByAscYear();
    }


    // упорядочивание по убыванию водоизмещения
    public void OnGetOrderByDescDisplacement()
    {
        NameHandler = "Упорядочивание по убыванию водоизмещения";
        CurrentShips = ShipListData.OrderByDescDisplacement();
    }


    // упорядочивание по названиям кораблей
    public void OnGetOrderByName()
    {
        NameHandler = "Упорядочивание по названиям кораблей";
        CurrentShips = ShipListData.OrderByName();
    }

    // добавление корабля
    public void OnPostAddShip()
    {
        // загрузка файла на сервер
        Utils.SaveFile(Path.Combine(Environment.ContentRootPath, DirectoryImages), FileImage!);

        // установка файла изображения
        ShipItem!.ImageFile = FileImage!.FileName;

        // добавление прибора в начало списка
        ShipListData.Ships.Insert(0, ShipItem);

        // сохранение данных
        ShipListData.Serialization();

        // установка списка
        CurrentShips = ShipListData.Ships;
    }

    #endregion

}
