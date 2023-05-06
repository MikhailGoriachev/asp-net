using Microsoft.AspNetCore.Razor.TagHelpers;
using UsingTagHelpers2.Models;

namespace UsingTagHelpers2.TagHelpers;

// Тег-хелпер для вывода данных о товаре в строку таблицы
// <good-table-row good="Имя"></good-table>
public class GoodTableRowTagHelper : TagHelper
{
    // Товар для отображения в строке таблицы - получение данных через
    // открытое свойство, привязка
    public Good? Good { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Content.SetHtmlContent(
            $"<td>{Good!.Id}</td>" +
            $"<td>{Good!.NameGood}</td>" +
            $"<td><a class=\"btn btn-outline-primary\" href=\"EditGood/{Good!.Id}\" title=\"Изменить...\">\r\n        <i class=\"bi bi-pencil-square\"></i></a></td>"
        );
    } // Process

} // class GoodTableRowTagHelper 
