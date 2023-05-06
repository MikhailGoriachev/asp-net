using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace HomeWork.TagHelpers;

// Тег-хелпер для вывода данных о товаре в строку таблицы
public class GoodTableTagHelper : TagHelper
{
    public Good Goods { get; set; } = new();


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Content.SetHtmlContent(
                    $"<td>{Goods.Id}</td>" +
                    $"<td>{Goods.NameGood}</td>" +
                    $"<td><a class=\"btn btn-outline-primary\" href=\"EditGood/{Goods!.Id}\" title=\"Изменить...\"><i class=\"bi bi-pencil-square\"></i></a></td>");

    }

}
