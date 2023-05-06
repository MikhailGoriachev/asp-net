using Homework.Models;
using Homework.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Homework.TagHelpers;

public class SellerTableRowTagHelper : TagHelper
{
    
    public Seller? Seller { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        string content = $"<td>{Seller.Id}</td><td>{Seller.Surname}</td>" +
                         $"<td>{Seller.Name}</td><td>{Seller.Patronymic}</td>" +
                         $"<td>{Seller.Passport}</td>" +
                         $"<td>{Seller.Interest}</td>" +
                         $"<td><a class=\"btn btn-primary btn-sm\" href=\"Sellers/Edit/{Seller.Id}\">" +
                         "<i class=\"bi bi-pencil-square\"></i></a></td>";

        output.Content.SetHtmlContent(content);
    } 

} 
