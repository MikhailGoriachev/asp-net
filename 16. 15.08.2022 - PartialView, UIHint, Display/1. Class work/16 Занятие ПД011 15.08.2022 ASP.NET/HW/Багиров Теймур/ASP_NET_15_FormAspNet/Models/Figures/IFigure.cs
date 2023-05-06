using System.Collections.Generic;

namespace ASP_NET_15_FormAspNet.Models.Figures
{
    // интерфейс плоских геометрических фигур
    public interface IFigure {  
        // ид фигуры
        public int Id { get; set; }
        
        // название (тип фигуры)
        public string? Name { get; }
 
        // изображение  
        public string? Image { get; }
 
        // вычисление площади
        public double Perimeter { get; }
 
        // вычисление периметр
        public double Area { get; } 

        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue { get;}
    }
}