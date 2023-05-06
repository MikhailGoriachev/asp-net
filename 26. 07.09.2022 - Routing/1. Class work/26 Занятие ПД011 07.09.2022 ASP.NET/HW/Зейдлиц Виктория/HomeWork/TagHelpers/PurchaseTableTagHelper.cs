using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Тег-хелпер для вывода данных о закупках в строку таблицы
public class PurchaseTableTagHelper : TagHelper
{
    public Purchase Purchases { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetHtmlContent(
                    $"<td>{Purchases.Id}</td>" +
                    $"<td>{Purchases!.Good!.NameGood}</td>" +
                    $"<td>{Purchases!.Unit!.Short}</td>" +
                    $"<td>{Purchases.DatePurchase:dd.MM.yyyy}</td>" +
                    $"<td>{Purchases.PricePurchase}</td>" +
                    $"<td>{Purchases.Amount}</td>" +
                    $"<td><a class=\"btn btn-outline-primary\" href=\"EditPurchase/{Purchases!.Id}\" title=\"Изменить...\"><i class=\"bi bi-pencil-square\"></i></a></td>");

    }

}
