using System.ComponentModel.DataAnnotations;

namespace HomeWork.Models;

// Класс Выржение 3
public record Expression03(double? A, double? B)
{
    // z1
    public double? Z1
    {
        get
        {
            if (A == null || B == null)
                return null;

            // временные значения
            double a = (double)A, b = (double)B, powB = b * b, powBSubA = powB - a;

            return (Math.Sin(a) + Math.Cos(powBSubA)) /
                   (Math.Cos(a) - Math.Sin(powBSubA));
        }
    }


    // z2
    public double? Z2
    {
        get
        {
            if (B == null)
                return null;

            // временные значения
            double b = (double)B, powB = b * b;

            return (1 + Math.Sin(powB)) / Math.Cos(powB);
        }
    }

}
