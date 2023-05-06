using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HomeWork.TagHelpers;

public class SellerTableTagHelper : TagHelper
{
    public List<Seller> Sellers { get; set; } = new();

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;
        string content = "";
            content += "<td>Ид</td>" +
                       "<td>Фамилия</td>" +
                       "<td>Имя</td>" +
                       "<td>Отчество</td>" +
                       "<td>Комиссионные,(%)</td>";

            output.Content.SetHtmlContent(content);
           
    }

} // class SellerTableTagHelper
