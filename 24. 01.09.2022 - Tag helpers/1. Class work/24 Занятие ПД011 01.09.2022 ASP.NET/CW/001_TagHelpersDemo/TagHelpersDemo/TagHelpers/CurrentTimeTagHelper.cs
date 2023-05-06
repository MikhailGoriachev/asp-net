using Microsoft.AspNetCore.Razor.TagHelpers;

namespace TagHelpersDemo.TagHelpers;

// Добавлены атрибуты тег-хелпера
// current-time

// По умолчанию tag-хелперы применяют соглашения об наименовании,
// согласно которым класс должен оканчиваться на суффикс TagHelper.
// Остальная часть названия, которая идет до TagHelper, будет использоваться
// в качестве названия тега, то есть <current-time>
//
// Хотя это не является обязательной практикой, мы могли бы определить
// просто класс CurrentTime
public class CurrentTimeTagHelper : TagHelper
{
    // свойства - атрибуты тег-хелпера
    // атрибут color
    public string? Color { get; set; }

    // атрибут seconds-included
    public bool SecondsIncluded { get; set; }


    // рендеринг разметки тег-хелпера
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        // если SecondsIncluded установлен в true добавляем секунды
        var time = DateTime.Now.ToString(SecondsIncluded ? "HH:mm:ss" : "HH:mm");

        // тип элементва разметки
        output.TagName = "span";

        // тип тега -            <a></a>   <hr />   <br>
        output.TagMode = TagMode.StartTagAndEndTag;

        // устанавливаем цвет, если свойство Color не равно null
        if (Color != null)
        {
            // Attributes - это атрибуты HTML
            output.Attributes.SetAttribute("style", $"color:{Color};");
        } // if

        // в д.с. SetContent удобнее SetHtmlContent т.к. требуетсям уствановить HTML--атрибут в тег
        output.Content.SetContent(time);
    }
} // class CurrentTimeTagHelper

