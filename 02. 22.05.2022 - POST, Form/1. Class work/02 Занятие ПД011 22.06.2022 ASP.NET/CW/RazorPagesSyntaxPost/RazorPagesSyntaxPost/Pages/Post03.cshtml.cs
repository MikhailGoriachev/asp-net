using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSyntaxPost.Pages
{
    [IgnoreAntiforgeryToken]
    public class Post03Model : PageModel
    {
        public string[] People { get; private set; } = Array.Empty<string>();

        public void OnGet() {
        }

        // Получение данных из формы, для примера запишем их в модель
        // для вывода на этой же странице
        public void OnPost(string[] people) {
            People = people;
        }
    }
}
