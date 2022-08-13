using EmptyProjectRazorPagesLayout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmptyProjectRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        // пример использования свойства модели
        public string Message { get; private set; } = "";

        //public List<Driver> Drivers = new List<Driver>(new[] {
        //    new Driver("Сергей", 34),
        //    new Driver("Оксана", 46),
        //    new Driver("Маргарита", 29),
        //});

        // обработчик Get-запроса 
        public void OnGet() {
            Message = "Текст сформирован в codebehind";
            // throw new InvalidDataException("Проблема....");
        } // OnGet
    }
}
