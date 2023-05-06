using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers;

// По умолчанию tag-хелперы применяют соглашения об наименовании,
// согласно которым класс должен оканчиваться на суффикс TagHelper.
// Остальная часть названия, которая идет до TagHelper, будет использоваться
// в качестве названия тега, то есть <first>
//
// Хотя это не является обязательной практикой, мы могли бы определить
// просто класс First
public class FirstTagHelper : TagHelper
{
    // Для генерации элемента html на основе тега используется метод Process.
    // Он принимает два параметра: объект TagHelperContext, представляющий
    // контекст тега (его содержимое, атрибуты), и объект TagHelperOutput, 
    // отвечающий за генерацию выходного элемента html на основе тега.
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "div";    // заменяет тег <first> тегом <div>
                                   // устанавливаем содержимое элемента
        output.Content.SetContent($"Текущее время: {DateTime.Now:HH:mm:ss}");
    } // Process
}

