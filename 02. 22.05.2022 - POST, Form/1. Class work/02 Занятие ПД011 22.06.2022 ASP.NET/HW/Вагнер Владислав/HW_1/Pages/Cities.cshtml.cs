using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HW_1.Models;
using HW_1.Utilities;

namespace HW_1.Pages
{
    public class CitiesModel : PageModel
    {

        public List<City> Cities;
        public void OnGet()
        {
            //Формирование списка
            Cities = new List<City>()
            {
                new City("Дрезден",
                        "~/wwroot/images/Cities/dresden.png",
                        Utils.GetRandom(500,1000),
                        Utils.GetRandom(900_000,1_200_000),
                        328.8

                    ),
                new City("Прага",
                        "~/wwroot/images/Cities/prague.jpg",
                        Utils.GetRandom(500,1000),
                        Utils.GetRandom(900_000,1_200_000),
                        496d
                    ),
                new City("Хельсинки",
                        "~/wwroot/images/Cities/helsinki.png",
                        Utils.GetRandom(500,1000),
                        Utils.GetRandom(900_000,1_200_000),
                        213.7
                    ),
                new City("Вена",
                        "~/wwroot/images/Cities/wien.png",
                        Utils.GetRandom(500,1000),
                        Utils.GetRandom(900_000,1_200_000),
                        414.6
                    ),
                new City("Брюссель",
                        "~/wwroot/images/Cities/brussel.png",
                        Utils.GetRandom(500,1000),
                        Utils.GetRandom(900_000,1_200_000),
                        32.62
                    ),
            };
        }
    }
}
