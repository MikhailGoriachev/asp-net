using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода продавцов в табличном виде
public class SellersTableBodyTagHelper : TagHelper
{
    // элементы
    public List<Seller> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tbody";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Surname}</td>
                <td>{i.Name}</td>
                <td>{i.Patronymic}</td>
                <td>{i.Interest}</td>
                <td>
                    <a class=""btn btn-outline-primary"" href=""/Sellers/EditSeller/{i.Id}"">
                        <i class=""bi bi-pencil-square""></i>
                    </a>
                </td>
            </tr>"));

        output.Content.SetHtmlContent(sb.ToString());
    }
}
