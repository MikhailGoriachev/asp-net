using System;
using System.Text;
using System.Threading;

namespace HW_1.Utilities
{
    // вспомогательные методы и объекты - статический класс, т.е. класс,
    // содержащий только статические члены и методы
    public static class Utils
    {
        
        // объект для получения случайных чисел
        public static Random Random = new Random();
        
        // формирование случайных чисел в диапазоне от lo до hi
        public static double GetRandom(double lo, double hi)
            => lo + (hi - lo)*Random.NextDouble();
        public static int GetRandom(int lo, int hi)
            => Random.Next(lo,hi);

        
    } // class Utils
}