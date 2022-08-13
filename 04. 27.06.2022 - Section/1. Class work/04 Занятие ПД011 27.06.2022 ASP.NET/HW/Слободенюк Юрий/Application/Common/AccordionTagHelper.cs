using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Application.Common;


[HtmlTargetElement("accordion", Attributes = nameof(Title))]
public sealed class AccordionTagHelper : TagHelper
{
    public string Title { get; set; } = null!;

    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var body = (await output.GetChildContentAsync()).GetContent();
        var accordionId = GetUniqueIdentifier();
        var headingId = GetUniqueIdentifier();
        var collapseId = GetUniqueIdentifier();

        output.TagName = "";
        output.Content.SetHtmlContent(@$"
<div class=""accordion"" id=""{accordionId}"">
    <div class=""accordion-item"">
        <h2 class=""accordion-header"" id=""{headingId}"">
            <button class=""accordion-button"" 
                    type=""button"" 
                    data-bs-toggle=""collapse"" 
                    data-bs-target=""#{collapseId}"" 
                    aria-expanded=""true"" 
                    aria-controls=""{collapseId}"">
            {Title}
            </button>
        </h2>
        <div id=""{collapseId}"" class=""accordion-collapse collapse show"" 
             aria-labelledby=""{headingId}"" 
             data-bs-parent=""#{accordionId}"">
            <div class=""accordion-body"">
                {body}
            </div>
        </div>
    </div>
</div>
");
    }


    private static string GetUniqueIdentifier() =>
        $"j-{Guid.NewGuid()}";
}