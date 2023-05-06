using Homework.Models;
using Homework.Models.Books;

namespace Homework.Common;

public static class Lang
{
    public static Dictionary<string, string> BookProperties = new ()
    {
        [nameof(Book.Amount)] = "количество",
        [nameof(Book.Author)] = "автор",
        [nameof(Book.Title)] = "название",
        [nameof(Book.Price)] = "цена",
        [nameof(Book.Year)] = "год",
    };

    public static Dictionary<string, string> FilterRules = new()
    {
        ["MaxAmount"] = "максимальное количество",
        ["MinAmount"] = "минимальное количество",
        ["Oldest"] = "самый старый год издания",
        ["Newest"] = "самый новый год издания",
        ["MostExpensive"] = "наибольшая цена",
        ["Cheapest"] = "наименьшая цена"
    };
}