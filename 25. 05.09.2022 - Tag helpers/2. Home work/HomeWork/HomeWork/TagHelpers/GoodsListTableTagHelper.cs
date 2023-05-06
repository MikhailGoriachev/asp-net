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
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Name}</td>
                <td>
                    <a class=""btn btn-outline-primary"" href=""/GoodsList/EditGoods/{i.Id}"">
                        <i class=""bi bi-pencil-square""></i>
                    </a>
                </td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Название</th>
                        <th>
                            <a class=""btn btn-success"" href=""/GoodsList/AddGoods"">
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
