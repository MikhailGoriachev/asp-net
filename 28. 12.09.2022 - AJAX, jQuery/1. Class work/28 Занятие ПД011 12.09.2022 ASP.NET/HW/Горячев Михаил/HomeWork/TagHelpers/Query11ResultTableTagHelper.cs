using System.Text;
using HomeWork.Models;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода результата запроса 8 в табличном виде
public class Query11ResultTableTagHelper : TagHelper
{
    // элементы
    public List<Query11Result> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <td>{i.Seller.IdAndFullName}</td>
                <td>{(i.MinPrice != null ? $"{i.MinPrice:n2}" : "нет данных")}</td>
                <td>{(i.AvgPrice != null ? $"{i.AvgPrice:n2}" : "нет данных")}</td>
                <td>{(i.MaxPrice != null ? $"{i.MaxPrice:n2}" : "нет данных")}</td>
                <td>{(i.AmountSales != null ? $"{i.AmountSales:n0}" : "нет данных")}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Название</th>
                        <th>Мин. цена продажи</th>
                        <th>Сред. цена продажи</th>
                        <th>Макс. цена продажи</th>
                        <th>Количество</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
        ");
    }
}
