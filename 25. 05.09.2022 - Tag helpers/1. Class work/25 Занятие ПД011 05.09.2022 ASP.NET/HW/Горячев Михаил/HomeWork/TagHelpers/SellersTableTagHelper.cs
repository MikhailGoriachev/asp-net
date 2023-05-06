using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода продавцов в табличном виде
public class SellersTableTagHelper : TagHelper
{
    // элементы
    public List<Seller> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Surname}</td>
                <td>{i.Name}</td>
                <td>{i.Patronymic}</td>
                <td>{i.Interest}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
            <table class=""table table-hover align-middle"">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Фамилия</th>
                        <th>Имя</th>
                        <th>Отчество</th>
                        <th>Процент комиссии</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
            </table>
        ");
    }
}
