using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_1.Models
{
    public class City
    {

        //Название
        public string CityName { get; private set; }

        //Герб
        public string Sign { get; private set; }

        //Год основания
        public int FoundYear { get; private set; }
        //Население
        public int Population { get; private set; }

        //Площадь
        public double Square { get; private set; }
        public City(string cityName, string sign, int foundYear, int population, double square)
        {
            CityName = cityName;
            Sign = sign;
            FoundYear = foundYear;
            Population = population;
            Square = square;
        }

    }
}
