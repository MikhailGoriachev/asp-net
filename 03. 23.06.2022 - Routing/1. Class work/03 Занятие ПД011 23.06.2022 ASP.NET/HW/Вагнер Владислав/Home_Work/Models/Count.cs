using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home_Work.Models
{
    public class Count
    {
        private double _a;
        public double A {
            get { return _a; } 
            set { _a = value; }
        }
        private double _b;
        public double B {
            get { return _b; } 
            set { _b = value; }
        }

        //Результат вычислений 
        public (double z1, double z2) Result;

        public Count(double a, double b)
        {
            A = a;
            B = b;
        }

        public void Calculate()
        {
            double powB = 2d * _b;
            double temp = powB - _a;

            Result.z1 = (Math.Sin(_a) + Math.Cos(temp)) / (Math.Cos(_a) + Math.Sin(temp));
            Result.z2 = (1 + Math.Sin(powB))/Math.Cos(powB);
        }
    }
}
