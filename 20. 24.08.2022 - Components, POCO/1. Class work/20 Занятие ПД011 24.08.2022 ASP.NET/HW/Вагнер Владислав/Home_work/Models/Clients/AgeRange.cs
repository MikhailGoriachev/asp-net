using System.ComponentModel.DataAnnotations;
using Home_work.Infrastructure;

namespace Home_work.Models.Clients;

public class AgeRange
{
    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(18, 65, ErrorMessage = "Введите число в диапазоне от 18 до 65")]
    [Display(Name = "Минимальный возраст")]
    public int Min { get; set; }


    [Required(ErrorMessage = "Поле обязатено к заполнению!")]
    [UIHint("Number")]
    [Range(18, 65, ErrorMessage = "Введите число в диапазоне от 18 до 65")]
    [Display(Name = "Максимальный возраст")]
    public int Max { get; set; }

    public AgeRange(int min, int max)
    {
        if (min >= max)
            throw new Exception("Мин. значение диапазона должно быть < максимального");

        Min = min;
        Max = max;
    }

    public AgeRange():this(20,50)
    {

    }
}
