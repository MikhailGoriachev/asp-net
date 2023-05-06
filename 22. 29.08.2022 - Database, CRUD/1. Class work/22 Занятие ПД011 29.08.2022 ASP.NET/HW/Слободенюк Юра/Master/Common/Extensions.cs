using Microsoft.AspNetCore.Mvc.Rendering;


namespace Master.Common;


public static class Extensions
{
    public static ReadOnlySpan<char> ExtractSegment(this Uri uri, Index index)
    {
        var lastSegment = uri.Segments[index];

        return lastSegment.AsSpan(0, lastSegment.Length - 1);
    }


    public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<string> source) =>
        source
            .Select(x => new SelectListItem(x, x))
            .ToArray();
}