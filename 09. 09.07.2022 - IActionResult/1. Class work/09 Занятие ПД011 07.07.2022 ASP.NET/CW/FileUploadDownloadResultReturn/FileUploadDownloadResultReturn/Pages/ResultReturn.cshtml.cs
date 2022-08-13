using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FileUploadDownloadResultReturn.Pages
{
    public class ResultReturnModel : PageModel
    {
        // по умолчанию ничего не возвращаем
        public void OnGet()
        {
            ViewData["Message"] = "ќтработал GET";
        }
        

        // возвращение ContentResult - текст
        public IActionResult OnGetContent() {
            return Content($"—ейчас на сервере: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        }

        // возврат текущей страницы - то же,что и void OnGet()
        public IActionResult OnGetPage()
        {
            ViewData["Message"] = "ѕривет от Page()";
            return Page();
        }

        // переадресаци€ на другую страницу
        public IActionResult OnGetRedirect()
        {
            // переадресаци€ на /TestPage/John/23000
            return RedirectToPage("/TestPage", new {name="John", salary=23_000});
        }

        // возврат статусного кода запроса - StatusCodeResult
        public IActionResult? OnGetStatusCode()
        {
            // return NotFound();
            // return NotFound(new {username="john"});
            return StatusCode(503);
        }

    }
}
