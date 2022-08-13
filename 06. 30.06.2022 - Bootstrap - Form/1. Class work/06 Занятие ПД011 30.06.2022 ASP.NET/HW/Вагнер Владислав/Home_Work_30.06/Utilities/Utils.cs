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

        public static int LastApplianceId = 1;

        #endregion

        #region Фиугры
        //Генерация фигуры
        public static IFigure GetFigure()
        {
            IFigure[] figures =
            {
                new Rectangle(GetRandom(5,11),GetRandom(5,11)),
                new Triangle(GetRandom(5,11),GetRandom(5,11),GetRandom(5,11)),
                new Square(GetRandom(5,11))
            };

            IFigure figure = figures[GetRandom(0, figures.Length)];
            figure.Id = LastFigureId++;
            return figure;
        }

        //Словарь изображений
        public static Dictionary<string, string> Icons = new Dictionary<string, string>()
        {
            {"Прямоугольник","Rectangle.jpg"},
            {"Квадрат","Square.jpg"},
            {"Треугольник","Triangle.png"},
        };


        public static int LastFigureId = 1;
        #endregion

    } // class Utils
}