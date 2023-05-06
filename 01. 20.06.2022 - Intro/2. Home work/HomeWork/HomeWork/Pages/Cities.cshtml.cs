using HomeWork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Pages
{
    public class CitiesModel : PageModel
    {
        // коллекция городов
        private List<City> _cityList;

        public List<City> CityList
        {
            get => _cityList;
            set => _cityList = value; 
        }


        // обработка запроса GET
        public void OnGet()
        {
            // создание коллекции
            _cityList = _cityList ?? new List<City> {
                new City("Шанхай", 1553, "shanhai.jpg", 24_870_895, 6341),
                new City("Пекин", 1949, "pekin.png", 21_893_095, 16_410),
                new City("Мумбаи", 1507, "mumbai.gif", 15_414_288, 603),
                new City("Стамбул", 1453, "stambul.jpg", 15_029_231, 5343),
                new City("Карачи", 1700, "karachi.png", 14_910_352, 3780),
            };
        }
    }
}
