using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    [IgnoreAntiforgeryToken]
    public class ApplianceList : PageModel
    {
        // хранилище коллекции бытовых приборов
        public static Models.ApplianceList AppList { get; set; } = new Models.ApplianceList();

        // текущая коллекция для отображения
        public List<Appliance>? CurrentAppliances { get; set; }

        // минимальная цена для выборки
        [BindProperty(SupportsGet = true)]
        public int MinPrice { get; set; }

        // максимальная цена для выборки
        [BindProperty(SupportsGet = true)]
        public int MaxPrice { get; set; }

        // название обработки
        public string NameHandler { get; set; } = "Исходная последовательность";

        #region Методы

        // исходая последовательность
        public void OnGet()
        {
            NameHandler = "Исходая последовательность";
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

        #endregion
    }
}
