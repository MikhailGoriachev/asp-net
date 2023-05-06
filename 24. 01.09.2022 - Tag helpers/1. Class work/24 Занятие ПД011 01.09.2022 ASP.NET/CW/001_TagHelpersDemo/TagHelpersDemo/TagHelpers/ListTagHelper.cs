using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers;

// передача модели (данных) в тег-хелпер также выполянется
// через HTML-атрибуты

// По умолчанию tag-хелперы применяют соглашения об наименовании,
// согласно которым класс должен оканчиваться на суффикс TagHelper.
// Остальная часть названия, которая идет до TagHelper, будет использоваться
// в качестве названия тега, то есть <list>
//
// Хотя это не является обязательной практикой, мы могли бы определить
// просто класс List
public class ListTagHelper: TagHelper
{
    // открытое свойство - модель данных
    public List<string> Items { get; set; } = new();

    // методы ренедринга списка - как <ul>
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "ul";
        StringBuilder listContent = new StringBuilder();
        Items.ForEach(item => listContent.Append($"<li>{item}</li>") );

        output.Content.SetHtmlContent(listContent.ToString());
    } // Process
} // class ListTagHelper

