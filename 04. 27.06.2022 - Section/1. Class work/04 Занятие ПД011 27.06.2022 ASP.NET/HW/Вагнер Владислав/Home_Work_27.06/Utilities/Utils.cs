using System;
using System.Text;
using System.Threading;
using Home_Work.Models;
using Home_Work.Models.Figures;

namespace Home_Work.Utilities
{
    public static class Utils
    {
        
        // объект для получения случайных чисел
        public static Random Random = new Random();
        
        // формирование случайных чисел в диапазоне от lo до hi
        public static double GetRandom(double lo, double hi)
            => lo + (hi - lo)*Random.NextDouble();
        public static int GetRandom(int lo, int hi)
            => Random.Next(lo,hi);

        #region Приборы
        //Названия приборов
        public static string GetApplianceName()
        {
            string[] names =
            {
                "Холодильник",
                "Стиральная машина",
                "Микроволновая печь",
                "Пылесос",
            };

            return names[GetRandom(0, names.Length)];
        }

        //Статическая коллекция приборов
        public static List<Appliance> AppliancesStatic = new List<Appliance>(){
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
            new Appliance(GetApplianceName(), GetRandom(10_000, 500_000), GetRandom(100, 300)*100, GetRandom(2, 50)),
        };
        #endregion

        #region Фиугры
        //Генерация фигуры
        public static iFigure GetFigure()
        {
            iFigure[] figures =
            {
                new Rectangle(GetRandom(5,11),GetRandom(5,11)),
                new Triaingle(GetRandom(5,11),GetRandom(5,11),GetRandom(5,11)),
                new Square(GetRandom(5,11))
            };


            return figures[GetRandom(0, figures.Length)];
        }

        //Словарь изображений
        public static Dictionary<string, string> Icons = new Dictionary<string, string>()
        {
            {"Прямоугольник","Rectangle.jpg"},
            {"Квадрат","Square.jpg"},
            {"Треугольник","Triangle.png"},
        };
        #endregion

    } // class Utils
}