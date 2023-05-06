using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Тег-хелпер для вывода данных о продавце в строку таблицы
public class SellerTableTagHelper : TagHelper
{
    public Seller Sellers { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetHtmlContent(
                    $"<td>{Sellers.Id}</td>" +
                    $"<td>{Sellers.Surname}</td>" +
                    $"<td>{Sellers.NameSeller}</td>" +
                    $"<td>{Sellers.Patronymic}</td>" +
                    $"<td>{Sellers.Interest}</td>" +
                    $"<td><a class=\"btn btn-outline-primary\" href=\"EditSeller/{Sellers!.Id}\" title=\"Изменить...\"><i class=\"bi bi-pencil-square\"></i></a></td>");

    }

} // class SellerTableTagHelper
