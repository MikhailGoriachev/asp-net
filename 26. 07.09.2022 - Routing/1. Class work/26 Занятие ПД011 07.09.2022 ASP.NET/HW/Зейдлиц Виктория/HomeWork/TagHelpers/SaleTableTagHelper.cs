using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Тег-хелпер для вывода данных о продажах в строку таблицы
public class SaleTableTagHelper : TagHelper
{
    public Sale Sales { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetHtmlContent(
                    $"<td>{Sales.Id}</td>" +
                    $"<td>{Sales!.Purchase!.Good!.NameGood}</td>" +
                    $"<td>{Sales!.Seller!.Surname} {Sales!.Seller!.NameSeller![0]}.{Sales!.Seller!.Patronymic![0]}.</td>" +
                    $"<td>{Sales.DateSale:dd.MM.yyyy}</td>" +
                    $"<td>{Sales.PriceSale}</td>" +
                    $"<td>{Sales.AmountSale}</td>" +
                    $"<td><a class=\"btn btn-outline-primary ms-1\" href=\"EditSale/{Sales!.Id}\" title=\"Изменить...\"><i class=\"bi bi-pencil-square\"></i></a><a class=\"btn btn-outline-danger\" href=\"RemoveSale/{Sales.Id}\"><i class=\"bi bi-trash3\"></i></a></td>");

    }
}
