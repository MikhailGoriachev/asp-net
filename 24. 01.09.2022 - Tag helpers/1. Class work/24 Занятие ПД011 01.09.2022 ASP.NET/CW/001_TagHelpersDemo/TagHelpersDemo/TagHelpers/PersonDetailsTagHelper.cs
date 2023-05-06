using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TagHelpersDemo.Models;

namespace TagHelpersDemo.TagHelpers;

// тег-хелпер для отображения сложной модели 

// По умолчанию tag-хелперы применяют соглашения об наименовании,
// согласно которым класс должен оканчиваться на суффикс TagHelper.
// Остальная часть названия, которая идет до TagHelper, будет использоваться
// в качестве названия тега, то есть <person-details>
//
// Хотя это не является обязательной практикой, мы могли бы определить
// просто класс  PersonDetails
public class PersonDetailsTagHelper : TagHelper
{
    // получение данных через откоытое свойство, привязка
    public Person? Person { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "ul";
        output.TagMode = TagMode.StartTagAndEndTag;

        // рендерти разметку тег-хелпера
        StringBuilder content = new StringBuilder();

        content
            .Append($"<li>Ид: <strong>{Person!.Id}</strong></li>")
            .Append($"<li>Полное имя: <strong>{Person!.FullName}</strong></li>")
            .Append($"<li>Возраст, лет: <strong>{Person!.Age}</strong></li>");

        output.Content.SetHtmlContent(content.ToString());
    } // Process
} // class PersonDetailsTagHelper

