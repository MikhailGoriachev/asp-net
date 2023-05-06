using Microsoft.AspNetCore.Razor.TagHelpers;
using UsingTagHelpers2.Models;

namespace UsingTagHelpers2.TagHelpers;

// Тег-хелпер для вывода сведений о продавце в строку таблицы
// <seller-table-row seller="ИмяМодели"></seller-table-row>
public class SellerTableRowTagHelper : TagHelper
{
    // Открытое свойство для привязки данных о продавце
    public Seller? Seller { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        string content = $"<td>{Seller.Id}</td><td>{Seller.Surname}</td>" +
                         $"<td>{Seller.NameSeller}</td><td>{Seller.Patronymic}</td>" +
                         $"<td>{Seller.Interest}</td>" +
                         $"<td><a class=\"btn btn-outline-primary\" href=\"EditSeller/{Seller.Id}\"><i class=\"bi bi-pencil-square\"></i></a></td>";

        output.Content.SetHtmlContent(content);
    } // Process

} // class SellerTableRowTagHelper
