using System.Text;
using Homework.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Homework.TagHelpers;

// Рендеринг таблицы клиентов
public class ClientsTableTagHelper : TagHelper
{
    public List<Seller> Items { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        StringBuilder sb = new();
        
        Items.ForEach(item => sb.Append((string?)@$" <tr>
                <td>{item.Id}</td>
                <td>{item.Surname}</td>
                <td>{item.Name}</td>
                <td>{item.Patronymic}</td>
                <td>{item.Passport}</td>
                <td>{item.Interest}</td>
                </tr>"));

        output.Content.SetHtmlContent($@"
            <table class=""table"">
            <thead>
            <tr>
            <th>Id</th>
            <th>Фамилия</th>
            <th>Имя</th>
            <th>Отчество</th>
            <th>Паспорт</th>
            <th>Комиссионные, %</th>
            </tr>
            </thead>
            <tbody>
            {sb}
            </tbody>
            </table>
        ");
    }
}