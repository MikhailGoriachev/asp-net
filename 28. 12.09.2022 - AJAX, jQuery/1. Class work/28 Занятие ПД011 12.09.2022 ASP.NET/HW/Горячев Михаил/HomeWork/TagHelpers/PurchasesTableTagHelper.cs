using System.Text;
using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода закупок в табличном виде
public class PurchasesTableTagHelper : TagHelper
{
    // элементы
    public List<Purchase> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <th>{i.Id}</th>
                <td>{i.Goods?.Name}</td>
                <td>{i.Unit?.LongName}</td>
                <td>{i.Price:n2}</td>
                <td>{i.Amount:n0}</td>
                <td>{i.DatePurchase:dd.MM.yyyy}</td>
                <td>
                    <a class=""btn btn-outline-primary"" href=""/Purchases/EditPurchase/{i.Id}"">
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
                        <th>Ед. измерения</th>
                        <th>Цена закупки (&#8381;)</th>
                        <th>Количество</th>
                        <th>Дата закупки</th>
                        <th>
                            <a class=""btn btn-success"" href=""/Purchases/AddPurchase"">
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
