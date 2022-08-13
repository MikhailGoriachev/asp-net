using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeWork.Pages
{
    public class ApplianceList : PageModel
    {
        // хранилище коллекции бытовых приборов
        public Models.ApplianceList AppList { get; set; } = new Models.ApplianceList();

        // текущая коллекция для отображения
        public List<Appliance>? CurrentAppliances { get; set; }

        // минимальная цена для выборки
        [BindProperty(SupportsGet = true)]
        public int MinPrice { get; set; }

        // максимальная цена для выборки
        [BindProperty(SupportsGet = true)]
        public int MaxPrice { get; set; }

        // прибор для добавления
        [BindProperty]
        public Appliance? ApplianceItem { get; set; }

        // словарь названий файлов
        public static Dictionary<string, string> FileNames { get; } = new()
        {
            ["пылесос"] = "cleaner_001.jpg",
            ["холодильник"] = "fridge_001.jpg",
            ["мультиварка"] = "multicooker_001.jpg",
        };

        // список типов техники
        public SelectList TypesAppliance { get; } = new SelectList(FileNames.Select(f => f.Key));

        // название обработки
        public string NameHandler { get; set; } = "Исходная последовательность";

        #region Методы

        // исходая последовательность
        public void OnGet()
        {
            NameHandler = "Исходная последовательность";
            CurrentAppliances = AppList.Appliances;
        }


        // упорядочивание коллекции по наименованию
        public void OnGetOrderByName()
        {
            NameHandler = "Упорядочивание коллекции по наименованию";
            CurrentAppliances = AppList.OrderByName();
        }


        // упорядочивание коллекции по убыванию цены
        public void OnGetOrderByDescPrice()
        {
            NameHandler = "Упорядочивание коллекции по убыванию цены";
            CurrentAppliances = AppList.OrderByDescPrice();
        }


        // упорядочивание коллекции по возрастанию количества
        public void OnGetOrderByAmount()
        {
            NameHandler = "Упорядочивание коллекции по возрастанию количества";
            CurrentAppliances = AppList.OrderByAmount();
        }


        // выборка коллекции – товары с ценой, попадающей в заданный диапазон
        public void OnGetSelectByPriceRange()
        {
            NameHandler = $"Выборка по диапазону цены ({MinPrice} — {MaxPrice})";
            CurrentAppliances = AppList.SelectByPriceRange(MinPrice, MaxPrice);
        }


        // добавление прибора
        public void OnPostAddAppliance()
        {
            // установка файла по типу прибора
            ApplianceItem.FileName = FileNames[ApplianceItem.Type];

            // добавление прибора в начало списка
            AppList.Appliances.Insert(0, ApplianceItem);

            // сохранение данных
            AppList.Serialization();

            // установка списка
            CurrentAppliances = AppList.Appliances;
        }

        #endregion
    }
}
