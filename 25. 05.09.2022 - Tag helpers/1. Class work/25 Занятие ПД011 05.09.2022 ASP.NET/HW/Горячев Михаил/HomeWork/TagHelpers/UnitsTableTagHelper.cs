using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода единиц измерения в табличном виде
public class UnitsTableTagHelper : TagHelper
{
    // элементы
    public List<Unit> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.ShortName}</td>
                <td>{i.LongName}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
            <table class=""table table-hover align-middle"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Сокращённое название</th>
                        <th>Полное название</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
            </table>
        ");
    }
}
