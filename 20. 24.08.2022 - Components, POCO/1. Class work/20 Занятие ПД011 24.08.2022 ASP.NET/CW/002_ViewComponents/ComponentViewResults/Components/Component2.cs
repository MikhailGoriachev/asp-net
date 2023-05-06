using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ComponentViewResults.Components;

public class Component2 : ViewComponent
{
    public IViewComponentResult Invoke() {
        IHtmlContent content = new HtmlString("<script>alert('Привет! Это Component2.');</script><b>Component2 (HtmlContentViewComponentResult)</b>");
        HtmlContentViewComponentResult result = new HtmlContentViewComponentResult(content);

        // HtmlContentViewComponentResult - класс, для указания фрагмента разметки
        // Для данного типа результата не предусмотрен метод в базовом классе
        // Результат не будет кодироваться
        // Если метод Invoke возвращает IHtmlContent будет использоваться тип результата
        // HtmlContentViewComponentResult
        return result;
    }
}

