using System.Text;
using Homework.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Homework.TagHelpers;

// Рендеринг таблицы клиентов
public class SellersTableTagHelper : TagHelper
{
    public List<Seller> Items { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table");

        StringBuilder sb = new();
        
        Items.ForEach(item => sb.Append((string?)@$" <tr>
                <td>{item.Id}</td>
                <td>{item.Surname}</td>
                <td>{item.Name}</td>
                <td>{item.Patronymic}</td>
                <td>{item.Passport}</td>
                <td>{item.Interest}</td>
                <td><a class=""btn btn-outline-primary"" href=""Sellers/Edit/{item.Id}"">
                <i class=""bi bi-pencil-square""></i></a></td>
                </tr>"));

        output.Content.SetHtmlContent($@"
            <thead>
            <tr>
            <th>Id</th>
            <th>Фамилия</th>
            <th>Имя</th>
            <th>Отчество</th>
            <th>Паспорт</th>
            <th>Комиссионные, %</th>
            <th>
            <a class=""btn btn-outline-primary"" href=""Sellers/Create/"">Добавить</a>
            </th>
            </tr>
            </thead>
            <tbody>
            {sb}
            </tbody>
        ");
    }
}