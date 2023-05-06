using HomeWork.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace HomeWork.TagHelpers;

public class GoodTableTagHelper : TagHelper
{

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "tr";
        output.TagMode = TagMode.StartTagAndEndTag;

        StringBuilder content = new StringBuilder();

        content.Append($"<th>Ид</th> <th>Наименование</th>");
          

        output.Content.SetHtmlContent(content.ToString());
    }

}
