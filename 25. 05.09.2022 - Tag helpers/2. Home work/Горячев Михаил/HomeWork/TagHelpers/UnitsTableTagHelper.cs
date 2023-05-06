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
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.ShortName}</td>
                <td>{i.LongName}</td>
                <td>
                    <a class=""btn btn-outline-primary"" href=""/Units/EditUnit/{i.Id}"">
                        <i class=""bi bi-pencil-square""></i>
                    </a>
                </td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Сокращённое название</th>
                        <th>Полное название</th>
                        <th>
                            <a class=""btn btn-success"" href=""/Units/AddUnit"">
                                <i class=""bi bi-plus-lg""></i> Добавить
                            </a>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
        ");
    }
}
