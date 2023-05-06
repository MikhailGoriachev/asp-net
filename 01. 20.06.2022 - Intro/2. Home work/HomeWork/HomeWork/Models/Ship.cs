using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Models
{
    // Класс Корабль
    public class Ship
    {
        // название
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        // длина в метрах
        private double _length;
        public double Length
        {
            get { return _length; }
            set { _length = value; }
        }


        // ширина в метрах
        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }


        // водоизмещение в тоннах
        private int _displacement;
        public int Displacement
        {
            get { return _displacement; }
            set { _displacement = value; }
        }


        // год постройки
        private int _year;
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }


        // изображение
        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }


        #region Конструкторы

        // конструктор по умолчанию
        public Ship() : this("", 50, 1, 1, 1, "") { }

        // конструктор инициализируюший
        public Ship(string name, double length, double width, int displacement, int year, string fileName)
        {
            // установка значений
            _name = name;
            _length = length;
            _width = width;
            _displacement = displacement;
            _year = year;
            _fileName = fileName;
        }

        #endregion

        #region Методы

        #endregion
    }
}
