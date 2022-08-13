using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Application.Common;


[HtmlTargetElement("a", Attributes = "is-active, asp-page")]
public sealed class PageActiveTagHelper : TagHelper
{
    [HtmlAttributeName("asp-page")]
    public string PageName { get; set; } = null!;


    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll("is-active");

        if (IsNeedToMark())
        {
            output.AddClass("active", HtmlEncoder.Default);
            output.Attributes.Add("aria-current", "page");
        }
    }


    private bool IsNeedToMark() =>
        ViewContext.RouteData.Values["Page"]!
            .ToString()!
            .Contains(PageName, StringComparison.Ordinal);
}