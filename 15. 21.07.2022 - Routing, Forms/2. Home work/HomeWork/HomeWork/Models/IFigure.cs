namespace HomeWork.Models
{
    // Класс Фигура
    public interface IFigure
    {
        // максимальный id
        public static int CurrentMaxId { get; set; } = 0;


        // id
        public int Id { get; set; }


        // название
        public string? Name { get; }


        // изображение
        public string? Image { get; }


        // вычисление площади
        public double Perimeter { get; }


        // вычисление периметр
        public double Area { get; }


        // словарь параметров и их значений
        public Dictionary<string, double> ParamAndValue { get; }
    }
}
