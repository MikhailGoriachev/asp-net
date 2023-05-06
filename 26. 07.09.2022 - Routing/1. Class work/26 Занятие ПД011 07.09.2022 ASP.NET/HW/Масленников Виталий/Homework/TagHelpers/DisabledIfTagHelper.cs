using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Homework.TagHelpers;

// Применение атрибута disabled по условию
[HtmlTargetElement(Attributes = HtmlAttribute)]
public class DisabledIfTagHelper : TagHelper
{
    public const string HtmlAttribute = "disabled-if";
    
    public bool DisabledIf { get; set; }


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.RemoveAll(HtmlAttribute);
        
        if (DisabledIf == false)
            return;

        output.AddClass("disabled", HtmlEncoder.Default);
    }
}