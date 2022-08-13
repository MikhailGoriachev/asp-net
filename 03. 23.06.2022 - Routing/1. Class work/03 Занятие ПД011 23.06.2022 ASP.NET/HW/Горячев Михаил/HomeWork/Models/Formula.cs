using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork.Models
{
    // Класс Формула
    public class Formula
    {
        // текущее число
        private (double a, double b)? _numbers;

        public (double a, double b)? Numbers
        {
            get => _numbers;
            set => Calculate(value);
        }


        // z1
        private double? _z1;

        public double? Z1
        {
            get => _z1;
            private set => _z1 = value;
        }


        // z2
        private double? _z2;

        public double? Z2
        {
            get => _z2;
            private set => _z2 = value;
        }


        #region Конструкторы

        // конструктор по умолчанию
        public Formula()
        {

        }


        // конструктор инициализирующий
        public Formula(double a, double b)
        {
            // установка значений
            Numbers = (a, b);
        }

        #endregion

        #region Методы

        // вычислить результат
        public void Calculate((double a, double b)? numbers = null)
        {
            // установка текущего числа
            _numbers = numbers ?? _numbers;

            if (_numbers == null)
                return;

            // временные значения
            double powB = _numbers.Value.b * _numbers.Value.b,
                powBSubA = powB - _numbers.Value.a;

            // вычисление z1
            _z1 = (Math.Sin(_numbers.Value.a) + Math.Cos(powBSubA)) / (Math.Cos(_numbers.Value.a) - Math.Sin(powBSubA));

            // вычисление z2
            _z2 = (1 + Math.Sin(powB)) / Math.Cos(powB);
        }

        #endregion
    }
}
