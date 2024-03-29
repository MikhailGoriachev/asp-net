﻿using System.Text;
using HomeWork.Models;
using HomeWork.Models.QueryResults;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

// Класс Тег хелпер для вывода результата запроса 10 в табличном виде
public class Query09ResultTableTagHelper : TagHelper
{
    // элементы
    public List<Query09Result> Items { get; set; } = new();


    // работа
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "table";
        output.Attributes.Add("class", "table table-hover align-middle");

        StringBuilder sb = new();

        Items.ForEach(i => sb.Append($@"
            <tr>
                <td>{i.Seller.Surname} {i.Seller.Name[0]}. {i.Seller.Patronymic[0]}.</td>
                <td>{(i.MinPrice != null ? $"{i.MinPrice:n2}" : "нет данных")}</td>
                <td>{(i.AvgPrice != null ? $"{i.AvgPrice:n2}" : "нет данных")}</td>
                <td>{(i.MaxPrice != null ? $"{i.MaxPrice:n2}" : "нет данных")}</td>
                <td>{(i.AmountSales != null ? $"{i.AmountSales:n0}" : "нет данных")}</td>
            </tr>"));

        output.Content.SetHtmlContent(
            $@"
                <thead>
                    <tr>
                        <th>Продавец</th>
                        <th>Мин. цена ед.</th>
                        <th>Сред. цена ед.</th>
                        <th>Макс. цена ед.</th>
                        <th>Количество</th>
                    </tr>
                </thead>
                <tbody>
                    {sb}
                </tbody>
        ");
    }
}
