using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Master.TagHelpers;


[HtmlTargetElement("a", Attributes = $"{HtmlAttribute}, asp-controller, asp-action")]
public sealed class IsActiveTagHelper : TagHelper
{
    public const string HtmlAttribute = "is-active";
    
    [HtmlAttributeName("asp-controller")]
    public string ControllerName { get; set; } = null!;
    
    [HtmlAttributeName("asp-action")]
    public string ActionName { get; set; } = null!;
    
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = null!;


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll(HtmlAttribute);

        if (IsNeedToMark())
        {
            output.AddClass("active", HtmlEncoder.Default);
        }
    }


    private bool IsNeedToMark()
    {
        var controller = ViewContext.RouteData.Values["controller"]?.ToString();
        var action = ViewContext.RouteData.Values["action"]?.ToString();

        if (controller is null || action is null)
            return false;
        
        var comparison = StringComparison.OrdinalIgnoreCase;

        return action.Equals(ActionName, comparison) &&
               controller.Equals(ControllerName, comparison);
    }
}