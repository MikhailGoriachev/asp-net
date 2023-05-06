using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using UsingTagHelpers2.Models;

namespace UsingTagHelpers2.TagHelpers;

public class GoodTableTagHelper : TagHelper
{
    // элементы
    public IEnumerable<Good> Items { get; set; }

    // разрешение кнопки добавления
    public bool GoodsCreate { get; set; } = false;


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "table";

        StringBuilder sb = new();
        foreach (var item in Items) {
            sb.Append($@"
            <tr>
                <th>{item.Id}</th>
                <td>{item.NameGood}</td>
                <td>
                    <a class=""btn btn-outline-primary"" href=""/GoodsList/EditGoods/{item.Id}"">
                        <i class=""bi bi-pencil-square""></i>
                    </a>
                </td>
            </tr>");
        }

        // кнопка добавдения товара, рендерим только по значению свойства/атрибута GoodsCreate
        string addButton = @"
            <a class=""btn btn-success"" href=""/GoodsList/AddGoods"">
                <i class=""bi bi-plus-lg""></i> Добавить
            </a>";

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Название</th>
                        <th>
                            {(GoodsCreate?addButton:"")}
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
        ");
    }
}

