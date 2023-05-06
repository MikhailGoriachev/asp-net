using Homework.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace Homework.TagHelpers;

// Добавление ряда атрибутов asp-route-xxxx по переданной коллекции данных параметров
[HtmlTargetElement("a", Attributes = "add-routes")]
public class AddRoutesTagHelper : AnchorTagHelper
{
    [HtmlAttributeName("add-routes")]
    public List<RouteParameter>? AddRoutes { get; set; }

    public AddRoutesTagHelper(IHtmlGenerator generator) : base(generator) { }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll("add-routes");
        
        AddRoutes?.ForEach(p =>
        {
            if (p.Value is not null)
                RouteValues.Add(p.Key, p.Value);
        });
        
        base.Process(context, output);
    }
}
