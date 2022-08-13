using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWork.Models;

namespace HomeWork.Infrastructure
{
    // Класс Утилиты
    public static class Utils
    {
        // объект для получения случайных числовых значений
        private static Random _rand;
        public static Random Rand
        {
            get => _rand;
            set => _rand = value;
        }


        // коллекция данных бытовых приборов
        private static List<Appliance>? _appliancesInfo;
        public static List<Appliance> AppliancesInfo =>
        (_appliancesInfo ??= new List<Appliance>()
        {
            new Appliance("BOSCH KGN39UL316", "холодильник", GetVendorCode(), 40_000, _rand.Next(2,15), "fridge_001.jpg"),
            new Appliance("BEKO RCNA366K30W", "холодильник", GetVendorCode(), 35_000, _rand.Next(2,15), "fridge_002.jpg"),
            new Appliance("BOSCH KGN39VI306", "холодильник", GetVendorCode(), 44_000, _rand.Next(2,15), "fridge_003.jpg"),
            new Appliance("PHILIPS HD3136/03", "мультиварка", GetVendorCode(), 6_000, _rand.Next(2,15), "multicooker_001.jpg"),
            new Appliance("MOULINEX CE430834", "мультиварка", GetVendorCode(), 12_000, _rand.Next(2,15), "multicooker_002.jpg"),
            new Appliance("Philips XB2125/09", "пылесос", GetVendorCode(), 6_000, _rand.Next(2,15), "cleaner_001.jpg"),
            new Appliance("Philips XB2125/09", "пылесос", GetVendorCode(), 6_000, _rand.Next(2,15), "cleaner_001.jpg"),
            new Appliance("Samsung VC07M31D3HU", "пылесос", GetVendorCode(), 6_000, _rand.Next(2,15), "cleaner_002.jpg"),
            new Appliance("RZTK Cyclone 30", "пылесос", GetVendorCode(), 4_000, _rand.Next(2,15), "cleaner_003.jpg"),
        });


        #region Конструкторы

        // статический конструктор
        static Utils()
        {
            // установка значений
            _rand = new Random();
        }

        #endregion

        #region Утилиты

        // получение случайного целого числа
        public static int GetInt(int min, int max) => _rand.Next(min, max);


        // получение слуйчного дробного числа
        public static double GetDouble(double min, double max)
        {
            // если границы диапазона неверны
            if (min >= max)
                throw new InvalidDataException($"Utils.GetDouble: Указан непрваильный диапазон ({min} - {max})");

            double value;

            do
            {
                value = GetInt((int)min, (int)max - 1) + _rand.NextDouble();
            } while (value <= min && value >= max);

            return value;
        }


        // получить артикул
        public static string GetVendorCode() => $"{_rand.Next(1, 100000)}".PadLeft(6, '0');


        // получить фигуру
        public static IFigure GetFigure(int? type = null)
        {
            type ??= GetInt(0, 3);

            return type switch
            {
                0 => new Square { Side = GetDouble(12d, 20d) },
                1 => new Triangle { Sides = (GetDouble(10d, 12d), GetDouble(10d, 12d), GetDouble(10d, 12d)) },
                _ => new Rectangle { SideA = GetDouble(10d, 12d), SideB = GetDouble(10d, 12d) }
            };
        }


        // получить коллекцию фигур
        public static List<IFigure> GetFigurList(int count) => Enumerable.Repeat(0, count)
                                                                         .Select(i => GetFigure())
                                                                         .ToList();

        #endregion

    }
}
