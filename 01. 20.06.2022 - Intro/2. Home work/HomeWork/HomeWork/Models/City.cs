using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Models
{
    // Класс Город
    // название, год основания, герб, население на текущий момент, площадь
    public class City
    {
        // название
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        // год основания
        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }


        // герб (название файла)
        private string _emblemFile;
        public string EmblemFile
        {
            get { return _emblemFile; }
            set { _emblemFile = value; }
        }


        // население 
        private int _population;
        public int Population
        {
            get { return _population; }
            set { _population = value; }
        }


        // площадь (км2)
        private int _area;
        public int Area
        {
            get { return _area; }
            set { _area = value; }
        }


        #region Конструкторы

        // конструктор по умолчанию
        public City() : this("", 2000, "", 10_000, 15_000) { }

        // конструктор инициализирующий
        // название, год основания, герб, население на текущий момент, площадь
        public City(string name, int year, string empblemFile, int population, int area)
        {
            // установка значений
            _name = name;
            _year = year;
            _emblemFile = empblemFile;
            _population = population;
            _area = area;
        }

        #endregion

        #region Методы

        #endregion
    }
}
