using Homework.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Homework.Models;

namespace Homework.TagHelpers;

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
        string fullName = $"{Sale!.Seller!.Surname} {Sale!.Seller!.Name[0]}.{Sale!.Seller!.Patronymic[0]}.";
        string content = 
            $"<td>{Sale!.Id}</td>" +
            $"<td>{Sale.SaleDate:dd.MM.yy}</td>" +
            $"<td>{Sale.Purchase!.Goods!.Name}</td>" +
            $"<td>{Sale.Seller!.ShortName}</td>" +
            $"<td>{Sale!.Amount}</td>" +
            $"<td>{Sale!.Purchase!.Price}</td>" +
            $"<td>{Sale!.Price}</td>" +
            $"<td>{Sale!.Unit!.Short}</td>" +
            $"<td>" +
            $"<a class=\"btn btn-outline-primary\" href=\"Sales/Edit/{Sale.Id}\"><i class=\"bi bi-pencil-square\"></i></a>" +
            $"<a class=\"ms-1 btn btn-outline-danger\" href=\"Sales/Delete/{Sale.Id}\"><i class=\"bi bi-trash3\"></i></a>" +
            $"</td>";

        output.Content.SetHtmlContent(content);
    } // Process
} // class SaleTableRowTagHelper 

