using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода товаров в табличном виде
public class GoodsListTableTagHelper : TagHelper
{
    // элементы
    public List<Goods> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Name}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
            <table class=""table table-hover align-middle"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Название</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
            </table>
        ");
    }
}
