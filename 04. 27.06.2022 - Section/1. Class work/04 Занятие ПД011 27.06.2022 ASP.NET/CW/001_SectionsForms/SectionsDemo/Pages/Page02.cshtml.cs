using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// уствановим Newtonsoft для записи/чтения в формате JSON
using Newtonsoft.Json;

using SectionsDemo.Models;

namespace SectionsDemo.Pages
{
    // Введение в тег-хелперы
    public class Page02Model : PageModel
    {
        // привязка полей формы к свойству
        [BindProperty]
        public Product Product { get; set; } = new("мяч резиновый", 1230, "Брянск-игрушка");

        public string Message { get; private set; } = "Добавление товара";

        // полное имя файла для записи/чтения
        private string _path = $"{Environment.CurrentDirectory}/App_Data/product.json";


        // обработка GET-запроса
        public void OnGet() {
            // чтение из файла в формате JSON
            if (System.IO.File.Exists(_path)) {
                string json = System.IO.File.ReadAllText(_path);
                Product = JsonConvert.DeserializeObject<Product>(json);
            } // if
        } // OnGet


        // обработчик формы
        public void OnPost() {
            Message = $"Добавлен и сохранен новый товар: {Product.Name} ({Product.Company}), {Product.Price} руб.";

            // запись в JSON-формате
            string json = JsonConvert.SerializeObject(Product);
            System.IO.File.WriteAllText(_path, json);
        } // OnPost
    } // class Page02Model 
}
