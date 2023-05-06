using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода закупок в табличном виде
public class PurchasesTableTagHelper : TagHelper
{
    // элементы
    public List<Purchase> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Goods?.Name}</td>
                <td>{i.Unit?.LongName}</td>
                <td>{i.Price:n2}</td>
                <td>{i.Amount:n0}</td>
                <td>{i.DatePurchase:dd.MM.yyyy}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
            <table class=""table table-hover align-middle"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Название</th>
                        <th>Ед. измерения</th>
                        <th>Цена закупки (&#8381;)</th>
                        <th>Количество</th>
                        <th>Дата закупки</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
            </table>
        ");
    }
}
