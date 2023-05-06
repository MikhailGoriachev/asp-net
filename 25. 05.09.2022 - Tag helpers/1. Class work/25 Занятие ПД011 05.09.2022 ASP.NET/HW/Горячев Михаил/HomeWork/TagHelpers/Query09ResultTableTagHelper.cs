using System.Text;
using HomeWork.Models;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода результата запроса 9 в табличном виде
public class Query09ResultTableTagHelper : TagHelper
{
    // элементы
    public List<Query09Result> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <td>{i.GoodsName}</td>
                <td>{(i.MinPrice != null ? $"{i.MinPrice:n2}" : "нет данных")}</td>
                <td>{(i.AvgPrice != null ? $"{i.AvgPrice:n2}" : "нет данных")}</td>
                <td>{(i.MaxPrice != null ? $"{i.MaxPrice:n2}" : "нет данных")}</td>
                <td>{(i.Amount != null ? $"{i.Amount:n0}" : "нет данных")}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
            <table class=""table table-hover align-middle"">
                <thead>
                    <tr>
                        <th>Название</th>
                        <th>Мин. цена закупки</th>
                        <th>Сред. цена закупки</th>
                        <th>Макс. цена закупки</th>
                        <th>Количество</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
            </table>
        ");
    }
}
