using Microsoft.AspNetCore.Mvc;

namespace ComponentViewResults.Components
{
    public class Component1 : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Метод Content создает экземпляр класса ContentViewComponentResult,
            // который реализует интерфейс IViewComponentResult
            // Если метод возвращает тип данных string также используется экземпляр
            // класса ContentViewComponentResult
            // Содержимое результата безопасно закодировано
            return Content("<b><script>alert('Привет! Это Component1.');</script>Component1 (ContentViewComponentResult)</b>");
        }
    }
}
