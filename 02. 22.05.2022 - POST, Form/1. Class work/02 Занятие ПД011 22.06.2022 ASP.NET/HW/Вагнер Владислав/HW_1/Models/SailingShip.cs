using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_1.Models
{
    public class SailingShip
    {
        //Завание и тип корабля
        public string ShipName { get; set; }

        //Длина
        public double Length { get; set; }

        //Ширина
        public double Width { get; set; }

        //Водоизмещение
        public int Weight { get; set; }

        //Год постройки
        public int ProdYear { get; set; }

        //Изображение
        public string Image { get; set; }

        public SailingShip(string name,double length,double width, int weight, int prodYear, string image)
        {
            ShipName = name;
            Length = length;
            Width = width;
            Weight = weight;
            ProdYear = prodYear;
            Image = image;
        }
    }
}
