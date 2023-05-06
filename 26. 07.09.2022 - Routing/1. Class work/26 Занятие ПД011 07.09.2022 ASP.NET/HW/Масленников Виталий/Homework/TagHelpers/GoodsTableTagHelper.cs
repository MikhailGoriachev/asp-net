using System.Text;
using Homework.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Homework.TagHelpers;

public class GoodsTableTagHelper : TagHelper
{
    public List<Goods> Items { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table");

        StringBuilder sb = new();
        
        Items.ForEach(item => sb.Append((string?)@$" <tr>
                <td>{item.Id}</td>
                <td>{item.Name}</td>
                <td><a class=""btn btn-outline-primary"" href=""Goods/Edit/{item.Id}"">
                <i class=""bi bi-pencil-square""></i></a></td>
                </tr>"));

        output.Content.SetHtmlContent($@"
            <thead>
            <tr>
            <th>Id</th>
            <th>Наименование</th>
            <th>
            <a class=""btn btn-primary btn-sm"" href=""Goods/Create/"">Добавить</a>
            </th>
            </tr>
            </thead>
            <tbody>
            {sb}
            </tbody>
        ");
    }
}