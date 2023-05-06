using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers;

// По умолчанию tag-хелперы применяют соглашения об наименовании,
// согласно которым класс должен оканчиваться на суффикс TagHelper.
// Остальная часть названия, которая идет до TagHelper, будет использоваться
// в качестве названия тега, то есть <second>
//
// Хотя это не является обязательной практикой, мы могли бы определить
// просто класс Second
public class SecondTagHelper : TagHelper
{
    // Для генерации элемента html на основе тега используется метод Process.
    // Он принимает два параметра: объект TagHelperContext, представляющий
    // контекст тега (его содержимое, атрибуты), и объект TagHelperOutput,
    // отвечающий за генерацию выходного элемента html на основе тега.
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        // устанавливаем HTML-содержимое элемента 
        output.TagName = "div";
        output.Content.SetHtmlContent($"<strong>Текущая дата: {DateTime.Now:dd-MM-yyyy}</strong>");
    } // Process
}