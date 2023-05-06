using System.Text;
using HomeWork.Models;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода результата запроса 7 в табличном виде
public class Query07ResultTableTagHelper : TagHelper
{
    // элементы
    public List<Query07Result> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <td>{i.DateSale:dd.MM.yyyy}</td>
                <td>{i.GoodsName}</td>
                <td>{i.PurchasePrice:n2}</td>
                <td>{i.SalePrice:n2}</td>
                <td>{i.Amount:n0}</td>
                <td>{i.Profit:n2}</td>                
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Дата продажи</th>
                        <th>Наименование товара</th>
                        <th>Цена закупки (&#8381;)</th>
                        <th>Цена продажи (&#8381;)</th>
                        <th>Количество проданных единиц</th>
                        <th>Прибыль (&#8381;)</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
        ");
    }
}
