using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    // ввод и вывод словаря
    public class Post06Model : PageModel
    {
        // сообщение, выводимое в заголовке формы
        public string Header { get; private set; } = "";

        // словарь данных для ввода и вывода
        public Dictionary<string, string> Countries { get; set; } = new();


        // при запросе страницы просто вывоем текущую дату и время в заголовок формы
        // конечно, можно вообще ничего не предпринимать
        public void OnGet() {
            Header = $"{DateTime.Now:G}";
        } // OnGet

        
        // обработка формы по запросу POST
        public void OnPost(Dictionary<string, string> items) {
            Header = $"{DateTime.Now:G}";
            // вывод словаря данных
            Countries = items;
        } // OnPost
    } //  class Post06Model 
}
