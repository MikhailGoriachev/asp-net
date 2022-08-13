using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorSyntaxContinue.Pages
{
    /*
     * Самый важный момент при обработки отправляемых форм - класс модели
     * Razor для валидации полученных форм использует специальный токен -
     * AntiforgeryToken.
     * Далее мы рассмотрим, как его устанавливать. Однако мы можем отключить
     * валидацию формы с помощью этого токена с помощью атрибута
     * [IgnoreAntiforgeryToken], который применяется к классу модели (также
     * можно применять глобально в приложении).
     *
     */
    [IgnoreAntiforgeryToken]
    public class Post01 : PageModel
    {
        public string Message { get; private set; } = "";
        public string UserName { get; private set; } = "";

        // Обработчик GET-запроса страницы на сервере
        // http://localhost:5283/Post01
        // по этому запросу формируется и возвращаетсмя разметка HTML + CSS + ...
        public void OnGet() {
            Message = "Введите свое имя";
            UserName = @"-- """" --";
        } // OnGet


        // обработчик POST-запроса - обработчик переданных
        // из формы данных
        // параметры метода -- параметры POST-запроса (поля ввода)
        // имя переметров регистр-независимы
        public void OnPost(string username) {
            Message = "Введите свое имя";
            UserName = $"{username}";
        } // OnPost
    }
}
