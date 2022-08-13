using Homework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Homework.Pages
{
    public class CitiesModel : PageModel
    {
        public List<City> Cities { get; set; }
        public void OnGet()
        {
            Cities = new List<City>(new []
            {
                new City{Name = "Санкт-Петербург", CoatOfArms = "spb.png", FoundationYear = 1703, Area = 1439, Population = 5_377_503},
                new City{Name = "Ростов-на-Дону", CoatOfArms = "rostov.png", FoundationYear = 1749, Area = 348.5, Population = 1_134_694},
                new City{Name = "Новосибирск", CoatOfArms = "novosib.png", FoundationYear = 1893, Area = 502.7, Population = 1_621_330},
                new City{Name = "Екатеринбург", CoatOfArms = "ekb.png", FoundationYear = 1723, Area = 1111.7, Population = 1_493_600},
                new City{Name = "Омск", CoatOfArms = "omsk.png", FoundationYear = 1716, Area = 577.9, Population = 1_126_193},
            });
        }
    }
}
