using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода продаж  в табличном виде
public class SalesTableBodyTagHelper : TagHelper
{
    // элементы
    public List<Sale> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tbody";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Seller?.Surname} {i.Seller?.Name[0]}. {i.Seller?.Patronymic[0]}.</td>
                <td>{i.Purchase?.Goods?.Name}</td>
                <td>{i.Unit?.ShortName}</td>
                <td>{i.Price:n2}</td>
                <td>{i.Amount:n0}</td>
                <td>{i.DateSell:dd.MM.yyyy}</td>
            </tr>"));

        output.Content.SetHtmlContent(sb.ToString());
    }
}
