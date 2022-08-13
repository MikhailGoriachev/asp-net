using Application.Common;
using Application.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Application.Pages;


public sealed record SortOptions(string PropertyName, SortOrder Order);


public class Third : PageModel
{
    private static readonly ShapeFactory ShapeFactory = new();
    private static List<IShape> Shapes { get; } = new();
    public IEnumerable<IShape> Show { get; set; }


    public void OnGet() =>
        Show = Shapes;


    public void OnGetOrder(string? prop = null, SortOrder order = SortOrder.Ascending, string? type = null, bool reverse = false)
    {
        IEnumerable<IShape> result = Shapes;

        if (type is not null)
            result = Shapes.Where(shape => shape.Type.Contains(type, StringComparison.OrdinalIgnoreCase));

        if (reverse)
            result = result.Reverse();

        if (prop is not null)
            result = Sorterer<IShape>.OrderBy(result, prop, order);

        Show = result;
    }


    public void OnPost()
    {
        Shapes.Add(ShapeFactory.Create());
        Show = Shapes;
    }
}


public class ShapeFactory
{
    public IShape Create() =>
        Random.Shared.Next(1, 4) switch
        {
            1 => new Rectangle(RandomDouble, RandomDouble),
            2 => new Square(RandomDouble),
            3 => new Triangle(RandomDouble, RandomDouble, RandomDouble),
            _ => throw new ArgumentOutOfRangeException()
        };


    private double RandomDouble => Random.Shared.RealNextDouble(5, 15);
}