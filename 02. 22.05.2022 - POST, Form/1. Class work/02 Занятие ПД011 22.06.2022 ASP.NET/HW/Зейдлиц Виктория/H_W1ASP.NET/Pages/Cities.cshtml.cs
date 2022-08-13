using H_W1ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace H_W1ASP.NET.Pages
{
    public class CitiesModel : PageModel
    {
        public List<City> Cities = new List<City>(new[]
        {
            new City("Москва", 2561.5, 1147, 12_692_466),
            new City("Санкт-Петербург", 1493, 1703, 5_377_503),
            new City("Казань", 588.98, 1005, 159_173),
            new City("Екатеринбург", 468, 1723, 1_483_119),
            new City("Самара", 541.4, 1586, 1_136_709),
        });

        public void OnGet()
        {
        }
    }
}
