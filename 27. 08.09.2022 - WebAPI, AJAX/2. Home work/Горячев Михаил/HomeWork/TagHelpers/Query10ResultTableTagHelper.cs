using System.Text;
using HomeWork.Models;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода результата запроса 8 в табличном виде
public class Query10ResultTableTagHelper : TagHelper
{
    // элементы
    public List<Query10Result> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <td>{i.Unit.LongName}</td>
                <td>{(i.Sum != null ? $"{i.Sum:n2}" : "нет данных")}</td>
                <td>{(i.Amount != null ? $"{i.Amount:n0}" : "нет данных")}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Единица измерения</th>
                        <th>Сумма продаж</th>
                        <th>Количество</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
        ");
    }
}
