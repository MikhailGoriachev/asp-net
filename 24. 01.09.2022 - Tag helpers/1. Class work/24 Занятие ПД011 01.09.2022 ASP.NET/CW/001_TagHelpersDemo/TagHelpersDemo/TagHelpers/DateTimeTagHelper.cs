using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers;

// пример использвоания асинхронного рендеринга разметки тег-хелпера

// По умолчанию tag-хелперы применяют соглашения об наименовании,
// согласно которым класс должен оканчиваться на суффикс TagHelper.
// Остальная часть названия, которая идет до TagHelper, будет использоваться
// в качестве названия тега, то есть <date-time>
//
// Хотя это не является обязательной практикой, мы могли бы определить
// просто класс DateTime
public class DateTimeTagHelper : TagHelper
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";

        // получаем вложенный контекст из дочерних tag-хелперов - сгенерированную разметку
        // html для вложенных tag-хелперов. Затем мы можем дополнительно каким-либо образом
        // изменить эту разметку и установить ее в качестве содержимого
        var target = await output.GetChildContentAsync();

        // очень удобно испошьзовать HtmlContent для формирования итоговой разметки
        var content = $"<h4>Дата и время</h4>{target.GetContent()}<hr/>";
        output.Content.SetHtmlContent(content);
    } // ProcessAsync
} // class DateTimeTagHelper

