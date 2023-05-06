using Microsoft.AspNetCore.Razor.TagHelpers;
using UsingTagHelpers.Models;

namespace UsingTagHelpers.TagHelpers;

// тег-хелпер для вывода строки таблицы продаж
// Ид, Наименование товара, Продавец ФИО, Дата продажи,	Цена, Количество
// <sale-table-row sale="имяПеременной"/>
public class SaleTableRowTagHelper : TagHelper
{
    // свойство для привязки к данным, выводимым в строку табоицы
    public Sale? Sale { get; set; }

    // метод ренедринга HTML-кода
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        // ренедеринг строки таблицы со свойствами факта прожвж
        string fullName = $"{Sale!.Seller!.Surname} {Sale!.Seller!.NameSeller[0]}.{Sale!.Seller!.Patronymic[0]}.";
        string content = 
            $"<td>{Sale!.Id}</td>" +
            $"<td>{Sale!.Purchase.Good.NameGood}</td>" +
            $"<td>{fullName}</td>" +
            $"<td>{Sale!.DateSale:dd/MM/yyyy}</td>" +
            $"<td>{Sale!.PriceSale:n2}</td>" +
            $"<td>{Sale!.AmountSale:n0}</td>" +
            $"<td>" +
            $"<a class=\"btn btn-outline-primary\" href=\"EditSales/{Sale.Id}\"><i class=\"bi bi-pencil-square\"></i></a>" +
            $"<a class=\"ms-1 btn btn-outline-danger\" href=\"RemoveSale/{Sale.Id}\"><i class=\"bi bi-trash3\"></i></a>" +
            $"</td>";

        output.Content.SetHtmlContent(content);
    } // Process
} // class SaleTableRowTagHelper 

