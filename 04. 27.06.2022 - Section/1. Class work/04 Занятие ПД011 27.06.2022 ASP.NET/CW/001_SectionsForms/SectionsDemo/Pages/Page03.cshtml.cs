using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SectionsDemo.Models;


namespace SectionsDemo.Pages
{
    // использование списочного элемента 
    public class Page03Model : PageModel
    {
        // данные дл€ выпадающего списка
        static List<Enterprise> enterprises { get; } = new() {
            new Enterprise(1, "Apple"),
            new Enterprise(2, "Samsung"),
            new Enterprise(3, "Google"),
            new Enterprise(4, "Motorola"),
            new Enterprise(5, "Xiaomi"),
        };

        // создание объекта дл€ выпадающего списка
        // SelectList(коллекци€ƒанныхƒл€¬ывода, »м€—войства«начени€, »м€—войстваќтображени€)
        public SelectList Companies { get; } = new SelectList(enterprises, "Id", "Name");

        [BindProperty]
        public Product Goods { get; set; } = new("", 1000, 0);
        public string Message { get; private set; } = "ƒобавление товара";

        public void OnPost() {
            // получаем производител€ товара - Goods.EnterpriseId прив€зан к выбранному элементу списка
            Enterprise? enterprise = enterprises.FirstOrDefault(e => e.Id == Goods.EnterpriseId);
            Message = $"ƒобавлен новый товар: {Goods.Name} ({enterprise?.Name}) {Goods.Price} руб.";
        } // OnPost

        public record class Product(string Name, int Price, int EnterpriseId);
        
    }
}
