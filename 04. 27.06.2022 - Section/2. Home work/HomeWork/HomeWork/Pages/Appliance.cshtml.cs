using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    [IgnoreAntiforgeryToken]
    public class ApplianceModel : PageModel
    {
        // объект бытовой техники
        [BindProperty]
        public Appliance? ApplianceItem { get; set; }

        // словарь названий файлов
        public Dictionary<string, string> FileNames { get; } = new()
        {
            ["пылесос"] = "cleaner_001.jpg",
            ["холодильник"] = "fridge_001.jpg",
            ["мультиварка"] = "multicooker_001.jpg",
        };

        // обработчик запроса GET
        public void OnGet()
        {

        }

        // обработчик запроса POST
        public void OnPost()
        {
            if (ApplianceItem == null)
                return;

            ApplianceItem.FileName = FileNames[ApplianceItem.Type];
        }
    }
}
