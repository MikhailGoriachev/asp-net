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
                new Appliance("BOSCH KGN39UL316", "холодильник", GetVendorCode(), 40_000, _rand.Next(2, 15),
                    "fridge_001.jpg"),
                new Appliance("BEKO RCNA366K30W", "холодильник", GetVendorCode(), 35_000, _rand.Next(2, 15),
                    "fridge_002.jpg"),
                new Appliance("BOSCH KGN39VI306", "холодильник", GetVendorCode(), 44_000, _rand.Next(2, 15),
                    "fridge_003.jpg"),
                new Appliance("PHILIPS HD3136/03", "мультиварка", GetVendorCode(), 6_000, _rand.Next(2, 15),
                    "multicooker_001.jpg"),
                new Appliance("MOULINEX CE430834", "мультиварка", GetVendorCode(), 12_000, _rand.Next(2, 15),
                    "multicooker_002.jpg"),
                new Appliance("Philips XB2125/09", "пылесос", GetVendorCode(), 6_000, _rand.Next(2, 15),
                    "cleaner_001.jpg"),
                new Appliance("Philips XB2125/09", "пылесос", GetVendorCode(), 6_000, _rand.Next(2, 15),
                    "cleaner_001.jpg"),
                new Appliance("Samsung VC07M31D3HU", "пылесос", GetVendorCode(), 6_000, _rand.Next(2, 15),
                    "cleaner_002.jpg"),
                new Appliance("RZTK Cyclone 30", "пылесос", GetVendorCode(), 4_000, _rand.Next(2, 15),
                    "cleaner_003.jpg"),
            });


        // коллекция данных о кораблях
        private static List<Ship>? _shipsInfo;

        public static List<Ship> ShipsInfo =>
            (_shipsInfo ??= new List<Ship>()
            {
                new Ship(ShipTypes[4], "PRELUDE", 488d, 74d, 394330d, 2017, "PRELUDE.jpg"),
                new Ship(ShipTypes[5], "PIONEERING SPIRIT", 477d, 124d, 499125d, 2014, "PIONEERING_SPIRIT.jpg"),
                new Ship(ShipTypes[4], "CORAL-SUL FLNG", 277d, 46d, 346165d, 2021, "CORAL-SUL_FLNG.jpg"),
                new Ship(ShipTypes[2], "FSO AFRICA", 378d, 68d, 236638d, 2002, "FSO_AFRICA.jpg"),
                new Ship(ShipTypes[2], "FSO ASIA", 380d, 68d, 236638d, 2002, "FSO_ASIA.jpg"),
                new Ship(ShipTypes[0], "CMA CGM SORBONNE", 400d, 61d, 236583d, 2021, "CMA_CGM_SORBONNE.jpg"),
                new Ship(ShipTypes[0], "EVER ARM", 400d, 62d, 200000d, 2022, "EVER_ARM.jpg"),
                new Ship(ShipTypes[0], "MSC MIA", 400d, 62d, 228149d, 2019, "MSC_MIA.jpg"),
                new Ship(ShipTypes[0], "MSC AMELIA", 400d, 61d, 228406d, 2021, "MSC_AMELIA.jpg"),
                new Ship(ShipTypes[2], "TA'KUNTAH", 392d, 60d, 170706d, 1977, "TA'KUNTAH.jpg"),
            });


        // типы кораблей
        private static List<string>? _shipTypes;

        public static List<string> ShipTypes =>
            (_shipTypes ??= new List<string>()
            {
                "судно-контейнеровоз",
                "сухогруз",
                "танкер",
                "ледокол",
                "портовый буксиры",
                "плавучий кран",
                "рыболовецкий траулер",
                "научно-исследовательское судно",
                "плавучий сухой док",
                "судно-газовоз"
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

        // сохранить файл на сервер
        public static void SaveFile(string path, IFormFile file)
        {
            using Stream stream = File.Create(Path.Combine(path, file.FileName));

            file.CopyTo(stream);
        }

        #endregion

    }
}
